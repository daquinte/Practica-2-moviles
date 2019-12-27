﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pelota : MonoBehaviour {
    
    RequireComponent RigidBody2D;
    const int velocidad = 500;


    // Use this for initialization
    void Start () {
        Physics2D.IgnoreLayerCollision(9, 9);         //Hace que las pelotas se ignoren (Todas están en layer 9)
    }

    //TODO: Mirar lo del Fixed Update
    public void LaunchBall(Vector3 pos, Vector2 dir)
    {
        transform.position = pos;
        GetComponent<Rigidbody2D>().velocity = dir * velocidad * Time.deltaTime * 1.5f;

        //Añadimos la pelota a la instancia de LevelManager
        LevelManager.instance.SumaPelota(this);
    }

    //Acelera la pelota aplicandole una fuerza
    public void Acelera()
    {
        //PROBLEMA QUE LE VEO A ESTO: ¿Como sabes la dirección hacia la que aplicarle la fuerza?
        //Hay una forma que sería calcular la dirección sabiendo una posicion antigua de la pelota y luego restandola
        //A la posicion actual= eso te da la dirección, que luego hay una vaina que haces InverseTransformDirection y aquello funciona
        //Lo malo sería que todas las bolas tendrían que estar registrando en cada frame su posicion anterior.
        //No es un drama, peeeero... 

        /*
            var direction = transform.position - lastPosition;
            var localDirection = transform.InverseTransformDirection(direction);
            lastPosition = transform.position;
         */

        //GetComponent<Rigidbody2D>().AddForce()
    }


    /// <summary>
    /// Detiene la pelota y la lleva a la posición del spawner. 
    /// El desplazamiento lo hace durante time segundos.
    /// </summary>
    /// <param name="time">Tiempo que tarda en llegar</param>
    /// <param name="callback">Función callback</param>
    public void GoToSpawner(float time, System.Action<Pelota> callback)
    {


        GetComponent<CircleCollider2D>().enabled = false;
        
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        Vector3 meta = LevelManager.instance.GetSpawnerPosition();

        StartCoroutine(GoTo(time, meta, callback));

    }

    private IEnumerator GoTo(float time, Vector3 meta, System.Action<Pelota> callback)
    {
        bool stop = false;
        while (!stop)
        {
            transform.position = Vector3.MoveTowards(transform.position, meta, time*Time.deltaTime);

            if (transform.position == meta) {
                stop = true;

                if (callback != null)
                    callback(this);

            }

            yield return null;
        }

        yield break;            //Detiene la corroutina
       
    }

}

