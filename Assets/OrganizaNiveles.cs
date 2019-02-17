using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrganizaNiveles : MonoBehaviour {

    //Boton
    public Button botonNivel;               //Prefab del botón. Contiene la primera posición.
    private Button botonActual;             //Boton auxiliar

    public Canvas canvas;                   //Canvas del que son hijos todos los botones

    //Ints para la organización del menú
    private float DesplazamientoX = 160;
    private float DesplazamientoY = 90;

    private float DesplazamientoFila = 180;           //Desplazamiento que hacemos cada 3 iteraciones
    private float DesplazamientoFilaActual;

    int NivelActual = 1;
    int NumBotones = 10; 



	// Use this for initialization

        //TODO: Investigar como colocar los botones en coordenadas del canvas de los huevos
        //Los calculos están más o menos bien pero hay que ajustarlos -> ¿Ajustar el calculo en base al primer botón?
	void Start () {

        Debug.Log("Testing para los primeros 3 niveles");
        for (float i = 0; i < 2; i++)
        {
            DesplazamientoFilaActual = DesplazamientoFila * i;
            for (float j = 0; j < 3; j++)
            {
                botonActual = Instantiate(botonNivel, canvas.transform);

                //La posicion del botón viene dada por la fila en la que está, que es un movimiento en Y,
                //y por lo lejos que está del botón inicial de la fila, que es un movimiento en X e Y
                Debug.Log("Posicion del boton original: " + botonActual.transform.position);

                botonActual.transform.position += new Vector3(0, DesplazamientoFilaActual, 0);
                Debug.Log("Posicion del boton tras fila: " + botonActual.transform.position);

                botonActual.transform.position += new Vector3(DesplazamientoX * j * 1.5f, DesplazamientoY * j * 1.5f, 0);

                Debug.Log("Posicion del boton general: " + botonActual.transform.position);
                
                //Ponemos la escala
                botonActual.transform.localScale = new Vector3(3, 3, 1);

                botonActual.name = NivelActual.ToString();
                botonActual.GetComponentInChildren<Text>().text = "Nivel " + NivelActual.ToString();

                botonActual.gameObject.SetActive(true);

                //preguntas si está bloqueado -> gm
                //si está, pones el candado
 
                NivelActual++;

            }
        }
	}
}
