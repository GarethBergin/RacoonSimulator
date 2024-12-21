using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyCollide : MonoBehaviour
{
    public ItemManager im;
    public float spotlightRadiusIncrease = 1f;
    public GameObject player;
    public Text fireflyText;
    // public Text scoreText;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player") && im.powerUp == 0 )
        {
            DecreasePlayerSpotlightRadius();
        }
        if(other.CompareTag("player") && im.powerUp == 1  )
        {
            //EnemyDead();
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            
        }
    }
    // Start is called before the first frame update
    void Start()
    {
      im.enemyTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void DecreasePlayerSpotlightRadius()
    {
        SpotlightPulse playerSpotlight = player.GetComponentInChildren<SpotlightPulse>();
        if (playerSpotlight != null)
        {
            // Increase min and max outer radius
            playerSpotlight.minOuterRadius -= spotlightRadiusIncrease;
            playerSpotlight.maxOuterRadius -= spotlightRadiusIncrease;

            Debug.Log("Min Outer Radius: " + playerSpotlight.minOuterRadius);
            Debug.Log("Max Outer Radius: " + playerSpotlight.maxOuterRadius);
            im.flyCount--;
            Debug.Log("Items: " + im.flyCount);
            fireflyText.text = im.flyCount.ToString();
            


        }
        else
        {
            Debug.LogWarning("Player's spotlight not found!");
        }
    }

    //public void EnemyDead()
    //{
       // im.score += 10;
        //Debug.Log("score: " + im.score);
       // scoreText.text = im.score.ToString();
       // gameObject.SetActive(false);

       // im.enemyTime += 5.0f;
       // im.flyCount--;
       // im.evadeTime = 0;
   // }
}
