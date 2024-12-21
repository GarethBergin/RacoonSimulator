using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class PlayerSpotlightL : MonoBehaviour
{
    public Light2D spotlight2D; // Change the variable type to Light2D

    void Start()
    {
        spotlight2D = GetComponentInChildren<Light2D>(); // Change the component type

        if (spotlight2D == null)
        {
            Debug.LogError("2D Spotlight not found in children!");
        }
    }

    void Update()
    {
        spotlight2D.transform.rotation = transform.rotation;
    }
}
