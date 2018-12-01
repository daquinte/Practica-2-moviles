using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pelota : MonoBehaviour {

    #region Atributos
    public float velocidad;

    bool vueltaACasa;
    Vector3 posicionOriginal;
    #endregion

    // Use this for initialization
    void Start () {
        vueltaACasa = false;
        posicionOriginal = transform.position;
    
    }
	
	// Update is called once per frame
	void Update () {
        
        if (vueltaACasa) {
            float speed = 3.0f * Time.deltaTime;
            transform.position += Vector3.MoveTowards(transform.position, posicionOriginal, speed);
        }    
    }


    public void LaunchBall(Vector3 dir)
    {
        GetComponent<Rigidbody2D>().velocity = dir * 200 * Time.deltaTime;
    }

}


//TODO: Hacer que las pelotas no reboten entre ellas porque eso es muy gay
//TODO: Corregir la direccion inicial de disparo de las pelotas 
//TODO: Intentar instanciarlas con velocidad sin acceder al componente de RigidBody2D 