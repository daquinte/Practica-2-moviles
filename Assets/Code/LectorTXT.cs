using System;
using UnityEngine;
using System.IO;
using System.Collections.Generic;


//¿Streamreader o textAsset? Cual es mejor para Android?
public class LectorTXT : MonoBehaviour
{

    public struct InfoBloque
    {
      
       public int X { get; set; }
       public int Y { get; set; }
       public int Tipo { get; set; }
       public int Vida { get; set; }
        
    }

    List <InfoBloque> listaInfo = new List<InfoBloque>();
    char[] caracteresDelimitadores = { ',','.' };
    

    /// <summary>
    /// Carga el nivel "level" de su txt correspondiente
    /// Recorre el txt por layers, cuando encuentra uno ignora dos líneas 
    /// y entonces procesa la informacion, que guarda en el struct de properties de Bloque
    /// </summary>
    /// <param name="level">Nivel a cargar</param>
    public void LoadLevel(int level)
    {
        try { 
         
            string path = "Assets/Maps/" + "mapdata" + level.ToString() + ".txt";
            Debug.Log(path);
          
            //Read the text from directly from the test.txt file
            StreamReader reader = new StreamReader(path);

            if (reader != null) Debug.Log(reader + " abierto con exito");

            // You generally use the "using" statement for potentially memory-intensive objects
            // instead of relying on garbage collection.
            using (reader)
            {
                string line;        //Linea leida
                int filaTxt = 0;    //Fila del archivo que estamos leyendo
                int j = 0;          //Fila de la zona numerica del txt
                int layer = 0;      //Itera sobre los layers. 1 = tipo 2 = vida.

                //IGNORE
                bool IgnoreReading = false;
                int lineaDesdeLaQueIgnoramos = 0;
                //

                while ((line = reader.ReadLine()) != null) {

                   
                    if (line == "[layer]")
                    {
                        if (!IgnoreReading)
                        {
                            //Has llegado al final del layer
                            layer++;
                            Debug.Log("Estoy en el layer " + layer);
                            lineaDesdeLaQueIgnoramos = filaTxt;
                            IgnoreReading = true;
                        }
                        else Debug.Log("Se esperaba IgnoreReading a false");
                    }

                    else if (IgnoreReading && filaTxt == lineaDesdeLaQueIgnoramos + 2)
                    {
                        Debug.Log("Ignoradas 2 lineas con exito");
                        IgnoreReading = false;  //Leemos la zona de numeros
                        lineaDesdeLaQueIgnoramos = 0;
                    }

                    else if (!IgnoreReading)
                    {

                        //Separamos el line en argumentos.
                        string[] entries = line.Split(caracteresDelimitadores);
                        //Debug.Log(line);
                        //Leemos e interpretamos lo que hemos leido
                        //Rcuerda que filaTxt = fila e i = columna
                        for (int i = 0; i < entries.Length - 1; i++)
                        {

                            //Si estás en el layer 1, guardas la posicion logica y los tipos
                            if (layer == 1)
                            {
                                Debug.Log(-(filaTxt - 3));
                                InfoBloque aux = new InfoBloque
                                {
                                    X = i,
                                    Y = j,
                                    Tipo = int.Parse(entries[i]),
                                    Vida = 0
                                };
                                
                                listaInfo.Add(aux);
                            }

                            //Si estás en el layer 2, guardas la vida, interpretas la posicion x e y a coordenadas de mundo
                            //Y lo creas, sacandolo de la lista de informacion
                            else if (layer == 2)
                            {
                                InfoBloque aux2 = listaInfo[i];
                                aux2.Vida = int.Parse(entries[i]);
                                aux2.X = i;
                                aux2.Y = -j; //??????????????????????????????

                                if(aux2.Vida != 0) {
                                    Debug.Log("Voy a crear un bloque");
                                    GetComponent<LevelManager>().CreaBloque(aux2.X, aux2.Y, aux2.Tipo, aux2.Vida);
                                }
                            }

                            else Debug.Log("ERROR CATASTROFICO: Se esperaba layer entre 1 y 2");

                        }
                        //Debug.Log("j; " + j);
                        if(layer == 2) { 
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


