using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameManager : MonoBehaviour {

    #region Attributes
    int numMaxPelotas;      //Numero (máximo actual) de pelotas que va a generar el spawner.
    int numPelotasAct;      //Numero de pelotas por el tablero

    public Spawner spawner;
    Vector3 spawnerPosition;

    public DeathZone deathZone;
    public delegate void LLegadaPelota();
    public LLegadaPelota llegada;
    bool puedeInstanciar;

    LineRenderer shootLine;

    public Button vueltaCasa;
    GameObject[] pelotas;
    int contador = 0;

    #endregion

    #region Singleton
    public static GameManager instance;

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    //Use this for init
    void Start() {
        puedeInstanciar = true;
        spawnerPosition = spawner.gameObject.transform.position;
        llegada = new LLegadaPelota(RestaPelota);
        shootLine = GetComponentInChildren<LineRenderer>();
      
        numMaxPelotas = 10;         //Valor inicial
    }

    // Update is called once per frame
    void FixedUpdate() {

        //ACTUALIZA TEXTO PUNTOS BLA BLA BLA


        //INPUT

        if (Input.GetMouseButtonDown(0) && puedeInstanciar)
        {
            shootLine.enabled = true;
        }

        if (Input.GetMouseButtonUp(0) && puedeInstanciar)
        {
            shootLine.enabled = false;
            spawner.GeneraPelotas(numMaxPelotas);
            numPelotasAct = numMaxPelotas;          //Establecemos el nº de pelotas en el tablero
            puedeInstanciar = false;
        }

    }

   
  

    public void Recogida()
    {
        pelotas = GameObject.FindGameObjectsWithTag("Bola");

        foreach (GameObject p in pelotas)
        {
            p.GetComponent<Pelota>().SetVueltaACasa();
        }

    }




    ///Metodo encargado de situar el spawner en la nueva posición,
    ///de bajar todos los muros 1 posicion hacia abajo -> Y comprobar si se ha acabado la partida!
    ///establecer puntos, nuevo numero max de pelotas y de algo más que no recuerdo ahora mismo.
    // PUBLIC???
    public void PreparaSiguienteGameRound()
    {
        Debug.Log("*PREPARNDO NUEVO GAME ROUND*");

        //REVISA SI HAS MUERTO

        //ACTUALIZA SPAWNER
        puedeInstanciar = true;

        //ACTUALIZA PUNTOS Y DEMÁS MIERDAS
    }

    //GM es notificado de que ha llegado una pelota
    //Si es la ultima, reset del bool de posicion del Spawner
    public void RestaPelota()
    {
        numPelotasAct--;

        if (numPelotasAct <= 0) //Si han llegado todas las pelotas
        {
            deathZone.ResetPrimeraPelota();
        }
    }

    public void SetSpawnerPosition(float newX)
    {
        spawnerPosition.x = newX;     //Guardamos la nueva posicion
    }
    public Vector3 GetSpawnerPosition()
    {
        return spawnerPosition;
    }

}
