using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGlow : MonoBehaviour
{
    public Color color;
    public float maxIntensity = 50f;
    public float minIntensity = 0f;
    public float pulseSpeed = 1f; // Speed = pulses per second
    private Light light;
    private float targetIntensity;
    private float currentIntensity;
    
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        currentIntensity = Mathf.MoveTowards(light.intensity, targetIntensity, Time.deltaTime * pulseSpeed * (maxIntensity - minIntensity));
        
        if (currentIntensity >= maxIntensity)
        {
            currentIntensity = maxIntensity;
            targetIntensity = minIntensity;
        }
        else if (currentIntensity <= minIntensity)
        {
            currentIntensity = minIntensity;
            targetIntensity = maxIntensity;
        }

        light.intensity = currentIntensity;
        light.color = color;
    }
}
