using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenue : MonoBehaviour
{
    // public float time = 0.0f;

    // void Start()
    // {
    //     time = 0.0f;
    // }
    // void Update()
    // {
    //     if(time > 0)
    //     {
    //         time -= Time.deltaTime;
    //         if (time < 0)
    //         {
    //             SceneManager.LoadSceneAsync(1);
    //         }
    //     }

    //     Debug.Log("Time: " + time);
    // }
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void credits()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void menue()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
