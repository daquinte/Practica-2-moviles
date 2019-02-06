using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class canvasMenu : MonoBehaviour {

    //Botones menú
    public GameObject niveles;
    public GameObject botonesNiveles;
    public Text titulo;
    public Text estrellas;

    private void Update()
    {
        estrellas.text = "Estrellas totales: " + GameManager.instance.GetEstrellas();
    }


    public void Carga_Nivel()
    {
        int nivel = int.Parse(EventSystem.current.currentSelectedGameObject.name);

        GameManager.instance.CargaNivel(nivel);

    }

    /// <summary>
    /// Hace que los botones de los niveles sean visibles
    /// </summary>
    public void Botones_Niveles()
    {
        niveles.SetActive(false);
        titulo.gameObject.SetActive(false);
        botonesNiveles.SetActive(true);
    }

    /// <summary>
    /// Vuelve a la pantalla de título desde la selección de niveles
    /// </summary>
    public void VolverAtras()
    {
        botonesNiveles.SetActive(false);
        niveles.SetActive(true);
        titulo.gameObject.SetActive(true);
    }

}
