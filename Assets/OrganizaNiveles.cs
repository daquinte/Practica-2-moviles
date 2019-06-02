using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrganizaNiveles : MonoBehaviour
{

    //Boton
    public GameObject botonNivel;                           //Prefab del botón. Contiene la primera posición.

    public Canvas canvas;                               //Canvas del que son hijos todos los botones
    public GameObject PanelColeccionables;              //Panel de objetivos


    //Ints para la organización del menú
    private float DesplazamientoX = 160.0f;
    private float DesplazamientoY = 90.0f;

    private float DesplazamientoFila = 180.0f;           //Desplazamiento que hacemos cada 3 iteraciones
    private float DesplazamientoFilaActual = 0.0f;

    private EnciendeEstrellasMenu estrellasMenu;             //Clase que se encarga de encender estrellas


    private int NivelActual = 1;

    public int NumFilas = 3;



    // Use this for initialization

    void Start()
    {
        for (float i = 0; i < NumFilas; i++)
        {
            DesplazamientoFilaActual = DesplazamientoFila * i;
            for (float j = 0; j < 3; j++)
            {
                GameObject boton = Instantiate(botonNivel, gameObject.transform);

                //La posicion del botón viene dada por la fila en la que está, que es un movimiento en Y,
                //y por lo lejos que está del botón inicial de la fila, que es un movimiento en X e Y
                boton.transform.localPosition += new Vector3(DesplazamientoX * j, DesplazamientoY * j + DesplazamientoFilaActual, 0);
                boton.name = NivelActual.ToString();
                boton.GetComponentInChildren<Text>().text = "Nivel " + NivelActual.ToString();
                boton.gameObject.SetActive(true);

                //preguntas si está bloqueado
                //si está, pones el candado
                if (!GameManager.instance.NivelDesbloqueado(NivelActual))
                {
                    boton.GetComponent<Button>().interactable = false;
                    StartCoroutine(FadeImage(true, boton.GetComponent<Image>()));
                }
                //Si no está bloqueado, miras a ver cuantas estrellas tiene
                else
                {
                    Debug.Log("pium");
                    estrellasMenu = boton.GetComponentInChildren<EnciendeEstrellasMenu>();
                    if (estrellasMenu != null)
                    {
                        Debug.Log("PINTAESTRELLAS");
                        int estrellas = GameManager.instance.GetEstrellasDelNivel(NivelActual);
                        estrellasMenu.EnciendeEstrellas(estrellas);
                    }
                }


                NivelActual++;

            }
        }

        //Movemos el panel de coleccionable
        //* 0.8f para windowed
        //PanelColeccionables.transform.position += new Vector3(0, DesplazamientoFilaActual * 1.7f, 0);


    }

    /// <summary>
    /// Corroutine that fades an object in or out
    /// </summary>
    /// <param name="fadeAway">True if out, false if in</param>
    /// <param name="img">image del objeto</param>
    /// <returns></returns>
    IEnumerator FadeImage(bool fadeAway, Image img)
    {

        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0.5f; i -= Time.deltaTime * 0.4f)
            {
                // set color with i as alpha
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0.5f; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }
}
