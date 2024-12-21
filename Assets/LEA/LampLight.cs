using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class LampLight : MonoBehaviour
{
    public Light2D spotlight;
    public float minIntensity = 0.5f;
    public float maxIntensity = 1f;
    public float minOuterRadius = 1f;
    public float maxOuterRadius = 2f;
    public float pulseSpeed = 1f;
    public float flickerSpeed = 5f;

    private float currentIntensity;
    private float currentOuterRadius;
    private bool increasing = true;

    void Start()
    {
        // Initialize the initial intensity and outer radius
        currentIntensity = Random.Range(minIntensity, maxIntensity);
        currentOuterRadius = Random.Range(minOuterRadius, maxOuterRadius);
    }

    void Update()
    {
        // Apply pulsating animation to intensity and outer radius
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

        // Apply flickering effect
        currentIntensity += Mathf.Lerp(-flickerSpeed, flickerSpeed, Mathf.PerlinNoise(Time.time, Time.time));
        currentOuterRadius += Mathf.Lerp(-flickerSpeed, flickerSpeed, Mathf.PerlinNoise(Time.time, Time.time));

        // Clamp intensity and outer radius to their min and max values
        currentIntensity = Mathf.Clamp(currentIntensity, minIntensity, maxIntensity);
        currentOuterRadius = Mathf.Clamp(currentOuterRadius, minOuterRadius, maxOuterRadius);

        // Apply the intensity and outer radius to the spotlight
        spotlight.intensity = currentIntensity;
        spotlight.pointLightOuterRadius = currentOuterRadius;
    }
}
