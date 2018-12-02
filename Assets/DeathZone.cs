using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {
    int numMaximoPelotas;           //Para que sepa cuando han llegado todas

    bool llegadaPrimeraPelota;      

	// Use this for initialization
	void Start () {
        llegadaPrimeraPelota = false;
	}
	
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bola")
        {
            if (!llegadaPrimeraPelota)
            {
                llegadaPrimeraPelota = true;

                //avisa al gameManager de la siguiente posicion
                float spawnerX = collision.gameObject.transform.position.x;
                GameManager.instance.SetSpawnerPosition(spawnerX);
            }

            //Notificamos al gameManager de que ha caido una bola 
            //y le decimos que vaya al spawner
            GameManager.instance.LlegadaPelota();
            collision.gameObject.GetComponent<Pelota>().SetVueltaACasa();
        }
    }

    public void ResetPrimeraPelota()
    {
        llegadaPrimeraPelota = false;
    }
}
