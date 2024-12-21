using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Objectpulse : MonoBehaviour
{
    public Light2D spotlight;
    public float minIntensity = 0.5f;
    public float maxIntensity = 1f;
    public float minOuterRadius = 1f;
    public float maxOuterRadius = 2f;
    public float pulseSpeed = 1f;

    private float currentIntensity;
    private float currentOuterRadius;
    private bool increasing = true;

    void Start()
    {
        currentIntensity = minIntensity;
        currentOuterRadius = minOuterRadius;
    }

    void Update()
    {
        if (increasing)
        {
            currentIntensity += pulseSpeed * Time.deltaTime;
            currentOuterRadius += pulseSpeed * Time.deltaTime;
            if (currentIntensity >= maxIntensity)
                increasing = false;
        }
        else
        {
            currentIntensity -= pulseSpeed * Time.deltaTime;
            currentOuterRadius -= pulseSpeed * Time.deltaTime;
            if (currentIntensity <= minIntensity)
                increasing = true;
        }

        spotlight.intensity = currentIntensity;
        spotlight.pointLightOuterRadius = currentOuterRadius;
    }
}
