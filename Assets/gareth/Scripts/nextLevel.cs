using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class nextLevel : MonoBehaviour
{
    // The name of the tag given to the tile you want to use as a trigger
    public string triggerTileTag = "NextLevelTile";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has the specified tag
        if (collision.CompareTag(triggerTileTag))
        {
            // Load the next scene
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        // // Get the index of the next scene
        // int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // // Check if there is a scene at the next index
        // if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        // {
        //     // Load the next scene
        //     SceneManager.LoadScene(nextSceneIndex);
        // }
        // else
        // {
        //     // If there are no more scenes, restart the game or go to the main menu
        //     // Example:
        //     SceneManager.LoadScene(0); // Loads the first scene (assuming it's the main menu)
           
        // }
        SceneManager.LoadScene(1);
    }
}

