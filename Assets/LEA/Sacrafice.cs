using UnityEngine;

public class Sacrafice : MonoBehaviour
{
    public delegate void KeyCollected();
    public static event KeyCollected OnKeyCollected1;
    public static event KeyCollected OnKeyCollected2;
    public static event KeyCollected OnKeyCollected3;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            // Check the tag of the key and trigger the corresponding event
            if (gameObject.CompareTag("Key1"))
            {
                if (OnKeyCollected1 != null)
                    OnKeyCollected1();
            }
            else if (gameObject.CompareTag("Key2"))
            {
                if (OnKeyCollected2 != null)
                    OnKeyCollected2();
            }
            else if (gameObject.CompareTag("Key3"))
            {
                if (OnKeyCollected3 != null)
                    OnKeyCollected3();
            }

            gameObject.SetActive(false);
        }
    }
}
