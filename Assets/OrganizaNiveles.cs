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

        //TODO: INVESTIGAR POR QUÉ SE ROMPIÓ EL PUTO DESPLAZAMIENTO
	void Start () {
        
        for (float i = 0; i < NumFilas; i++)
        {
            DesplazamientoFilaActual = DesplazamientoFila * i;
          

            for (float j = 0; j < 3; j++)
            {
               Button boton = Instantiate(botonNivel, BotonesNiveles.transform);
                Debug.Log(boton.transform.position);

                //La posicion del botón viene dada por la fila en la que está, que es un movimiento en Y,
                //y por lo lejos que está del botón inicial de la fila, que es un movimiento en X e Y
                boton.transform.position += new Vector3(0, DesplazamientoFilaActual * 0.4f, 0);

                boton.transform.position += new Vector3(DesplazamientoX * j  *0.4f, DesplazamientoY * j * 0.4f, 0);

                
                //Ponemos la escala
                boton.transform.localScale = new Vector3(3, 3, 1);

                boton.name = NivelActual.ToString();
                boton.GetComponentInChildren<Text>().text = "Nivel " + NivelActual.ToString();

                boton.gameObject.SetActive(true);

                //preguntas si está bloqueado -> gm
                //si está, pones el candado
 
                NivelActual++;
                Debug.Log("soy un boton y mi posicion es " + boton.transform.position);
            }
        }

        //Movemos el panel de coleccionable
        PanelColeccionables.transform.position += new Vector3(0, DesplazamientoFilaActual * 0.8f, 0);


	}
}
