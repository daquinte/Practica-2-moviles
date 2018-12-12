using UnityEngine;
using UnityEditor;
using System.IO;

//¿Streamreader o textAsset? Cual es mejor para Android???
public class LectorTXT : MonoBehaviour
{
    public GameObject bloquePrefab1;
    //...y los demás tipos

    int[,] matrizTipo = new int [11,11];
    int[,] matrizVida = new int[11, 11];
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


    public void ReadMap(string mapPath)
    {
        string path = "Assets/Maps/mapdata1.txt";

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }
}
    

