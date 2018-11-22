using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pelota : MonoBehaviour {

    public GameObject spawner;

    #region Atributos
    public float velocidad;

    bool vueltaACasa;
    #endregion

    // Use this for initialization
    void Start () {
        vueltaACasa= false;
        GetComponent<Rigidbody2D>().velocity = new Vector2(5, 5);
    }
	
	// Update is called once per frame
	void Update () {
        
        if (!vueltaACasa) {


            //transform.position += new Vector3((velocidad * Time.deltaTime), 0.0f, 0.0f);
            //HAY QUE USAR VELOCITY
            //Vector2 fuerza = new Vector2((velocidad * Time.deltaTime), (velocidad * Time.deltaTime));
            

        }
        //Calculas para ir hacia el centro
        else
        {
            float speed = 3.0f * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, spawner.transform.position, speed);
        }
       
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       /* switch (collision.gameObject.tag)
        {
            case "MuroLateral":
                velPelotaX *= -1;
                break;
            case "MuroSuperior":
                velPelotaY *= -1;
                break;
         
            case "Limite":
                vueltaACasa = true;
                break;

            case "Bloque":
                //Llamar al bloque para que reste sus puntos
                //collision.getComponent<Bloque>().restaPuntos( 1 || losPuntosQueSean);
                break;
        }
        */
    }

}
