using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(LectorTXT))]

public class LevelManager : MonoBehaviour
{

    #region Attributes

    //ATRIBUTOS DE LEVELMANAGER

    public static int numeroNivelActual = 0;

    LectorTXT lectorNivel;
    bool gameOver;
    public bool Pausa { get; set; }
    public int puntuacionMaxima;                           //Limite de la barra. Si has llegado = 3 estrellas
    private int puntuacionActual;
    private int multiplicadorPuntuacion;

    private int numeroEstrellas;


    public const int bordeLateralIzquierdo = -2;
    public const int bordeLateralDerecho = 12;
    public const int bordeSuperior = 2;
    public const int bordeInferior = -14;


    List<GameObject> ListaObjetosADestruir;

    public int numMaxPelotas;                                //Numero (máximo actual) de pelotas que va a generar el spawner.
    int numPelotasAct;                                      //Numero de pelotas por el tablero
    public Pelota PelotaPrefab;                             //Prefab de la pelota
    List<Pelota> ListaPelotas;                             //Array de pelotas


    List<Bloque> ListaBloques;                              //Lista de Bloques
    public Bloque Bloque_1;                                 //Prefab del bloque 1
    public Bloque Bloque_2;                                 //Prefab del bloque 2
    public Bloque Bloque_3;                                 //Prefab del bloque 3
    public Bloque Bloque_4;                                 //Prefab del bloque 4
    public Bloque Bloque_5;                                 //Prefab del bloque 5
    public Bloque Bloque_6;                                 //Prefab del bloque 6

    List<GameObject> ListaPowerUps;                         //Lista de los power ups de la escena.
    public GameObject PU_sumaPelotas1;                      //Prefab del powerup
    public GameObject PU_sumaPelotas2;                      //Prefab del powerup
    public GameObject PU_sumaPelotas3;                      //Prefab del powerup

    public GameObject PU_Laser_Horizontal;                  //Prefab del laser horizontal
    public GameObject PU_Laser_Vertical;                    //Prefab del laser vertical



    public Spawner spawner;                                 //Spawner del nivel
    bool llegadaPrimeraPelota;                              //Bool que determina si ha llegado la primera serpiente
    Vector3 spawnerPosition;                                //Posicion siguiente/actual del spawner
    bool puedeInstanciar;                                   //Determina si puede generar bolas o no

