using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class CollectableItem : MonoBehaviour
{
    public ItemManager im;
    public float spotlightRadiusIncrease = 1f;
    public GameObject player;
    public GameObject interactionPrompt; // Reference to the 2D GameObject representing the "E" key

    private bool isCollected = false;
    private bool inRange = false;
    public Text fireflyText;

    public AudioSource coinSoundSource;
    public AudioClip[] coinSounds;
    //public Text scoreText;
    

    
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
        //Debug.Log("score: " + im.score);
        //scoreText.text = im.score.ToString();
        if (isCollected)
            return;

        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            CollectItem();
        }

       
    }

    public void CollectItem()
    {
        int randomIndex = Random.Range(0, coinSounds.Length);
        AudioClip selectedSound = coinSounds[randomIndex];


        coinSoundSource.clip = selectedSound;
        coinSoundSource.Play();
        isCollected = true;
        IncreasePlayerSpotlightRadius();
        Debug.Log("Item collected.");
        gameObject.SetActive(false);
        im.flyCount++;
        Debug.Log("Items: " + im.flyCount);
        fireflyText.text = im.flyCount.ToString();
        // im.score += 5;

        

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
