

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMovementL2 : MonoBehaviour
{
    // Public variables
    public GameObject target; 
    public float speed; 
    public float radius = 10.0f; 
    public Animator animator; 
    public EnemyLightControllerL lightController;
    public float restartDelay = 3f; 

    // Private variables
    private bool facingRight = true; 
    private bool isRunningAway = false; 
    private bool isIdle = false; 

    void Update()
    {
        // Check if the target is not null
        if (target != null)
        {
            // Calculate distance to the target
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

            if (distanceToTarget <= radius && !isRunningAway)
                ChaseTarget();
            else if (isRunningAway)
                EvadeTarget();
            else
                StopChasing();
        }
    }

    // Function to chase the target
    void ChaseTarget()
    {
        isIdle = false;
        animator.SetBool("isRunning", true);
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;
        transform.position = newPosition;
        Flip(direction);
        lightController.SetChasing(true);
    }

    // Function to evade the target
    void EvadeTarget()
    {
        float speedDelta = speed * Time.deltaTime;
        if (Mathf.Abs(transform.position.x - target.transform.position.x) > speedDelta)
            transform.Translate(Vector3.right * Mathf.Sign(transform.position.x - target.transform.position.x) * speedDelta);
        if (Mathf.Abs(transform.position.y - target.transform.position.y) > speedDelta)
            transform.Translate(Vector3.up * Mathf.Sign(transform.position.y - target.transform.position.y) * speedDelta);
        if (Vector3.Distance(transform.position, target.transform.position) > radius)
            isIdle = true;
    }

    // Function to stop chasing
    void StopChasing()
    {
        if (!isIdle)
        {
            animator.SetBool("isRunning", false); 
            lightController.SetChasing(false); 
        }
    }

    // Function to flip sprite based on direction
    void Flip(Vector3 direction)
    {
        if ((direction.x < 0 && facingRight) || (direction.x > 0 && !facingRight))
        {
            facingRight = !facingRight;
            GetComponent<SpriteRenderer>().flipX = !facingRight;
        }
    }

    // Function to handle collision with player
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);

            Invoke("RestartLevel", restartDelay);
        }
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Function to make the enemy run away
    public void RunAway() => isRunningAway = true;
}
