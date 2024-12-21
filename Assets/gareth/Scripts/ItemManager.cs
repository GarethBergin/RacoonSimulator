using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject enemy;
    public int flyCount;
    public int score;
    public int powerUp;
    public float enemyTime;
    public float evadeTime;
    public float playerDead;
    public float enemyCount;
    public float health = 1.0f;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyTime > 0)
        {
            enemyTime -= Time.deltaTime;
            if (enemyTime < 0)
            {
                enemy.SetActive(true);
            }
        }
        //Debug.Log("Time: " + enemyTime);
    }
}
