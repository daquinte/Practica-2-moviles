using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject PelotaPrefab;

    public int numBolasSpawner;

    int bolasAct;
    bool puedeInstanciar;

	// Use this for initialization
	void Start () {
        puedeInstanciar = true;

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton(0) && puedeInstanciar)
        {
            StartCoroutine(InstanciaPelota());
        }

        if (numBolasSpawner == bolasAct)
            StopCoroutine(InstanciaPelota());

    }

    IEnumerator InstanciaPelota()
    {
        GameObject pelotaAux = Instantiate(PelotaPrefab, transform.position, Quaternion.identity);

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;     //2D
        Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
        objectPos.z = 0;    //2D

        Vector3 dir = (mousePos - objectPos).normalized;
        /*
        Vector3 touchPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        Vector3 dir = (touchPos - transform.position).normalized;
        */
        pelotaAux.GetComponent<Pelota>().LaunchBall(dir); //¿¿Como hacemos esto??
        bolasAct++;

        yield return new WaitForSeconds(10);
    }
}
