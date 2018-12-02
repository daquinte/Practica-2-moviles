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
       
       line.SetPosition(0, new Vector2(Input.mousePosition.x, Input.mousePosition.y).normalized);
       line.SetPosition(1, Spawner.transform.position);
    }
}
