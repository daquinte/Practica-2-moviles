using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {
   
    public Slider sliderPuntuacion;
    public GameObject panel;

    //Textos
    public Text textoPuntuacion;
    public Text textoPago;
    public Text textoPagoInsuficiente;
    public Text textoPelotas;

    //Botones
    public Button botonAceptarPago;
    public Button botonCancelarPago;
    public Button botonAceptarGenerico;                 //Acepta la accion para desactivar el panel

    //Estrellas
    public GameObject estrellaBase;
    public GameObject estrellaMedio;
    public GameObject estrellaFinal;

    //Sprites
    public Sprite estrellaConseguida;

    // Use this for initialization
    void Start () {
        sliderPuntuacion.maxValue = LevelManager.instance.puntuacionMaxima;   
    }
	
	// Update is called once per frame
	void Update () {
        //Actualiza puntuacion
        int puntuacionAct = LevelManager.instance.GetPuntuacionActual();
        EvaluaPuntuacion(puntuacionAct);
        textoPuntuacion.text = "Puntos: " + puntuacionAct;
        sliderPuntuacion.value = puntuacionAct;

        int pelotasActuales = LevelManager.instance.GetPelotasSpawner();
        textoPelotas.text = "Pelotas: " + pelotasActuales;
    }

    /// <summary>
    /// Enciende las estrellas en función de la puntuación obtenida
    /// </summary>
    private void EvaluaPuntuacion(int puntuacion)
    {
        if(puntuacion >= 1 && estrellaBase.GetComponent<Image>().sprite != estrellaConseguida)
        {
            estrellaBase.GetComponent<Image>().sprite = estrellaConseguida;
        }
        //La puntuacion de la mitad la guardas aqui y la sacas de un get del LevelManager
        if (puntuacion >= 100 && estrellaMedio.GetComponent<Image>().sprite != estrellaConseguida)
        {
            estrellaMedio.GetComponent<Image>().sprite = estrellaConseguida;
        }

        if (puntuacion >= 200 && estrellaFinal.GetComponent<Image>().sprite != estrellaConseguida)
        {
            estrellaFinal.GetComponent<Image>().sprite = estrellaConseguida;
        }

    }

    /// <summary>
    /// Establece el panel a los valores por defecto,
    /// tiene que ser llamado cuando quieras quitar los popup en "Panel_Cancelar" 
    /// y reanudar la partida
    /// </summary>
    private void SetPanelToDefault()
    {
        textoPagoInsuficiente.gameObject.SetActive(false);
        botonAceptarGenerico.gameObject.SetActive(false);
        botonAceptarPago.gameObject.SetActive(true);
        botonCancelarPago.gameObject.SetActive(true);

    }


    public void Panel_Confirmacion()
    {
        panel.SetActive(true);
        LevelManager.instance.Pausa = true;
    }

    public void Panel_Aceptar()
    {
        
        
        if (!GameManager.instance.RestaRubies(1))
        {
            botonAceptarPago.gameObject.SetActive(false);
            botonCancelarPago.gameObject.SetActive(false);

            textoPagoInsuficiente.gameObject.SetActive(true);
            botonAceptarGenerico.gameObject.SetActive(true);
        }
        else
        {
            //LLamar al levelManager para que active el power up

            LevelManager.instance.ColocaPowerUpLasers(4);
            panel.SetActive(false);
            LevelManager.instance.Pausa = false;
        }
    }

    public void Panel_Cancelar()
    {
        SetPanelToDefault();
        panel.SetActive(false);
        LevelManager.instance.Pausa = false;
    }
}