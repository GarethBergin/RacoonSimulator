using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class CollectableItemJ : MonoBehaviour
{
    public ItemManager im;
    public float spotlightRadiusIncrease = 1f;
    public GameObject player;
    public GameObject interactionPrompt; // Reference to the 2D GameObject representing the "E" key

    private bool isCollected = false;
    private bool inRange = false;

    public Text fireflyText;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player") && !isCollected)
        {
            inRange = true;
            ShowInteractionPrompt(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            inRange = false;
            ShowInteractionPrompt(false);
        }
    }

    private void Update()
    {
        if (isCollected)
            return;

        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            CollectItem();
        }
    }

    void CollectItem()
    {
        isCollected = true;
        IncreasePlayerSpotlightRadius();
        im.flyCount++;
        Debug.Log("Item collected." + im.flyCount);
        fireflyText.text = im.flyCount.ToString();
        Debug.Log("Item collected.");
        Destroy(gameObject); // Destroy the collectable item after it's collected
    }

    void IncreasePlayerSpotlightRadius()
    {
        SpotlightPulse playerSpotlight = player.GetComponentInChildren<SpotlightPulse>();
        if (playerSpotlight != null)
        {
            // Increase min and max outer radius
            playerSpotlight.minOuterRadius += spotlightRadiusIncrease;
            playerSpotlight.maxOuterRadius += spotlightRadiusIncrease;

            Debug.Log("Min Outer Radius: " + playerSpotlight.minOuterRadius);
            Debug.Log("Max Outer Radius: " + playerSpotlight.maxOuterRadius);
        }
        else
        {
            Debug.LogWarning("Player's spotlight not found!");
        }
    }

    void ShowInteractionPrompt(bool show)
    {
        interactionPrompt.SetActive(show);
    }
}
