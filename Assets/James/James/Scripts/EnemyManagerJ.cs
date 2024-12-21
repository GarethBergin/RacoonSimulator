using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManagerJ : MonoBehaviour
{

    public Animator animator;

    // ItemHealth itemHealth;

    bool deathTriggered = false;

    public int destroyTimer = 1;
    public ItemManager im;
    public Text enemyText;

    public GameObject gate;
    public GameObject gateGrid;

    public AudioSource enemySoundSource;
    




    // Start is called before the first frame update
    void Start()
    {
        // animator = gameObject.GetComponent<Animator>();
        // itemHealth = gameObject.GetComponent<ItemHealth>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bullet"))
        {
            im.health--;
            if (im.health <= 0 && !deathTriggered)
        {
            enemySoundSource.Play();

            //Trigger Death Animation
            im.enemyCount--;
            enemyText.text = im.enemyCount.ToString();

            animator.SetTrigger("Death");

            deathTriggered = true;

            StartCoroutine(DestroyGameObject(destroyTimer));
        }
        }
    }
    // Update is called once per frame
    void Update()
    {

        Debug.Log("Enemies" + im.enemyCount);
        if(im.enemyCount <= 0 )
        {
            gate.SetActive(false);
            gateGrid.SetActive(false);
        }
        //Debug.Log("Enemys: " + im.enemyCount);

        

    }

    IEnumerator DestroyGameObject(int delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        Destroy(gameObject);
    }
}
