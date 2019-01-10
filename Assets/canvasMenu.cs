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

    public Text estrellitas;

    /*private void Update()
    {
        estrellitas.text = "Estrellas en el nivel 0: " + GameManager.instance.estrellasPorNivel[0];
    }*/

    public void Carga_Nivel()
    {
        int nivel = int.Parse(EventSystem.current.currentSelectedGameObject.name);

        GameManager.instance.CargaNivel(nivel);

    }

    /// <summary>
    /// Hace ue los botones d elos niveles sean visibles
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
