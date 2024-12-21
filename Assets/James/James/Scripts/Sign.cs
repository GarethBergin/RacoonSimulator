using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gravestone : MonoBehaviour
{
    public GameObject dialogBox;
    //public Text dialogText;
    public string dialog;
    public bool playerInRange;
    public GameObject interactionPrompt;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
                Time.timeScale = 1; 
            }
            else
            {
                dialogBox.SetActive(true);
                //dialogText.text = dialog;
                Time.timeScale = 0; 
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            playerInRange = true;
             ShowInteractionPrompt(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            playerInRange = false;
            dialogBox.SetActive(false);
            Time.timeScale = 1;
            ShowInteractionPrompt(false);
        }
    }
      

    void ShowInteractionPrompt(bool show)
    {
        interactionPrompt.SetActive(show);
    }
}
