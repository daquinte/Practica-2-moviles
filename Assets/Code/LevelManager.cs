using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LectorTXT))]

public class LevelManager : MonoBehaviour {

    #region Attributes

    int numMaxPelotas;                                      //Numero (máximo actual) de pelotas que va a generar el spawner.
    int numPelotasAct;                                      //Numero de pelotas por el tablero
    public Pelota PelotaPrefab;                             //Prefab de la pelota
    List <Pelota> pelotas;                                  //Array de pelotas
    public Button vueltaCasa;                               //Boton de vuelta a casa

    List <Bloque> bloques;                                  //Bloques

    public Spawner spawner;                                 //Spawner del nivel
    bool llegadaPrimeraPelota;
    Vector3 spawnerPosition;                                //Posicion siguiente/actual del spawner
    bool puedeInstanciar;                                   //Determina si puede generar bolas o no

    public DeathZone deathZone;                             //Deathzone del nivel

    
    LectorTXT lectorNivel;


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
        
        lectorNivel = GetComponentInChildren<LectorTXT>();
        lectorNivel.LoadLevel(1);

        puedeInstanciar = true;
        spawnerPosition = spawner.gameObject.transform.position;

        pelotas = new List<Pelota>();

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
            numPelotasAct = numMaxPelotas;          //Establecemos el nº de pelotas en el tablero
            spawner.GeneraPelotas(numPelotasAct, PelotaPrefab);
       
            puedeInstanciar = false;
        }

    }

    /// <summary>
    ///Metodo encargado de situar el spawner en la nueva posición,
    ///de bajar todos los muros 1 posicion hacia abajo -> Y comprobar si se ha acabado la partida!
    ///establecer puntos, nuevo numero max de pelotas y de algo más que no recuerdo ahora mismo.
    /// </summary>
    private void PreparaSiguienteGameRound()
    {
        Debug.Log("*PREPARNDO NUEVO GAME ROUND*");

        //REVISA SI HAS MUERTO

        //ACTUALIZA SPAWNER
        puedeInstanciar = true;
        llegadaPrimeraPelota = false;

        //ACTUALIZA PUNTOS Y DEMÁS MIERDAS
        numPelotasAct = 0;
        //numMaxPelotas += 10;    
    }

    /// <summary>
    /// Determina la posicion nueva del spawner si es la primera bola
    /// Llama a la bola para avisarla de que modifique su comportamiento
    /// </summary>
    /// <param name="pelota">Pelota del deathzone</param>
    public void LlegadaPelota(Pelota pelota)
    {

        if (!llegadaPrimeraPelota)
        {
            llegadaPrimeraPelota = true;
            spawnerPosition.x = pelota.gameObject.transform.position.x;
            spawner.ActualizaPosicionSpawner(spawnerPosition);
            
        }

        pelota.GoToSpawner(10, RestaPelota);

    }





    public void Recogida()
    {
        foreach (Pelota p in pelotas)
        {
            // p.SetVueltaACasa();
        }

    }

    //Métodos de la pelota para la gestion de nivel
    #region Methods Pelota

    public void SumaPelota(Pelota nuevaPelota)
    {
        pelotas.Add(nuevaPelota);
    }

    //GM es notificado de que ha llegado una pelota
    //Si es la ultima, reset del bool de posicion del Spawner
    public void RestaPelota(Pelota pelotaQuitada)
    {
        //La sacamos de la lista
        numPelotasAct--;

        pelotas.Remove(pelotaQuitada);
        
        Destroy(pelotaQuitada.gameObject);

        if (numPelotasAct <= 0) //Si han llegado todas las pelotas
        {
            PreparaSiguienteGameRound();
        }
    }

    #endregion


    //Métodos del spawner para la gestion de nivel
    #region Spawner Methods

    public int GetBolasAct()
    {
        return numPelotasAct;
    }

    public Vector3 GetSpawnerPosition()
    {
        return spawner.gameObject.transform.position;
    }
#endregion



}