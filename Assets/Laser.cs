using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(DestroyAfterSeconds(100));
	}
	
    /// <summary>
    /// Destruye el objeto tras time segundos
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
	IEnumerator DestroyAfterSeconds(int time)
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);

        yield break; //Detiene la corroutina
    }
    
}
