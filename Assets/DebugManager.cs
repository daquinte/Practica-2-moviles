using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {

        if (GUI.Button(new Rect(10, 100, 100, 30), "Suma Estrellas"))
        {
            GameManager.instance.Estrellas += 10;
        }
        if (GUI.Button(new Rect(10, 140, 100, 30), "Resta Estrellas"))
        {
            GameManager.instance.Estrellas -= 10;
        }
        if (GUI.Button(new Rect(250, 100, 100, 30), "Suma Rubies"))
        {
            GameManager.instance.Rubies += 10;
        }
        if (GUI.Button(new Rect(250, 140, 100, 30), "Resta Rubies"))
        {
            GameManager.instance.Rubies -= 10;
        }

    }
}
