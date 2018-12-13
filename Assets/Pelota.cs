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

    public void LaunchBall(Vector2 dir)
    {
        GetComponent<Rigidbody2D>().velocity = dir * velocidad * Time.deltaTime;
    }

    public void SetVueltaACasa(GameManager.LLegadaPelota callback = null)
    {
        vueltaACasa = true;
     
        
        GetComponent<CircleCollider2D>().isTrigger = true; //Ignora colisiones con los bloques
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        StartCoroutine(VueltaACasa(callback));
    }

    private IEnumerator VueltaACasa(GameManager.LLegadaPelota callback = null)
    {
        while (vueltaACasa)
        {
            transform.position = Vector3.MoveTowards(transform.position, posicionOriginal, 10.0f * Time.deltaTime);
            if (transform.position == posicionOriginal) {
                vueltaACasa = false;
                callback.Invoke();
                Destroy(gameObject);

            }

            yield return null;
        }

       
    }

}

