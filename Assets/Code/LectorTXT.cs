using UnityEngine;
using UnityEditor;
using System.IO;

//¿Streamreader o textAsset? Cual es mejor para Android?
public class LectorTXT : MonoBehaviour
{
    public GameObject bloquePrefab1;
    //...y los demás tipos

    int[,] MatrizTipo = new int [11,11];
    int[,] MatrizVida = new int[11, 11];
    int contadorLayer;
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


     /*Este método recibe un numero de nivel
       Y en base a eso se apaña la ruta, interpreta el nivel y lo genera
         */
    public void loadLevel(int level)
    {
        string path = "Assets/Maps/" + "mapdata" + level.ToString() + ".txt";
       
        ReadMap(path);
    }

    private void ReadMap(string mapPath)
    {
        Debug.Log("A leer");
        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(mapPath);
        if (reader != null)
            Debug.Log("Me he abierto");
        reader.Close();
    }



    private void LeeMapaEInterpreta(TextAsset textAsset)
    {
       
    }
}
    