    public DeathZone deathZone;                             //Deathzone del nivel
    public GameObject warning;                              //Warning de que estás a punto de morir

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
    }
    #endregion

    //Use this for init
    //TODO: ordenar eso mejor
    void Start()
    {
        gameOver = false;
        Pausa = false;

        lectorNivel = GetComponentInChildren<LectorTXT>();
        if (numeroNivelActual == 0) numeroNivelActual = 1;
        lectorNivel.LoadLevel(numeroNivelActual);

        ListaObjetosADestruir = new List<GameObject>();
        ListaPowerUps = new List<GameObject>();

        puntuacionMaxima = ListaBloques.Count * ListaBloques.Count * numeroNivelActual;

        //Le damos al CanvasManager la puntuacionMaxima para las estrellas
        CanvasManager.instance.SetMaxPuntuacion(puntuacionMaxima);

        puntuacionActual = 0;
        multiplicadorPuntuacion = 10;


        numeroEstrellas = 0;


        puedeInstanciar = true;
        spawnerPosition = spawner.gameObject.transform.position;

        ListaPelotas = new List<Pelota>();

        shootLine = GetComponentInChildren<LineRenderer>();

        if (numMaxPelotas == 0)
        {
            numMaxPelotas = 100;         //Valor inicial
        }
        numPelotasAct = 0;


    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!gameOver && !Pausa)
        {

            //INPUT

            if (Input.GetMouseButtonDown(0) && puedeInstanciar)
            {
                shootLine.enabled = true;
            }

            if (Input.GetMouseButtonUp(0) && puedeInstanciar)
            {
                ///
                //Si el raton está menos de -1 o más de 11 en la X
                //O que no sea mayor que 1 y no sea menos que -13 en la Y
                //Y además, compruebas que no hayas pulsado en la UI
                ///
                Vector3 mousePos = Input.mousePosition;
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(new Vector2(mousePos.x, mousePos.y));

                if ((touchPos.x >= bordeLateralIzquierdo && touchPos.x <= bordeLateralDerecho)
                    && (touchPos.y >= bordeInferior && touchPos.y <= bordeSuperior)
                    && !EventSystem.current.IsPointerOverGameObject())
                {
                    shootLine.enabled = false;
                    numPelotasAct = 0;          //Establecemos el nº de pelotas en el tablero

                    //Disparo de las pelotas
                    spawner.GeneraPelotas(numMaxPelotas, PelotaPrefab);
                    spawner.gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    puedeInstanciar = false;
                }
            }
        }

    }



    #region Level Manager Methods
    /// <summary>
    ///Metodo encargado de situar el spawner en la nueva posición,
    ///de bajar todos los muros 1 posicion hacia abajo -> Y comprobar si se ha acabado la partida!
    ///establecer puntos, nuevo numero max de pelotas.
    /// </summary>
    private void PreparaSiguienteGameRound()
    {
        ///
        //Compruebas si se ha acabado la partida,
        //y si es asi sacas un mensaje de ¡Has ganado!

        ///

        if (ListaBloques.Count == 0)
        {
            CanvasManager.instance.ActivaPanelGanador();
        }

        //Si no has ganado, preparas la siguiente ronda
        else
        {
            int it = 0;
            while (!gameOver && it < ListaBloques.Count)
            {
                ListaBloques[it].transform.position -= new Vector3(0, 1, 0);

                if ((ListaBloques[it].transform.position.y - spawner.transform.position.y) <= 0)
                {
                    gameOver = true;
                    CanvasManager.instance.ActivaPanelPerdedor();
                }
                //Si algún bloque está lo suficientemente cerca del Spawner activamos el warning
                else if ((ListaBloques[it].transform.position.y - spawner.transform.position.y) <= 4 && !warning.activeSelf)
                {
                    warning.SetActive(true);
                }

                //Si estabas en peligro y dejas de estarlo, apaga el warning
                else if (warning.activeSelf && (ListaBloques[it].transform.position.y - spawner.transform.position.y) > 4)
                {
                    warning.SetActive(false);
                }

                it++;
            }

            //ACTUALIZA SPAWNER
            puedeInstanciar = true;
            llegadaPrimeraPelota = false;

            //ELIMINA LOS OBJETOS INNECESARIOS(Power ups)
            if (ListaObjetosADestruir.Count != 0)
            {
                foreach (GameObject GO in ListaObjetosADestruir)
                {
                    Destroy(GO);
                }

                ListaObjetosADestruir.Clear();
            }


            //BAJAR LOS POWER UPS ACTIVOS
            foreach (GameObject PowerUp in ListaPowerUps)
            {
                PowerUp.transform.position -= new Vector3(0, 1, 0);
            }

            //ACTUALIZA PUNTOS
            numPelotasAct = 0;
            multiplicadorPuntuacion = 0;
        }
    }

    /// <summary>
    /// Cuando el ususario compra el power ups, y la compra es aceptada
    /// se llama a este metodo para que genere n lasers
    /// </summary>
    public void ColocaPowerUpLasers(int n)
    {
        int i = 0;

        do
        {
            int x, y;
            x = Random.Range(bordeLateralIzquierdo, bordeLateralDerecho + 1);
            y = Random.Range(bordeSuperior, bordeInferior + 1);
            GameObject powerUp = null;

            RaycastHit2D raycast = Physics2D.Raycast(new Vector2(x, y), Vector2.zero);
            if (raycast.collider == null)
            {
                //No ha chocado con nada, instancio un rayo aleatorio
                int randomBinario = Random.Range(0, 2);
                if (randomBinario == 0)
                {
                    powerUp = Instantiate(PU_Laser_Horizontal, new Vector3(x, y, 0), Quaternion.identity);
                    ListaPowerUps.Add(powerUp);
                }
                else
                {
                    powerUp = Instantiate(PU_Laser_Vertical, new Vector3(x, y, 0), Quaternion.identity);
                    ListaPowerUps.Add(powerUp);
                }

                i++;                            //Sumamos i 

            }

        }
        while (i < n);
    }


    //Método que suma puntos a la puntuación actual
    public void SumaPuntos()
    {
        puntuacionActual += multiplicadorPuntuacion;
        multiplicadorPuntuacion += 10;
    }


    /// <summary>
    /// Suma n pelotas al numero maximo de pelotas que tienes en este nivel
    /// </summary>
    /// <param name="n">pelotas a sumar</param>
    public void SumaPelotasAlNumeroMaximo(int n)
    {
        numMaxPelotas += n;
    }

    /// <summary>
    /// Pasa al siguiente nivel del nivel que estés, independientemente de cual sea
    /// y avisa a gamemanager de que ese nivel está desbloqueado
    /// </summary>
    public void SiguienteNivel()
    {
        if (numeroNivelActual + 1 < 10)
        {

            numeroNivelActual++;
            GameManager.instance.DesbloqueaNivel(numeroNivelActual);
            SceneManager.LoadScene("GameScene");

        }
        else
        {

            CanvasManager.instance.ActivaPanelFinNivel();

        }

    }

    /// <summary>
    /// Carga el nivel dado por parametro
    /// </summary>
    /// <param name="nivel"></param>
    public void CargaNivel(int nivel)
    {
        numeroNivelActual = nivel;
    }

    public void ReiniciaNivel()
    {
        SceneManager.LoadScene("GameScene");

    }

    //Carga el menu principal
    public void CargaMenuPrincipal()
    {
        SceneManager.LoadScene("Menu Seleccion");
        GameManager.instance.Save();
    }


    /*
    GETTERS
    */
    public int GetPuntuacionActual() { return puntuacionActual; }
    public int GetPelotasSpawner() { return (numMaxPelotas - numPelotasAct); }
    #endregion



    /// <summary>
    /// Método que fuerza la recogida de las pelotas en cualquier momento
    /// de la ejecución. Se activa y se desactiva la colision de los layer
    /// pelota y power up temporalmemte para evitar que colisionen 
    /// cuando no deberían.
    /// </summary>
    public void Recogida()
    {
        //TODO: DESACTIVAR EL BOTON CUANDO LE DEMOS
        foreach (Pelota p in ListaPelotas)
        {
            p.GoToSpawner(10, RestaPelota);
        }
    }



    /// <summary>
    /// Inserta el objeto dado por parámetro en la lista de objetos que se han de borrar
    /// al inicio de cada ronda de juego y se elimina de la lista correspondiente. 
    /// Idealmente se usa para powerup de laser.
    /// </summary>
    /// <param name="gameObject">Objeto a eliminar</param>
    void InsertaObjetoParaEliminar(GameObject gameObject)
    {
        ListaObjetosADestruir.Add(gameObject);

        if (gameObject.GetComponent<PowerUpLaser>())
        {
            ListaPowerUps.Remove(gameObject);

        }
    }

    #region Power Up Methods

    /// <summary>
    /// Instancia en la escena el power up del tipo dado y lo introduce en la lista.
    /// </summary>
    /// <param name="tipo">tipo del powerup</param>
    public void CreaPowerUp(int x, int y, int tipo)
    {
        GameObject powerUp = null;
        switch (tipo)
        {
            case 7: //Laser horizontal
                powerUp = Instantiate(PU_Laser_Horizontal, new Vector3(x, y, 0), Quaternion.identity);
                ListaPowerUps.Add(powerUp); //Metemos el powerup en la lista
                break;
            case 8: //Laser vertical
                powerUp = Instantiate(PU_Laser_Vertical, new Vector3(x, y, 0), Quaternion.identity);
                ListaPowerUps.Add(powerUp);
                break;
            case 21:
                powerUp = Instantiate(PU_sumaPelotas1, new Vector3(x, y, 0), Quaternion.identity);
                ListaPowerUps.Add(powerUp);
                break;
            case 22:
                powerUp = Instantiate(PU_sumaPelotas2, new Vector3(x, y, 0), Quaternion.identity);
                ListaPowerUps.Add(powerUp);
                break;
            case 23:
                powerUp = Instantiate(PU_sumaPelotas3, new Vector3(x, y, 0), Quaternion.identity);
                ListaPowerUps.Add(powerUp);
                break;
            default:
                Debug.Log("tipo no registrado! No se crea nada");
                break;
        }
    }


    public void RestaPowerUp(GameObject PowerUpQuitado)
    {
        ListaPowerUps.Remove(PowerUpQuitado);

        InsertaObjetoParaEliminar(PowerUpQuitado);
    }

    #endregion

    //Métodos de bloque
    #region  Methods Bloque
    /// <summary>
    /// Crea una instancia del prefab del Bloque
    /// según el tipo del mismo y lo introduce en la lista de Bloques
    /// </summary>
    /// <param name="x">Posicion X en el mundo</param>
    /// <param name="y">Posicion Y en el mundo</param>
    /// <param name="tipo">Tipo del bloque</param>
    /// <param name="vida">Vida del bloque</param>
    public void CreaBloque(int x, int y, int tipo, int vida)
    {
        if (ListaBloques == null)
        {
            ListaBloques = new List<Bloque>();
        }

        Bloque bloque = null;
        switch (tipo)
        {

            case 0:
                Debug.Log("No debería haber un tipo 0");
                break;
            case 1:
                bloque = Instantiate(Bloque_1);
                break;
            case 2:
                bloque = Instantiate(Bloque_2);
                break;
            case 3:
                bloque = Instantiate(Bloque_3);
                break;
            case 4:
                bloque = Instantiate(Bloque_4);
                break;
            case 5:
                bloque = Instantiate(Bloque_5);
                break;
            case 6:
                bloque = Instantiate(Bloque_6);
                break;

            default:
                Debug.Log("TIPO NO REGISTRADO. Crea un caso en el switch o revisa el txt dado.");
                break;
        }

        //Configuramos el bloque y lo metemos en el vector
        bloque.ConfiguraBloque(x, y, vida);
        ListaBloques.Add(bloque);

    }

    public void RestaBloque(Bloque bloqueQuitado)
    {
        ListaBloques.Remove(bloqueQuitado);

        Destroy(bloqueQuitado.gameObject);

        //A sumar puntos o lo que sea
    }
    #endregion

    //Métodos de la pelota para la gestion de nivel
    #region Methods Pelota

    public void SumaPelota(Pelota nuevaPelota)
    {
        numPelotasAct++;
        ListaPelotas.Add(nuevaPelota);
    }



    //GM es notificado de que ha llegado una pelota
    //Si es la ultima, reset del bool de posicion del Spawner
    public void RestaPelota(Pelota pelotaQuitada)
    {
        //La sacamos de la lista
        numPelotasAct--;

        ListaPelotas.Remove(pelotaQuitada);

        Destroy(pelotaQuitada.gameObject);


        if (numPelotasAct <= 0) //Si han llegado todas las pelotas
        {
            PreparaSiguienteGameRound();
        }
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
            spawner.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            spawnerPosition.x = pelota.gameObject.transform.position.x;
            spawner.ActualizaPosicionSpawner(spawnerPosition);

        }

        pelota.GoToSpawner(10, RestaPelota);

    }

    #endregion


    //Métodos del spawner para la gestion de nivel
    #region Methods Spawner

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