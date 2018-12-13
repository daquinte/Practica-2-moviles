using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class LevelManager : MonoBehaviour {

    #region Attributes
    int numMaxPelotas;                                      //Numero (máximo actual) de pelotas que va a generar el spawner.
    int numPelotasAct;                                      //Numero de pelotas por el tablero
    public Pelota PelotaPrefab;                             //Prefab de la pelota
    List <Pelota> pelotas;                                  //Array de pelotas
    public Button vueltaCasa;                               //Boton de vuelta a casa

    List <Bloque> bloques;                                  //Bloques

    public Spawner spawner;                                 //Spawner del nivel
    Vector3 spawnerPosition;                                //Posicion siguiente/actual del spawner
    bool puedeInstanciar;                                   //Determina si puede generar bolas o no

    public DeathZone deathZone;                             //Deathzone del nivel

    #region delegate                                        
    public delegate void LLegadaPelota();                   //Delegate para la función de recogida de pelotas
    public LLegadaPelota llegada;
    #endregion



    LineRenderer shootLine;                                 //Marca la trayectoria de disparo


    #endregion

    #region Singleton
    public static LevelManager instance;

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

        pelotas = new List<Pelota>();

        llegada = new LLegadaPelota(RestaPelota);
        shootLine = GetComponentInChildren<LineRenderer>();
      
        numMaxPelotas = 10;         //Valor inicial
        numPelotasAct = 0;
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
            spawner.GeneraPelotas(numMaxPelotas, PelotaPrefab);
            numPelotasAct = numMaxPelotas;          //Establecemos el nº de pelotas en el tablero
            puedeInstanciar = false;
        }

    }

   
  

    public void Recogida()
    {
        foreach (Pelota p in pelotas)
        {
           p.SetVueltaACasa();
        }

    }




    ///Metodo encargado de situar el spawner en la nueva posición,
    ///de bajar todos los muros 1 posicion hacia abajo -> Y comprobar si se ha acabado la partida!
    ///establecer puntos, nuevo numero max de pelotas y de algo más que no recuerdo ahora mismo.
    // PUBLIC???
    private void PreparaSiguienteGameRound()
    {
        Debug.Log("*PREPARNDO NUEVO GAME ROUND*");

        //REVISA SI HAS MUERTO

        //ACTUALIZA SPAWNER
        puedeInstanciar = true;
        spawner.ActualizaPosicionSpawner(spawnerPosition);

        //ACTUALIZA PUNTOS Y DEMÁS MIERDAS
        numPelotasAct = 0;
        //numMaxPelotas += 10;    
    }



//Métodos de la pelota para la gestion de nivel
    #region Methods Pelota

    public void SumaPelota(Pelota nuevaPelota)
    {
        pelotas.Add(nuevaPelota);
        numPelotasAct++;
    }

    //GM es notificado de que ha llegado una pelota
    //Si es la ultima, reset del bool de posicion del Spawner
    public void RestaPelota()
    {
        numPelotasAct--;

        if (numPelotasAct <= 0) //Si han llegado todas las pelotas
        {
            PreparaSiguienteGameRound();
            deathZone.ResetPrimeraPelota();
        }
    }

    #endregion


    //Métodos del spawner para la gestion de nivel
    #region Spawner Methods
    public void SetSpawnerPosition(float newX)
    {
        spawnerPosition.x = newX;     //Guardamos la nueva posicion
    }
    public Vector3 GetSpawnerPosition()
    {
        return spawnerPosition;
    }

    public int getBolasAct()
    {
        return numPelotasAct;
    }
#endregion



}

//TODO: Revisar si se restan y se suman las pelotas en el levelManager -> ¿El delegate funciona?