using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Cuando se aplica esta clase a un objeto con compente de Imagen
/// Este parpadeará tras N segundos para llamar la atencion del usuario
/// </summary>
/// 

[RequireComponent(typeof(Image))]


public class FadeSprite : MonoBehaviour {

    public float minimum = 0.0f;
    public float maximum = 1f;
    public float duration = 5.0f;

    //private float startTime;

    bool fade = false;
    public Image sprite = null; 

    void Start()
    {
        sprite = GetComponent<Image>();

        Fade();
    }

    /// <summary>
    /// Detiene el fading por donde estuviera y pone el alpha a 0 para reiniciar
    /// el fading de 0.
    /// </summary>
    /// <param name="newFade"></param>
    public void resetFading(bool newFade)
    {
        StopCorroutine(Fade());
        sprite.color = new Color(1f, 1f, 1f, 0);
        fade = false;

        StartCorroutine(waitForSeconds(5));
        fade = true;
    }

    /// <summary>
    /// Corroutina de Fadein y fade out
    /// </summary>
    /// <returns></returns>
    private IEnumerator Fade()
    {
        StartCorroutine(waitForSeconds(10));

        ////
        //Fade in
        ////
        if (fade)
        {
            //float t = (Time.time - startTime) / duration;
            for(float i = 1; i >= 0; i += Time.deltaTime) { 
                sprite.color = new Color(1f, 1f, 1f, i);
                yield return null;
            }

            fade = false;
        }
        ////
        //Fade out
        ////
        else
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                sprite.color = new Color(1f, 1f, 1f, i);
                yield return null;
            }

            fade = true;
        }

    }



    //Herramientas
    private IEnumerator waitForSeconds(int seconds)
    {
        yield return new waitForSeconds(seconds);

    }


    /*
     * Referencia lmao
    private IEnumerator FadeIn()
    {
        float t = (Time.time - startTime) / duration;
        sprite.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(minimum, maximum, t));

    }*/



}
