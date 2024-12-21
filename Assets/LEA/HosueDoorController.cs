using UnityEngine;

public class HouseDoorController : MonoBehaviour
{
    public GameObject doorClosed;
    public GameObject doorOpen;
    public string playerTag = "player";

    private bool isOpen = false;

    void Update()
    {
        // Check if the player is in range
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, GetComponent<CircleCollider2D>().radius);
        bool playerInRange = false;
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(playerTag))
            {
                playerInRange = true;
                break;
            }
        }

        // Open/close the door based on player's range
        if (playerInRange && !isOpen)
        {
            OpenDoor();
        }
        else if (!playerInRange && isOpen)
        {
            CloseDoor();
        }
    }

    void OpenDoor()
    {
        doorClosed.SetActive(false);
        doorOpen.SetActive(true);
        isOpen = true;
    }

    void CloseDoor()
    {
        doorOpen.SetActive(false);
        doorClosed.SetActive(true);
        isOpen = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, GetComponent<CircleCollider2D>().radius);
    }
}