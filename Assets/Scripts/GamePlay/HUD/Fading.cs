using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fading : MonoBehaviour
{
    //the actual black screen that will be used
    public CanvasGroup fade;
    
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;

    //if the screen is fading out
    public bool inToOut;
    //if the screen if fading in
    public bool OutToIn;

    //controls how long the fade will last
    float m_Timer;
    //controls the transparency of the screen
    double m_counter = 1;

    void Start()
    {
        //fading out
        inToOut = true;
        OutToIn = false;
    }

    
    void Update()
    {
        if(inToOut) //fading out from black
        {
            m_counter -= 0.01;
            fade.alpha = Convert.ToSingle(m_counter);
            //print(fade.alpha);
            //print(m_counter);
            if(m_counter <= 0) //fade out is finished
            {
                inToOut = false;
                m_counter = 1;
                //print(m_counter);
            }
        }

        if(OutToIn) //fading in to black
        {
            m_Timer += Time.deltaTime;
            fade.alpha = m_Timer / fadeDuration;
            //print(fade.alpha);
            if(m_Timer > fadeDuration + displayImageDuration)
            {
                OutToIn = false;
                m_Timer = 0;
            }
        }
    }


    public void Fade(bool fadingIn, bool fadingOut)
    {
        OutToIn = fadingIn;
        inToOut = fadingOut;
    }
}
