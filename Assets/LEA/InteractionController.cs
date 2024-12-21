using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public GameObject promptObject; // The object that prompts the player to press 'E'
    public GameObject interactableObject; // The object that becomes active when 'E' is pressed

    private bool inRange = false;
    private bool promptShown = false; // Tracks whether the prompt has been shown

    void Update()
    {
        // Check if the player is in range and 'E' is pressed
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            // Activate the interactable object
            interactableObject.SetActive(true);

            // Deactivate the prompt object
            if (promptObject != null)
                promptObject.SetActive(false);
        }
    }

    // Check if the player enters the interaction range
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            inRange = true;
            // Show the prompt to press 'E' if it hasn't been shown before
            if (promptObject != null && !promptShown)
            {
                promptObject.SetActive(true);
                promptShown = true;
            }
        }
    }

    // Check if the player exits the interaction range
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            inRange = false;
            // Hide the prompt
            if (promptObject != null)
                promptObject.SetActive(false);

            // Reset promptShown when player exits range so it shows again when re-entering
            promptShown = false;
        }
    }

    // Visualize the interaction range with gizmos in the Unity Editor
    private void OnDrawGizmosSelected()
    {
        // Draw the interaction range using the collider's radius
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, GetComponent<CircleCollider2D>().radius);
    }
}
