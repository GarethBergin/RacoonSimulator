using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenue : MonoBehaviour
{
    [SerializeField] GameObject pauseMenue;
    public void Pause()
    {
        pauseMenue.SetActive(true);
        Time.timeScale = 0;
    }
        
    public void exit()
    {
        pauseMenue.SetActive(false);
        Time.timeScale = 1;
        
    }
    public void menue()
    {
        SceneManager.LoadScene(0);
    }
}
