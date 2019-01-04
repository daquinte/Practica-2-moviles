using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    private void OnEnable()
    {
        StartCoroutine(DisableAfterSeconds(0.5f));
    }
    
    /// <summary>
    /// Deshabilita el objeto tras time segundos
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator DisableAfterSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);

        yield break; //Detiene la corroutina
    }
    
}
