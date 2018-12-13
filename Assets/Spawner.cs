using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SpawnerState { Lanzamiento, Espera };


public class Spawner : MonoBehaviour {
    #region Attributes
    public GameObject PelotaPrefab;

    TextMesh textoPelotas;

    private SpawnerState spawnerState;
    public int numBolasSpawner;
    private int numBolasPorLLegar;
    #endregion

    // Use this for initialization
    void Start () {
        textoPelotas = GetComponentInChildren<TextMesh>();
        spawnerState = SpawnerState.Lanzamiento; 
    }

    private void Update()
    {
        textoPelotas.text = (numBolasSpawner - numBolasPorLLegar).ToString();
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if(spawnerState == SpawnerState.Espera
            && collision.gameObject.tag == "Bola")
        {
            
            Destroy(collision.gameObject);
            numBolasPorLLegar--;


            if(numBolasPorLLegar <= 0)
            {
                //Actualizo posicion spawner (actualizada en la primera bola)
                transform.position = GameManager.instance.GetSpawnerPosition();

                //Preparamos la nueva ronda de juego
                spawnerState = SpawnerState.Lanzamiento;
                GameManager.instance.PreparaSiguienteGameRound();
            }
        }
    }

    public void GeneraPelotas(int numMaxPelotasAct)
    {
        numBolasSpawner = numMaxPelotasAct;
        textoPelotas.text = numBolasSpawner.ToString();
        StartCoroutine(InstanciaPelota());
    }

    IEnumerator InstanciaPelota()
    {
        for (int i = 0; i < numBolasSpawner; i++)
        {
            //La pongo un poco por encima para el deathzone
            
            GameObject pelotaAux = Instantiate(PelotaPrefab, transform.position, Quaternion.identity);

            Vector3 mousePos = Input.mousePosition;
            Vector2 targetPos = Camera.main.ScreenToWorldPoint(new Vector2(mousePos.x, mousePos.y));

            Vector2 dir = (targetPos - new Vector2(transform.position.x, transform.position.y)).normalized;

            pelotaAux.GetComponent<Pelota>().LaunchBall(dir);


            yield return null;
        }

      
        numBolasPorLLegar = numBolasSpawner;
        yield return new WaitForSeconds(1);
        spawnerState = SpawnerState.Espera;
        
        yield break;    //Stop coroutine
    }
}
