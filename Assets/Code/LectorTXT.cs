using System;
using UnityEngine;
using System.IO;
using System.Collections.Generic;


//¿Streamreader o textAsset? Cual es mejor para Android?
public class LectorTXT : MonoBehaviour
{
    public GameObject bloquePrefab1;
    //...y los demás tipos

    struct InfoBloque
    {
        int x, y;
        int tipo;
        int vida;
    }

    List <Bloque>     listaBloques;
    List <InfoBloque> listaInfo;
    char[] caracteresDelimitadores = { ',', '=', '.' };
    

    /// <summary>
    /// Carga el nivel "level" de su txt correspondiente
    /// </summary>
    /// <param name="level"></param>
    public void LoadLevel(int level)
    {
        try { 
         
            string path = "Assets/Maps/" + "mapdata" + level.ToString() + ".txt";
          
            //Read the text from directly from the test.txt file
            StreamReader reader = new StreamReader(path);


            // You generally use the "using" statement for potentially memory-intensive objects
            // instead of relying on garbage collection.
            using (reader)
            {
                string line;        //Linea leida
                int filaTxt = 0;    //Fila del archivo que estamos leyendo
                int layer = 0;      //Itera sobre los layers. 1 = tipo 2 = vida.

                //IGNORE
                bool IgnoreReading = false;
                int lineaDesdeLaQueIgnoramos = 0;
                //

                while ((line = reader.ReadLine()) != null) {

                    if (line == "[Layer]")
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

                    else
                    {

                        //Separamos el line en argumentos.
                        string[] entries = line.Split(caracteresDelimitadores);

                        //Leemos e interpretamos lo que hemos leido
                        //Rcuerda que filaTxt = fila e i = columna
                        for (int i = 0; i < entries.Length - 1; i++)
                        {

                            //Si estás en el layer 1, guardas los tipos


                            //Si estás en el layer 2, guardas la vida

                        }
                    }

                    //En cualquier caso, aumento el contador de fila leida;
                    filaTxt++;
                }

             
            }
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


