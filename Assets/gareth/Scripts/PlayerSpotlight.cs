using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerSpotlight : MonoBehaviour
{
    public ItemManager im;
    public Light2D spotlight2D; // Change the variable type to Light2D
    public Animator animator;

    void Start()
    {
        im.playerDead += 2.5f;
        spotlight2D = GetComponentInChildren<Light2D>(); // Change the component type

        if (spotlight2D == null)
        {
            Debug.LogError("2D Spotlight not found in children!");
        }
    }

    void Update()
    {
        spotlight2D.transform.rotation = transform.rotation;

        if(im.flyCount < 0 && im.playerDead > 1)
        {
            im.playerDead -= Time.deltaTime;
            animator.SetTrigger("death");
        }
        if(im.flyCount < 0 && im.playerDead < 1)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
    }

    
}
