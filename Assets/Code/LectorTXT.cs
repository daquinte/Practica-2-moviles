using System;
using UnityEngine;
using System.IO;
using System.Collections.Generic;


//¿Streamreader o textAsset? Cual es mejor para Android?
public class LectorTXT : MonoBehaviour
{


    //List <InfoBloque> listaInfo = new List<InfoBloque>();
    List<int> listaInfo = new List<int>();
    char[] caracteresDelimitadores = { ',', '.' };


    /// <summary>
    /// Carga el nivel "level" de su txt correspondiente
    /// Recorre el txt por layers, cuando encuentra uno ignora dos líneas 
    /// y entonces procesa la informacion, que guarda en el struct de properties de Bloque
    /// </summary>
    /// <param name="level">Nivel a cargar</param>
    public void LoadLevel(int level)
    {
        try
        {
            string path;
            StreamReader reader;
#if UNITY_ANDROID && !UNITY_EDITOR
            //Read from the resources file using TextAsset
           TextAsset pathTextAsset = Resources.Load<TextAsset>("mapdata" + level.ToString());
            path = pathTextAsset.text;

            byte[] byteArray = System.Text.Encoding.ASCII.GetBytes(path);
            MemoryStream memoryStream = new MemoryStream(byteArray);

             reader = new StreamReader(memoryStream);


#endif
#if UNITY_EDITOR
            //Read the text from directly from the test.txt file
            path = "Assets/Maps/" + "mapdata" + level.ToString() + ".txt";
            reader = new StreamReader(path);
#endif



            if (reader != null) Debug.Log(reader + " abierto con exito");

            // You generally use the "using" statement for potentially memory-intensive objects
            // instead of relying on garbage collection.
            using (reader)
            {
                string line;        //Linea leida
                int filaTxt = 0;    //Fila del archivo que estamos leyendo
                int j = 0;          //Fila de la zona numerica del txt
                int layer = 0;      //Itera sobre los layers. 1 = tipo 2 = vida.

                int indiceTipo = 0; //Itera sobre los tipos leidos en layer 1

                //IGNORE
                bool IgnoreReading = false;
                int lineaDesdeLaQueIgnoramos = 0;
                //

                while ((line = reader.ReadLine()) != null)
                {


                    if (line == "[layer]")
                    {
                        if (!IgnoreReading)
                        {
                            //Has llegado al final del layer
                            layer++;

                            lineaDesdeLaQueIgnoramos = filaTxt;
                            IgnoreReading = true;
                        }
                        else Debug.Log("Se esperaba IgnoreReading a false");
                    }

                    else if (IgnoreReading && filaTxt == lineaDesdeLaQueIgnoramos + 2)
                    {

                        IgnoreReading = false;  //Leemos la zona de numeros
                        lineaDesdeLaQueIgnoramos = 0;
                    }

                    else if (!IgnoreReading)
                    {

                        //Separamos el line en argumentos.
                        string[] entries = line.Split(caracteresDelimitadores);

                        //Leemos e interpretamos lo que hemos leido
                        for (int i = 0; i < entries.Length - 1; i++)
                        {

                            //Si estás en el layer 1, guardas los tipos en la lista
                            if (layer == 1)
                            {
                                if (int.Parse(entries[i]) != 0)
                                {
                                    listaInfo.Add(int.Parse(entries[i]));
                                }
                            }

                            //Si estás en el layer 2, guardas la vida, interpretas la posicion x e y a coordenadas de mundo
                            //Y lo creas, sacando el tipo de la lista de informacion
                            else if (layer == 2)
                            {
                                if (listaInfo[indiceTipo] > 0 && listaInfo[indiceTipo] <= 6)
                                {
                                    if (int.Parse(entries[i]) != 0)
                                    {
                                        GetComponent<LevelManager>().CreaBloque(i, -j, listaInfo[indiceTipo], int.Parse(entries[i]));
                                        indiceTipo++;
                                    }
                                }

                                else
                                {
                                    if (int.Parse(entries[i]) != 0)
                                    {
                                        Debug.Log("Creando un power up");
                                        GetComponent<LevelManager>().CreaPowerUp(i, -j, listaInfo[indiceTipo]);
                                        indiceTipo++;
                                    }
                                }


                            }

                            else Debug.Log("ERROR CATASTROFICO: Se esperaba layer entre 1 y 2");

                        }

                        if (layer == 2)
                        {
                            j++;    //Sumamos la fila de la matriz numerica
                        }
                    }

                    //En cualquier caso, aumento el contador de fila leida;
                    filaTxt++;

                }


            }

            listaInfo.Clear();
            reader.Close();
        }

        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
    }

    /*
     * ESTO NOS VA A SERVIR PARA LO DE LEER PROGRESO
    static void WriteString()
    {
        string path = "Assets/Resources/test.txt";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine("Test");
        writer.Close();

        //Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset(path);
        TextAsset asset = Resources.Load("test");

        //Print the text from the file
        Debug.Log(asset.text);
    }*/

}


