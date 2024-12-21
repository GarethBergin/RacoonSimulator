using Cinemachine;
using UnityEngine;

public class GraveController : MonoBehaviour
{
    // Reference to key objects
    public GameObject keyObject1;
    public GameObject keyObject2;
    public GameObject keyObject3;

    // Reference to objects that will appear
    public GameObject objectToAppear1;
    public GameObject objectToAppear2;
    public GameObject objectToAppear3;

    // Reference to spotlights
    public GameObject spotlight1;
    public GameObject spotlight2;
    public GameObject spotlight3;

    public GameObject altar1;
    public GameObject altar2;
    public GameObject altar3;

    public GameObject mushroomwall;
    public GameObject rockwall;

    public CinemachineVirtualCamera virtualCamera;

    // Flags to check if the player has keys
    private bool hasKey1 = false;
    private bool hasKey2 = false;
    private bool hasKey3 = false;

    private void Start()
    {
        // Subscribe to the KeyCollected events for each key
        Sacrafice.OnKeyCollected1 += HandleKeyCollected1;
        Sacrafice.OnKeyCollected2 += HandleKeyCollected2;
        Sacrafice.OnKeyCollected3 += HandleKeyCollected3;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the KeyCollected events
        Sacrafice.OnKeyCollected1 -= HandleKeyCollected1;
        Sacrafice.OnKeyCollected2 -= HandleKeyCollected2;
        Sacrafice.OnKeyCollected3 -= HandleKeyCollected3;
    }

    private void HandleKeyCollected1()
    {
        hasKey1 = true;
        spotlight1.SetActive(true);
    }

    private void HandleKeyCollected2()
    {
        hasKey2 = true;
        spotlight2.SetActive(true);
    }

    private void HandleKeyCollected3()
    {
        hasKey3 = true;
        spotlight3.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            if (gameObject.CompareTag("Grave") && hasKey1)
            {
                keyObject1.SetActive(false);
                objectToAppear1.SetActive(true);
                altar1.SetActive(true);
                mushroomwall.SetActive(false);
                hasKey1 = false;
                spotlight1.SetActive(false);
            }
            else if (gameObject.CompareTag("Grave1") && hasKey2)
            {
                keyObject2.SetActive(false);
                objectToAppear2.SetActive(true);
                altar2.SetActive(true);
                rockwall.SetActive(false);
                hasKey2 = false;
                spotlight2.SetActive(false);
            }
            else if (gameObject.CompareTag("Grave3") && hasKey3)
            {
                keyObject3.SetActive(false);
                objectToAppear3.SetActive(true);
                altar3.SetActive(true);
                hasKey3 = false;
                spotlight3.SetActive(false);
            }
        }
    }
}
