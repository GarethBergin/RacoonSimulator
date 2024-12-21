using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class SpotlightPulseL : MonoBehaviour
{
    public Light2D spotlight;
    public float minIntensity = 0.5f;
    public float maxIntensity = 1f;
    public float minOuterRadius = 0.3f;
    public float maxOuterRadius = 0.5f;
    public float pulseSpeed = 1f;
    public float blinkDuration = 0.1f;
    public float fastBlinkInterval = 0.5f;
    public int fastBlinkCount = 2;
    public float slowBlinkInterval = 10f;

    private float currentIntensity;
    private float currentOuterRadius;
    private bool increasing = true;
    private bool isBlinking = false;
    private float nextBlinkTime;
    private float currentTime;
    private int blinkCounter = 0;

    void Start()
    {
        currentIntensity = minIntensity;
        currentOuterRadius = minOuterRadius;
        nextBlinkTime = Time.time + slowBlinkInterval;
        currentTime = Time.time;
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (Time.time >= nextBlinkTime)
        {
            isBlinking = true;
            nextBlinkTime = Time.time + (isBlinking ? blinkDuration : fastBlinkInterval);
            blinkCounter++;
        }

        if (isBlinking)
        {
            spotlight.enabled = !spotlight.enabled;
            isBlinking = false;

            if (blinkCounter >= fastBlinkCount)
            {
                blinkCounter = 0;
                nextBlinkTime = currentTime + slowBlinkInterval;
            }
        }

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
        spotlight.pointLightOuterRadius = Mathf.Clamp(currentOuterRadius, minOuterRadius, maxOuterRadius);
    }
}
