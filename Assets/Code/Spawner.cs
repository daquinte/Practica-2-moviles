using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Spawner : MonoBehaviour {

    #region Attributes
    public GameObject PelotaPrefab;

   
    #endregion

    //Método para modificar su posición cuando se lo indique LevelManager
     public void ActualizaPosicionSpawner(Vector3 nuevaPos)
    {
        transform.position = nuevaPos;
        
        //Reactivar el texto y empezar a contar las pelotas que han llegado
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
            yield return new WaitForSeconds(0.05f);

            Pelota nuevaPelota = Instantiate(pelotaPrefab);
            Vector2 dir = (targetPos).normalized;

            nuevaPelota.LaunchBall(posOrigen, dir);

            yield return null;
        }

        CanvasManager.instance.setReturnSpawnActive(true);
        
        yield break;    //Stop coroutine
    }
}
