using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrganizaNiveles : MonoBehaviour {

    //Boton
    public Button botonNivel;
    private Button botonActual;

    public Canvas canvas;

    //Ints para la organización del menú
    int DesplazamientoX = 160;
    int DesplazamientoY = 90;

    int XAct, YAct = 0;

    int NivelActual = 1;
    int NumBotones = 10; 



	// Use this for initialization

        //TODO: Investigar como colocar los botones en coordenadas del canvas de los huevos
        //Los calculos están más o menos bien pero hay que ajustarlos -> ¿Ajustar el calculo en base al primer botón?
	void Start () {

        for (int i = 0; i < NumBotones/3 +1; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                botonActual = Instantiate(botonNivel,  new Vector3(XAct, YAct, 0), Quaternion.identity, canvas.transform);


                //Parent = BotonesNiveles
                //botonActual.transform.SetParent(gameObject.transform);
                botonActual.transform.localPosition = new Vector3(XAct, YAct, 0);
                botonActual.transform.localScale = new Vector3(3, 3, 1);

                botonActual.name = NivelActual.ToString();
                botonActual.GetComponentInChildren<Text>().text = "Nivel " + NivelActual.ToString();

                botonActual.gameObject.SetActive(true);
                //preguntas si está bloqueado -> gm
                //si está, pones el candado

                //Actualizamos variables
                XAct += DesplazamientoX;
                YAct += DesplazamientoY;

                NivelActual++;

            }

            //Actualizas los valores
            XAct = -160;
            YAct = 90;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
