using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpLaserVertical : MonoBehaviour {




    public bool Usado;              //Si está usado, en el siguiente turno se destruye
                                    // Use this for initialization
    void Start()
    {
        Usado = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Pelota p = collision.GetComponent<Pelota>();

        if (p != null)
        {
            if (!Usado)
            {
                Usado = true;
                LevelManager.instance.InsertaObjetoParaEliminar(gameObject);
            }
            //Activa el rayo
            Raycast();

            //Activa la parte visual
            Laser Rayo = gameObject.GetComponentInChildren<Laser>(true);
            Rayo.gameObject.SetActive(true);


        }
    }

    /// <summary>
    /// Lanza un rayo en ambas direcciones, derecha e izquierda, respecto al objeto
    /// ,toma la lista de objetos y a los bloques que hay en ella les resta vida.
    /// </summary>
    private void Raycast()
    {
        RaycastHit2D[] raycastHit2D_Arriba = Physics2D.RaycastAll(transform.position, Vector2.up);
        RaycastHit2D[] raycastHit2D_Abajo = Physics2D.RaycastAll(transform.position, Vector2.down);

        foreach (RaycastHit2D go in raycastHit2D_Arriba)
        {
            Bloque b = go.collider.gameObject.GetComponent<Bloque>();
            if (b != null)
            {
                b.RestaVida();
            }
        }

        foreach (RaycastHit2D go in raycastHit2D_Abajo)
        {
            Bloque b = go.collider.gameObject.GetComponent<Bloque>();
            if (b != null)
            {
                b.RestaVida();
            }
        }
    }



}
