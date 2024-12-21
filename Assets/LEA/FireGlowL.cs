using UnityEngine;



public class FireGlowL : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D fireLight; // Reference to the Light2D component
    public float minIntensity = 0.5f; // Minimum intensity of the light
    public float maxIntensity = 1.0f; // Maximum intensity of the light
    public float flickerSpeed = 1.0f; // Speed of the flickering effect

    private float targetIntensity; // Target intensity of the light
    private float currentIntensity; // Current intensity of the light

    void Start()
    {
        // Initialize target intensity with a random value between min and max intensity
        targetIntensity = Random.Range(minIntensity, maxIntensity);
        // Set initial intensity of the light
        fireLight.intensity = targetIntensity;
    }

    void Update()
    {
        // Update the current intensity towards the target intensity
        currentIntensity = Mathf.MoveTowards(currentIntensity, targetIntensity, flickerSpeed * Time.deltaTime);

        // Apply the current intensity to the light
        fireLight.intensity = currentIntensity;

        // If the current intensity reaches the target intensity, set a new target intensity
        if (Mathf.Approximately(currentIntensity, targetIntensity))
        {
            // Generate a new random target intensity
            targetIntensity = Random.Range(minIntensity, maxIntensity);
        }
    }
}
