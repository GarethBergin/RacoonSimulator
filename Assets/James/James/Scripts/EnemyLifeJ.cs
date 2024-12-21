using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHealth : MonoBehaviour
{
    public int health = 3;
    // Start is called before the first frame update
    void Start()
    {

    }

    //Damag eMethod
    public void Hit(int hitAmount)
    {

        //Health is greater than 0
        if (health >= 0)
        {
            //calculate new health
            health = health - hitAmount;

            //Report the value to the console
            Debug.Log("Health is now: " + health + "for the " + gameObject.name);
        }
    }

}
