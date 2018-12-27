using UnityEngine;

public class Bloque : MonoBehaviour {

    int vida;
    TextMesh tm;

    // Use this for initialization
    void Start () {
        tm = GetComponentInChildren<TextMesh>();
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

                LevelManager.instance.RestaBloque(this);
            }
        }
    }

    public void ConfiguraBloque(int x, int y, int vida)
    {
        Vector3 posicion = new Vector3(x, y, 0);
        this.vida = vida;

        transform.position = posicion;
    }
}
