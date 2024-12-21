using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector2 moveDirection;

    // Reference to the Animator component
    public Animator animator;

    public AudioClip[] walkSoundClips; // Reference to the walking sound effect
    public AudioSource audioSource; // Reference to the AudioSource component

   

    void Start()
    {
       audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        

        ProcessInputs();
        UpdateAnimator();
        
       
    }

    private void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

    }

    private void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        if (moveDirection.magnitude > 0 && !audioSource.isPlaying)
        {
            AudioClip selectedSound = walkSoundClips[Random.Range(0, walkSoundClips.Length)];
            // Play the walking sound effect
            audioSource.clip = selectedSound;
            audioSource.Play();
        }
        // Check if the player stops moving and the walking sound effect is playing
        else if (moveDirection.magnitude == 0 && audioSource.isPlaying)
        {
            // Stop the walking sound effect
            audioSource.Stop();
        }
        
    }

    void UpdateAnimator()
    {
        // Set the animator parameters based on the movement direction
        animator.SetFloat("Horizontal", moveDirection.x);
        animator.SetFloat("Vertical", moveDirection.y);
        animator.SetFloat("Speed", moveDirection.magnitude);

        // Set facing direction based on movement angle
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        animator.SetFloat("FacingDirection", angle);


    }

    
}
