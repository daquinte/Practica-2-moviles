using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpLaser : MonoBehaviour {



    public bool Usado;              //Si está usado, en el siguiente turno se destruye
	// Use this for initialization
	void Start () {
        Usado = false;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Pelota p = collision.GetComponent<Pelota>();

        if(p != null)
        {
            Usado = true;

            Laser Rayo = gameObject.GetComponentInChildren<Laser>();
            
        }
    }

}
