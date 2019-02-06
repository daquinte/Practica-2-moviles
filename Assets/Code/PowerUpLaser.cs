using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum TipoLaser { Vertical, Horizontal };

public class PowerUpLaser : MonoBehaviour {

    bool Usado;              //Si está usado, en el siguiente turno se destruye
    TipoLaser Tipo;          //Tipo del laser

    // Use this for initialization
    void Start () {
        Usado = false;
        AsignaTipo();
	}

    void AsignaTipo()
    {
        if (gameObject.tag == "PowerUpLaserVertical")
        {
            Tipo = TipoLaser.Vertical;
        }

        else if (gameObject.tag == "PowerUpLaserHorizontal")
        {
            Tipo = TipoLaser.Horizontal;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Pelota p = collision.GetComponent<Pelota>();

        if (p != null)
        {
            if (!Usado)
            {
                Usado = true;
                LevelManager.instance.RestaPowerUp(gameObject);
            }

            //Activa el rayo en función de si es horizontal o vertical
            switch (Tipo)
            {
                case TipoLaser.Vertical:
                    RaycastVertical();
                    break;

                case TipoLaser.Horizontal:
                    RaycastHorizontal();
                    break;
            }

            //Activa la parte visual
            Laser Rayo = gameObject.GetComponentInChildren<Laser>(true);
            Rayo.gameObject.SetActive(true);


        }
    }


    /// <summary>
    /// Lanza un rayo en ambas direcciones, arriba y abajo, respecto al objeto,
    /// toma la lista de objetos y a los bloques que hay en ella les resta vida.
    /// </summary>
    private void RaycastVertical()
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

    /// <summary>
    /// Lanza un rayo en ambas direcciones, derecha e izquierda, respecto al objeto,
    /// toma la lista de objetos y a los bloques que hay en ella les resta vida.
    /// </summary>
    private void RaycastHorizontal()
    {
        RaycastHit2D[] raycastHit2D_Izquierda = Physics2D.RaycastAll(transform.position, Vector2.left);
        RaycastHit2D[] raycastHit2D_Derecha = Physics2D.RaycastAll(transform.position, Vector2.right);

        foreach (RaycastHit2D go in raycastHit2D_Izquierda)
        {
            Bloque b = go.collider.gameObject.GetComponent<Bloque>();
            if (b != null)
            {
                b.RestaVida();
            }
        }

        foreach (RaycastHit2D go in raycastHit2D_Derecha)
        {
            Bloque b = go.collider.gameObject.GetComponent<Bloque>();
            if (b != null)
            {
                b.RestaVida();
            }
        }
    }

}
