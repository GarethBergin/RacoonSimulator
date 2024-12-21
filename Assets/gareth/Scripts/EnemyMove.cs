using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMoveTowards : MonoBehaviour
{
    public Animator animator;
    public ItemManager im;
    public GameObject target;
    public float speed = 0.0f;

    public AudioSource enemySound;
 
    void Start()
    {
        enemySound.Play();
        im.evadeTime += 5.0f;
    }
    // Update is called once per frame
    void Update()
    {
        if(im.flyCount == 1)
        {
            speed = 2.5f;
        }
        if(im.flyCount == 2)
        {
            speed = 3f;
        }
        if(im.flyCount == 3)
        {
            speed = 3.5f;
        }
        if(im.flyCount > 3)
        {
            speed = 4f;
        }
        if(im.powerUp == 1 && im.evadeTime <= 0)
        {
            enemySound.Play();
            speed = 2.5f;
            im.powerUp = 0;
        }
        // Position of the target: target.transform.position
        // Position of ther NPC: transform.position
        float speedDelta = speed * Time.deltaTime;
        if (im.powerUp == 0)
        {
            Vector3 newPosition = enemyMoveTowards(target.transform.position, speedDelta);


            transform.position = newPosition;
        }

        if(im.powerUp == 1)
        {
            im.evadeTime -= Time.deltaTime;
            if(im.evadeTime > 0)
            {
                enemySound.Stop();
                speed = 1.0f;
                Evade();
            }
        }
    }


    Vector3 enemyMoveTowards(Vector3 pos, float sd)
    {

        animator.SetBool("Right", true);
        animator.SetBool("Left", false);
        Vector3 targetPosition = pos;
        Vector3 enemyPosition = transform.position;

        Vector3 rangeToClose = targetPosition - enemyPosition;

        Vector3 vectorNormalize = Vector3.Normalize(rangeToClose);

        float distanceToTarget = rangeToClose.magnitude;

        //Debug.Log("Distance to target = " +  distanceToTarget);
        //Debug.Log("Normalize Distance to target = " + vectorNormalize);

        Debug.DrawRay(enemyPosition, rangeToClose, new Color(0, 0, 255));
        Debug.DrawRay(enemyPosition, vectorNormalize, new Color(255, 0, 255));


        Vector3 delta = vectorNormalize * sd;

        Vector3 newPosition = enemyPosition + delta;
        return newPosition;
    }

    public void Evade()
    {
        animator.SetBool("Left", true);
        animator.SetBool("Right", false);
        
        float speedDelta = speed * Time.deltaTime;
        if (Mathf.Abs(transform.position.x - target.transform.position.x) > -speedDelta)
        {
            if (transform.position.x > target.transform.position.x)
            {
                float deltax = speedDelta;
                transform.Translate(new Vector3(deltax, 0, 0));
            }
            else if (transform.position.x < target.transform.position.x)
            {
                float deltax = -speedDelta;
                transform.Translate(new Vector3(deltax, 0, 0));
            }
        }

        if (Mathf.Abs(transform.position.y - target.transform.position.y) > -speedDelta)
        {
            if (transform.position.y > target.transform.position.y)
            {
                float deltay = speedDelta;
                transform.Translate(new Vector3(0, deltay, 0));
            }
            else if (transform.position.y < target.transform.position.y)
            {
                float deltay = -speedDelta;
                transform.Translate(new Vector3(0, deltay, 0));
            }
        }
    }
    
}
