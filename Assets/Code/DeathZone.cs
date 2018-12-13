using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Pelota p = collision.GetComponent<Pelota>();
        if (p != null)
        {
            //Notificamos al Levelmanager que ha llegado una pelota
            LevelManager.instance.LlegadaPelota(p);
        }
    }

}
