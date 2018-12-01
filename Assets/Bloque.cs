using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloque : MonoBehaviour {

    public int vida;
    TextMesh tm;

    // Use this for initialization
    void Start () {
        tm = GetComponentInChildren<TextMesh>();
        tm.text = vida.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        tm.text = vida.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Bola")
        {
          
            vida--;
            
            if (vida == 0) {
                
                Destroy(gameObject);
            }
        }
    }
}
