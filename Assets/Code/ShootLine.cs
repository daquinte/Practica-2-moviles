using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLine : MonoBehaviour {

    public GameObject Spawner;        

    private LineRenderer line;                       

    // Use this for initialization
    void Start()
    {
        // Add a Line Renderer to the GameObject
        line = gameObject.GetComponent<LineRenderer>();        
    }

    // Update is called once per frame
    void Update()
    {

        // Update position of the two vertex of the Line Renderer
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
       
        line.SetPosition(0, mousePos);
        line.SetPosition(1, Spawner.transform.localPosition);
    }

    /// <summary>
    /// Es llamado para poner el estado del gameobject al valor dado
    /// </summary>
    /// <param name="state">Estado</param>
    public void SetLineActive(bool state)
    {
        gameObject.SetActive(state);
    }
}
