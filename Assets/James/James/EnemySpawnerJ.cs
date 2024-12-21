using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnerJ : MonoBehaviour
{
    [SerializeField] private GameObject swarmerPrefab;
    [SerializeField] private int maxSwarmerCount = 5;
    public int currentSwarmerCount = 0;
    [SerializeField] private float swarmerInterval = 5f;

    [SerializeField] private GameObject swarmerEyePrefab;
    [SerializeField] private int maxSwarmerEyeCount = 5;
    public int currentSwarmerEyeCount = 0;
    [SerializeField] private float swarmerEyeInterval = 5f;

    private bool isSpawningEnabled = true;
    public ItemManager im;
    public Text enemyText;
 

    // Start is called before the first frame update
    void Start()
    {
        im.enemyCount = 10.0f;
        enemyText.text = im.enemyCount.ToString();
        StartCoroutine(SpawnEnemy(swarmerInterval, swarmerPrefab, maxSwarmerCount, () => currentSwarmerCount++, () => currentSwarmerCount));
        StartCoroutine(SpawnEnemy(swarmerEyeInterval, swarmerEyePrefab, maxSwarmerEyeCount, () => currentSwarmerEyeCount++, () => currentSwarmerEyeCount));
    }

    void update()
    {

    }

    private IEnumerator SpawnEnemy(float interval, GameObject enemy, int maxCount, System.Action incrementCount, System.Func<int> getCurrentCount)
    {
        while (isSpawningEnabled && getCurrentCount() < maxCount)
        {
            
            yield return new WaitForSeconds(interval);
            Instantiate(enemy, new Vector3(Random.Range(-5f, 5f), Random.Range(-6f, 6f), 0), Quaternion.identity);
            incrementCount();

            if (currentSwarmerCount + currentSwarmerEyeCount >= 10)
            {
                isSpawningEnabled = false;
                break;
            }
        }
    }
}
