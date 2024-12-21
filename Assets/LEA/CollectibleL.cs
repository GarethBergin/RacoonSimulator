using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class Collectible : MonoBehaviour
{
    public Light2D playerSpotlight;
    public float spotlightExpansionAmount = 2.0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ExpandSpotlight();
            Destroy(gameObject);
        }
    }

    private void ExpandSpotlight()
    {
        if (playerSpotlight != null)
        {
            playerSpotlight.pointLightOuterRadius += spotlightExpansionAmount;
        }
    }
}
