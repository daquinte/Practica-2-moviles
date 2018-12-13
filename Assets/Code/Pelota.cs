using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pelota : MonoBehaviour {

    #region Atributos
    public float velocidad;

    bool vueltaACasa = false;
    Vector3 posicionOriginal;   //Position

    RequireComponent RigidBody2D;
    #endregion

    // Use this for initialization
    void Start () {
        posicionOriginal = transform.position;
        Physics2D.IgnoreLayerCollision(9, 9);         //Hace que las pelotas se ignoren (Todas están en layer 9)
    }

    public void LaunchBall(Vector3 pos, Vector2 dir)
    {
        transform.position = pos;
        GetComponent<Rigidbody2D>().velocity = dir * velocidad * Time.deltaTime;

        //Añadimos la pelota
        
        LevelManager.instance.SumaPelota(this);
    }

    /// <summary>
    /// Detiene la pelota y la lleva a la posición de origen en la que estaba cuando se
    /// activó el componente. El desplazamiento lo hace durante time segundos.
    /// Cuando llega se destruye.
    /// </summary>
    /// <param name="time">Tiempo que tarda en llegar</param>
    /// <param name="callback">Función callback</param>
    public void GoToSpawner(float time, System.Action<Pelota> callback)
    {
        vueltaACasa = true;
     
        
        GetComponent<CircleCollider2D>().isTrigger = true; //Ignora colisiones con los bloques
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        StartCoroutine(GoTo(time, callback));
    }

    private IEnumerator GoTo(float time, System.Action<Pelota> callback)
    {
        while (vueltaACasa)
        {
            time *= Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, posicionOriginal, time);
            if (transform.position == posicionOriginal) {
                vueltaACasa = false;


                if (callback != null)
                    callback(this);
              

                Destroy(gameObject);

            }

            yield return null;
        }

       
    }

}

