using UnityEngine;

public class Bloque : MonoBehaviour {

    int vida;                               //Vida
    TextMesh tm;                            //Texto de vida
   
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
        Pelota p = collision.gameObject.GetComponent<Pelota>();
        
        if (p != null)
        {
          
            vida--;
            
            if (vida == 0) {
                LevelManager.instance.SumaPuntos();
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
