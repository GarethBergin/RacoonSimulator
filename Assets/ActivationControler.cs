using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationController : MonoBehaviour
{
    public GameObject level3;
    public GameObject level2;
    public GameObject gameEnd;



    void Start()
    {
        // Check if collision occurred in SceneA
        if (PlayerPrefs.GetInt("CollisionOccurred") == 1)
        {
            // Activate the desired GameObject or component
            level2.SetActive(true);
            
            // Reset PlayerPrefs value (optional)
            //PlayerPrefs.SetInt("CollisionOccurred", 0);
        }

        if (PlayerPrefs.GetInt("CollisionOccurred") == 2)
        {
            // Activate the desired GameObject or component
            level3.SetActive(true);
            
            // Reset PlayerPrefs value (optional)
            //PlayerPrefs.SetInt("CollisionOccurred", 0);
        }

            if (PlayerPrefs.GetInt("CollisionOccurred") == 3)
        {
            // Activate the desired GameObject or component
            gameEnd.SetActive(true);
            
            // Reset PlayerPrefs value (optional)
            //PlayerPrefs.SetInt("CollisionOccurred", 0);
        }

        
    }
}
