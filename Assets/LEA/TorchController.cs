using UnityEngine;


public class TorchController : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D torchLight;
    public UnityEngine.Rendering.Universal.Light2D spotlight;
    public float minIntensity = 0.5f;
    public float maxIntensity = 1f;
    public float pulseSpeed = 1f;

    private bool playerInsideZone = false;
    private bool isAnimating = false;
    private float currentIntensity;
    private bool increasing = true;

    void Start()
    {
        currentIntensity = minIntensity;
        torchLight.intensity = minIntensity;
        spotlight.intensity = 0f;
        isAnimating = false;
    }

    void Update()
    {
        if (playerInsideZone)
        {
            if (!isAnimating)
            {
                isAnimating = true;
                currentIntensity = minIntensity;
            }

            if (increasing)
            {
                currentIntensity += pulseSpeed * Time.deltaTime;
                if (currentIntensity >= maxIntensity)
                    increasing = false;
            }
            else
            {
                currentIntensity -= pulseSpeed * Time.deltaTime;
                if (currentIntensity <= minIntensity)
                    increasing = true;
            }

            torchLight.intensity = currentIntensity;

            spotlight.intensity = maxIntensity;
        }
        else
        {
            isAnimating = false;
            spotlight.intensity = 0f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            playerInsideZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            playerInsideZone = false;
        }
    }
}
