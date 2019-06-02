using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnciendeEstrellasMenu : MonoBehaviour {


    
    public GameObject EstrellaBase;
    Sprite estrellaEncendida;

    private GameObject[] estrellas = new GameObject[3];

    // Use this for initialization
    void Start () {
        for(int i = 0; i < 3; i++)
        {
            GameObject estrellaAux = EstrellaBase;
            estrellaAux.transform.localPosition += new Vector3(40, 0, 0);
            estrellas[i] = estrellaAux;
        }
	}

    public void EnciendeEstrellas(int nEstrellas)
    {
        for(int i = 0; i < nEstrellas; i++)
        {
            estrellas[i].GetComponent<Sprite>();
        }
    }
}
