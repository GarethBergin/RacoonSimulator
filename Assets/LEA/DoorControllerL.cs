using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorControllerL : MonoBehaviour
{
    public Tilemap openDoorTilemap;
    public Tilemap closedDoorTilemap;
    public ItemManager im; // Reference to the ItemManager script

    private bool playerInRange = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            playerInRange = true;
            OpenOrCloseDoor();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            playerInRange = false;
            OpenOrCloseDoor();
        }
    }

    private void OpenOrCloseDoor()
    {
        if (playerInRange)
        {
            // Player is in range
            if (openDoorTilemap.gameObject.activeSelf)
            {
                // Door is open
                if (im.flyCount < 1) // Player has fewer than one firefly
                {
                    closedDoorTilemap.gameObject.SetActive(true);
                    openDoorTilemap.gameObject.SetActive(false);
                }
            }
            else
            {
                // Door is closed, open if player has collected fireflies
                if (im.flyCount >= 1)
                {
                    closedDoorTilemap.gameObject.SetActive(false);
                    openDoorTilemap.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            // Player left the range, close the door if it's open
            if (openDoorTilemap.gameObject.activeSelf)
            {
                openDoorTilemap.gameObject.SetActive(false);
                closedDoorTilemap.gameObject.SetActive(true);
            }
        }
    }
}
