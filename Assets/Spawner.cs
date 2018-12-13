using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Spawner : MonoBehaviour {
    #region Attributes
    public GameObject PelotaPrefab;

    TextMesh textoPelotas;


    int numBolasASpawnear;
    int numBolasEnSpawner;
    #endregion

    // Use this for initialization
    void Start () {
        textoPelotas = GetComponentInChildren<TextMesh>();
    }

    private void Update()
    {
        
        textoPelotas.text = (numBolasEnSpawner).ToString();
    }

    /*
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
                transform.position = LevelManager.instance.GetSpawnerPosition();

                //Preparamos la nueva ronda de juego
                spawnerState = SpawnerState.Lanzamiento;
                LevelManager.instance.PreparaSiguienteGameRound();
            }
        }
    }*/

     public void ActualizaPosicionSpawner(Vector3 nuevaPos)
    {
        transform.position = nuevaPos;
    }

    public void GeneraPelotas(int numMaxPelotasAct, Pelota p)
    {
        numBolasASpawnear = numMaxPelotasAct;
        numBolasEnSpawner = numMaxPelotasAct;
        StartCoroutine(InstanciaPelota(p));
    }

    IEnumerator InstanciaPelota(Pelota pelotaPrefab)
    {
        for (int i = 0; i < numBolasASpawnear; i++)
        {
            Pelota nuevaPelota = Instantiate(pelotaPrefab);

            Vector3 mousePos = Input.mousePosition;
            Vector2 targetPos = Camera.main.ScreenToWorldPoint(new Vector2(mousePos.x, mousePos.y));

            Vector2 dir = (targetPos - new Vector2(transform.position.x, transform.position.y)).normalized;

            nuevaPelota.LaunchBall(transform.position, dir);

            numBolasEnSpawner--;

            yield return null;
        }

      

        
        yield break;    //Stop coroutine
    }
}
