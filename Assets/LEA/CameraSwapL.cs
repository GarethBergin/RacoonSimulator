using UnityEngine;
using Cinemachine;

public class CameraSwapL : MonoBehaviour
{
    public CinemachineVirtualCamera defaultCamera; // The default camera
    public CinemachineVirtualCamera swapCamera; // The camera to swap to
    public GameObject player; // The player GameObject

    private bool inZone = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            inZone = true;
            SwapCameras();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            inZone = false;
            SwapCameras();
        }
    }

    private void SwapCameras()
    {
        if (inZone && !Input.GetKeyDown(KeyCode.E))
        {
            defaultCamera.gameObject.SetActive(false);
            swapCamera.gameObject.SetActive(true);

            // Set the swapCamera to follow the player
            swapCamera.Follow = player.transform;
            defaultCamera.Follow = null;
        }
        
        else
        {
            defaultCamera.gameObject.SetActive(true);
            swapCamera.gameObject.SetActive(false);

            // Set the defaultCamera to follow the player
            defaultCamera.Follow = player.transform;
            swapCamera.Follow = null;
        }
    }

}
