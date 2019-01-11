using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    int Rubies;           //Moneda de pago
    int Estrellas;            //Moneda F2P

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

        Rubies = 100;
        Estrellas = 0;

        nivelesAccesibles = new bool[10];
        for (int i = 0; i < 10; i++)
        {
            nivelesAccesibles[i] = false;
        }
        nivelesAccesibles[0] = true;

        estrellasPorNivel = new int[10];
        puntosPorNivel = new int[10];
        //For para inicializar los vectores de estrellas y puntos
        for (int i = 0; i < 10; i++)
        {
            estrellasPorNivel[i] = 0;
            puntosPorNivel[i] = 0;
        }


        Load();
    }


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

        PlayerData data = new PlayerData(Rubies, Estrellas, nivelesAccesibles, estrellasPorNivel, puntosPorNivel);

        //Serializamos data y lo guardamos en file
        bf.Serialize(file, data);

        //Cerramos file
        file.Close();
    }

    /// <summary>
    /// Carga el estado del archivo .dat previamente creado
    /// si este existiera.
    /// </summary>
    public void Load()
    {
        //Comprobamos si el archivo existe antes de abrirlo
        if (File.Exists(Application.persistentDataPath + "/ProgresoJugador.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/ProgresoJugador.dat", FileMode.Open);

            //Tenemos que castear la deserializacion que ha leido a Playerdata
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            //Esto hay que ponerlo mejor, es para la prueba!
            Estrellas = data._estrellas;
            Rubies = data._rubies;

            for (int i = 0; i < 10; i++)
            {
                nivelesAccesibles[i] = data._nivelesAccesibles[i];
                estrellasPorNivel[i] = data._estrellasPorNivel[i];
                puntosPorNivel[i] = data._puntosPorNivel[i];
            }
        }

    }

    //Datos que vamos a guardar en el .dat; Se podría hacer mejor.
    //Estos datos se tienen que serializar
    [System.Serializable]
    class PlayerData : System.Object
    {
        public PlayerData(int rubies, int estrellas, bool[] niveles, int[] estrellasNivel, int[] puntosNivel)
        {
            _rubies = rubies;
            _estrellas = estrellas;

            _nivelesAccesibles = niveles;
            _estrellasPorNivel = estrellasNivel;
            _puntosPorNivel = puntosNivel;
        }


        public int _rubies { get; set; }
        public int _estrellas { get; set; }

        public bool[] _nivelesAccesibles;                       //Guarda los niveles accesibles por el jugador
        public int[] _estrellasPorNivel;                       //Guardas las estrellas por nivel
        public int[] _puntosPorNivel;                          //Guardamos los puntos por nivel
    }

    /// <summary>
    /// Resta num rubies al contador global de rubies del juego
    /// </summary>
    /// <param name="num"></param>
    public bool RestaRubies(int num)
    {
        if (Rubies - num >= 0)
        {
            Rubies -= num;
            return true;
        }

        else return false;


    }

    //GETTERS RUBIES Y ESTRELLAS
    public int GetRubies() { return Rubies; }
    public int GetEstrellas() { return Estrellas; }

    public void SumaEstrellas(int nivel)
    {
        Debug.Log("SumoEstrella");
        if (estrellasPorNivel[nivel - 1] < 3)
        {
            Estrellas++;
            Debug.Log(Estrellas);
            estrellasPorNivel[nivel - 1]++;
        }

        Save();
    }

    public void CargaNivel(int nivel)
    {

        if (nivelesAccesibles[nivel - 1])
        {
            SceneManager.LoadScene("GameScene");
            LevelManager.numeroNivelActual = nivel;
        }
    }

    /// <summary>
    /// Abre el acceso a los niveles superados en el menú de selección de nivel
    /// </summary>
    public void DesbloqueaNivel(int nivel)
    {

        nivelesAccesibles[nivel - 1] = true;
        Save();
        Debug.Log("He guardado el nivel " + (nivel - 1));

    }

}
