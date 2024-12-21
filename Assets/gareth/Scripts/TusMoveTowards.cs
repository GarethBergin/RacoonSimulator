using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TusMoveTowards : MonoBehaviour
{
    public GameObject target;
    public float speed;
 
    // Update is called once per frame
    void Update()
    {
        // Position of the target: target.transform.position
        // Position of ther NPC: transform.position
        float speedDelta = speed * Time.deltaTime;

        Vector3 newPosition = tusMoveTowards(target.transform.position, speedDelta);

        transform.position = newPosition;
    }


    Vector3 tusMoveTowards(Vector3 pos, float sd)
    {
        

        Vector3 targetPosition = pos;
        Vector3 enemyPosition = transform.position;

        Vector3 rangeToClose = targetPosition - enemyPosition;

        Vector3 vectorNormalize = Vector3.Normalize(rangeToClose);

        float distanceToTarget = rangeToClose.magnitude;

        Debug.Log("Distance to target = " +  distanceToTarget);
        Debug.Log("Normalize Distance to target = " + vectorNormalize);

        Debug.DrawRay(enemyPosition, rangeToClose, new Color(0, 0, 255));
        Debug.DrawRay(enemyPosition, vectorNormalize, new Color(255, 0, 255));


        Vector3 delta = vectorNormalize * sd;

        Vector3 newPosition = enemyPosition + delta;
        return newPosition;
    }
}
