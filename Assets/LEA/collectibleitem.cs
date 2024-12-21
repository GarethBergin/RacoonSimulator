using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class CollectableItemL : MonoBehaviour
{
    public float spotlightRadiusIncrease = 1f;
    public GameObject player;
    public GameObject interactionPrompt;
    public ItemManager im;
    public Text fireflyText;

    private bool isCollected = false;
    private bool inRange = false;

    // Reference to the enemy movement script
    public EnemyMovement enemyMovement;

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
        Debug.Log("Item collected.");
        im.flyCount++;
        Debug.Log("Items : " + im.flyCount);
        fireflyText.text = im.flyCount.ToString();

        // Check if the player has collected 4 fireflies
        if (im.flyCount >= 4)
        {
            // Trigger the enemy to run away
            if (enemyMovement != null)
            {
                enemyMovement.RunAway();
            }
            else
            {
                Debug.LogWarning("EnemyMovement script reference is not assigned.");
            }
        }

        Destroy(gameObject);
    }

    void IncreasePlayerSpotlightRadius()
    {
        SpotlightPulse playerSpotlight = player.GetComponentInChildren<SpotlightPulse>();
        if (playerSpotlight != null)
        {
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
