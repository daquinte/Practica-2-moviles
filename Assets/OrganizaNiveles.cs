using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrganizaNiveles : MonoBehaviour {

    //Boton
    public Button botonNivel;               //Prefab del botón. Contiene la primera posición.

    public Canvas canvas;                   //Canvas del que son hijos todos los botones
    public GameObject BotonesNiveles;
    public GameObject PanelColeccionables;    //Panel de objetivos


    //Ints para la organización del menú
    private float DesplazamientoX = 160.0f;
    private float DesplazamientoY = 90.0f;

    private float DesplazamientoFila = 180.0f;           //Desplazamiento que hacemos cada 3 iteraciones
    private float DesplazamientoFilaActual = 0.0f;

    private int NivelActual = 1;

    public int NumFilas = 3; 



	// Use this for initialization
    
	void Start () {
        
        for (float i = 0; i < NumFilas; i++)
        {
            DesplazamientoFilaActual = DesplazamientoFila * i;
          

            for (float j = 0; j < 3; j++)
            {
               Button boton = Instantiate(botonNivel, BotonesNiveles.transform);


                //La posicion del botón viene dada por la fila en la que está, que es un movimiento en Y,
                //y por lo lejos que está del botón inicial de la fila, que es un movimiento en X e Y
                boton.transform.position += new Vector3(0, DesplazamientoFilaActual * 0.8f, 0);
                //0.4f en ambos para windowed
                boton.transform.position += new Vector3(DesplazamientoX * j  * 0.8f, DesplazamientoY * j * 0.8f, 0);

                
                //Ponemos la escala
                boton.transform.localScale = new Vector3(3, 3, 1);

                boton.name = NivelActual.ToString();
                boton.GetComponentInChildren<Text>().text = "Nivel " + NivelActual.ToString();

                boton.gameObject.SetActive(true);

                //preguntas si está bloqueado
                //si está, pones el candado
                if (!GameManager.instance.NivelDesbloqueado(NivelActual))
                {
                    boton.interactable = false; 
                    StartCoroutine(FadeImage( true, boton.GetComponent<Image>()));
                }

 
                NivelActual++;
               
            }
        }

        //Movemos el panel de coleccionable
        //* 0.8f para windowed
        PanelColeccionables.transform.position += new Vector3(0, DesplazamientoFilaActual * 1.7f, 0);


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
