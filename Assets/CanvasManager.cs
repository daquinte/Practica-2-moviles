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

    //Botones
    public Button botonAceptarPago;
    public Button botonCancelarPago;
    public Button botonAceptarGenerico;                 //Acepta la accion para desactivar el panel


    // Use this for initialization
    void Start () {
        sliderPuntuacion.maxValue = LevelManager.instance.puntuacionMaxima;   
    }
	
	// Update is called once per frame
	void Update () {
        //Actualiza puntuacion
        int puntuacionAct = LevelManager.instance.getPuntuacionActual();
        textoPuntuacion.text = "Puntos: " + puntuacionAct;
        sliderPuntuacion.value = puntuacionAct;

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
        
        
        if (!GameManager.instance.RestaRubies(2000))
        {
            botonAceptarPago.gameObject.SetActive(false);
            botonCancelarPago.gameObject.SetActive(false);

            textoPagoInsuficiente.gameObject.SetActive(true);
            botonAceptarGenerico.gameObject.SetActive(true);
        }
        else
        {
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