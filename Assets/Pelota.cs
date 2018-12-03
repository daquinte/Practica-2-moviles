using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pelota : MonoBehaviour {

    #region Atributos
    public float velocidad;
    bool vueltaACasa;

    Vector3 posicionOriginal;   //Position
    #endregion

    // Use this for initialization
    void Start () {
        posicionOriginal = transform.position;
        Physics2D.IgnoreLayerCollision(9, 9);         //Hace que las pelotas se ignoren (Todas están en layer 9)
    }

    void Update()
    {
        
        if (vueltaACasa)
        {
            float speed = 10.0f * Time.deltaTime;
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            transform.position = Vector3.MoveTowards(transform.position, posicionOriginal, speed);
        }
    }



    public void LaunchBall(Vector2 dir)
    {
        GetComponent<Rigidbody2D>().velocity = dir * velocidad * Time.deltaTime;
    }

    public void SetVueltaACasa()
    {
        vueltaACasa = true;
        GetComponent<CircleCollider2D>().isTrigger = true; //Ignora colisiones con los bloques
        Debug.Log("A CASITA JAJA");
        
    }

}


//TODO: Intentar instanciarlas con velocidad sin acceder al componente de RigidBody2D <- Se puede???
//TODO: Que vayan al puto spawner ANTES DE ACTUALIZAR SU POSICION