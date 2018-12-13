﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {
    int numMaximoPelotas;           //Para que sepa cuando han llegado todas

    bool llegadaPrimeraPelota;

    // Use this for initialization
    void Start() {
        llegadaPrimeraPelota = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Pelota p = collision.GetComponent<Pelota>();
        if (p != null)
        {
            if (!llegadaPrimeraPelota)
            {
                llegadaPrimeraPelota = true;

                //avisa al LevelManager de la siguiente posicion
                float spawnerX = collision.gameObject.transform.position.x;
                LevelManager.instance.SetSpawnerPosition(spawnerX);
            }

            //Notificamos al LevelManager de que ha caido una bola 
            //y le decimos que vaya al spawner

            collision.gameObject.GetComponent<Pelota>().SetVueltaACasa(LevelManager.instance.llegada);
        }
    }

    

    public void ResetPrimeraPelota()
    {
        llegadaPrimeraPelota = false;
    }
}
