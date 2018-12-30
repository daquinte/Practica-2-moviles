using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Serialization
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/// <summary>
/// Clase que se encarga de la gestión del juego.
/// Es la que controla todos los elementos del juego (...)
/// Y la que mantiene los datos globales del juego entre escenas
/// </summary>
public class GameManager : MonoBehaviour
{


    public int Rubies { get; set; }                       //Moneda de pago
    public int Estrellas { get; set; }                    //Moneda F2P

    bool[] nivelesAccesibles;                       //Guarda los niveles accesibles por el jugador
    int[] estrellasPorNivel;                        //Guardas las estrellas por nivel
    int[] puntosPorNivel;                           //Guarda los puntos por nivel

    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //Solo queremos 1 gameManager
        else if (instance != this)
            Destroy(gameObject);

        //InitGame();
    }
    #endregion  //Awake and Singleton

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Esto no acabará estando aqui

    /// <summary>
    /// Guarda el estado del juego en un .dat
    /// crea el archivo y lo guarda.
    /// Application.persistentDataPath es independiente del dispositivo
    /// 
    /// </summary>
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/ProgresoJugador.dat");

        PlayerData data = new PlayerData(Rubies, Estrellas);

        //Serializamos data y lo guardamos en file
        bf.Serialize(file, data);

        //Cerramos file
        file.Close();
    }


    public void Load()
    {
        //Comprobamos si el archivo existe antes de abrirlo
        if (File.Exists(Application.persistentDataPath + "/ProgresoJugador.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/ProgresoJugador.dat", FileMode.Open);

            //Tenemos que castear la deserializacion que ha leido a Playerdata
            PlayerData data =(PlayerData)bf.Deserialize(file);
            file.Close();

            //Esto hay que ponerlo mejor, es para la prueba!
            Estrellas = data._estrellas;
            Rubies = data._rubies;
        }
    }

    //Datos que vamos a guardar en el .dat; Se podría hacer mejor.
    //Estos datos se tienen que serializar
    [System.Serializable]
    class PlayerData
    {
        public PlayerData (int rubies, int estrellas)
        {
            _rubies = rubies;
            _estrellas = estrellas;
        }

        public int _rubies;
        public int _estrellas;
    }


    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 30), "Estrellas: " + Estrellas);
        GUI.Label(new Rect(10, 40, 150, 30), "Rubies: " + Rubies);
    }
}
