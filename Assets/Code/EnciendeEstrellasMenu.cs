using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Esta clase se encarga de encender las estrellas desbloqueadas por cada nivel
/// </summary>
public class EnciendeEstrellasMenu : MonoBehaviour {
    
    public Image EstrellaBase;
    public Sprite estrellaEncendida;

    private Image[] estrellas;

    // Use this for initialization
    void Init () {
        estrellas = new Image[3];
        for (int i = 0; i < 3; i++)
        {
            Image estrellaAux = Instantiate(EstrellaBase, transform);
            estrellaAux.transform.localPosition += new Vector3(40 * i, 0, 0);
            estrellas[i] = estrellaAux;
            Debug.Log(estrellas[i]);

        }
    }

    /// <summary>
    /// Establece que se enciendan nEstrellas encima del nivel
    /// </summary>
    /// <param name="nEstrellas"></param>
    public void EnciendeEstrellas(int nEstrellas)
    {
        if (estrellas == null) Init();

        for(int i = 0; i < nEstrellas; i++)
        {
            estrellas[i].sprite = estrellaEncendida;
        }
    }
}
