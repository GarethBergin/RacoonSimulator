using UnityEngine;
using UnityEngine.Tilemaps;

public class OpenDoorController : MonoBehaviour
{
    public Tilemap openDoorTilemap;
    public Tilemap closedDoorTilemap;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            // Player left the trigger area, close the door
            openDoorTilemap.gameObject.SetActive(false);
            closedDoorTilemap.gameObject.SetActive(true);
        }
    }
}
