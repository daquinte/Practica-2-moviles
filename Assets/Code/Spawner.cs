using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Spawner : MonoBehaviour {

    #region Attributes
    public GameObject PelotaPrefab;

    TextMesh textoPelotas;                  //Texto del Spawner
    int numPelotasSpawner;                  //Nº de Pelotas almacenadas en el Spawner
    #endregion

    // Use this for initialization
    void Start () {
        textoPelotas = GetComponentInChildren<TextMesh>();
    }

    void Update()
    {
        textoPelotas.text = (numPelotasSpawner).ToString();
    }

    //Método para modificar su posición cuando se lo indique LevelManager
     public void ActualizaPosicionSpawner(Vector3 nuevaPos)
    {
        transform.position = nuevaPos;
        
        //Reactivar el texto y empezar a contar las pelotas que han llegado
    }

    public void SumaContadorSpawner()
    {
        numPelotasSpawner++;
    }

    /// <summary>
    /// Genera numPelotas instancias del prefab de Pelota que recibe,
    /// Mediante una coroutina que las instancia en la posicion del raton
    /// en el momento en el que el método fue llamado.
    /// </summary>
    /// <param name="numPelotas">Numero de pelotas que tiene que generar</param>
    /// <param name="p">PRefab de pelota</param>
    public void GeneraPelotas(int numPelotas, Pelota p)
    {
        //Desactivar el texto
        numPelotasSpawner = numPelotas;
        Vector3 mousePos = Input.mousePosition;
        Vector2 targetPos = Camera.main.ScreenToWorldPoint(new Vector2(mousePos.x, mousePos.y));
        Vector2 posOrigen = new Vector2(transform.position.x, transform.position.y);

        //Calcular la direccion restando el target a la posicion de origen2D
        targetPos -= posOrigen;      


        StartCoroutine(InstanciaPelota(numPelotas, p, targetPos, transform.position));
    }

    IEnumerator InstanciaPelota(int numPelotas, Pelota pelotaPrefab, Vector2 targetPos, Vector3 posOrigen)
    {
        for (int i = 0; i < numPelotas; i++)
        {
            Pelota nuevaPelota = Instantiate(pelotaPrefab);
            Vector2 dir = (targetPos).normalized;

            nuevaPelota.LaunchBall(posOrigen, dir);

            numPelotasSpawner--;

            yield return null;
        }
        
        yield break;    //Stop coroutine
    }
}
