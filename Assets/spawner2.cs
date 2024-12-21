using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarSim;


public class spawner2 : MonoBehaviour
{
    public GameObject [] carPrefabs;
    public Transform [] spawnPoints;
    public Transform[] waypoints;
    //public Transform spawnPointStart, spawnPointEnd ;
    public float spawnFrequency = 5f;
    private float spawnTimer = 0f;
    public float lifetime;
    public bool spawning = false;

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if(spawnTimer >= spawnFrequency)
        {
            Spawn();
            spawnTimer = 0f;
        }

        
    }

    private void Spawn()
    {
        // Choose a random spawn point
        int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomSpawnIndex];

        // Choose a random Car prefab
        int randomCARIndex = Random.Range(0, carPrefabs.Length);
        GameObject carPrefab = carPrefabs[randomCARIndex];

        // Instantiate the Car at the chosen spawn point
        GameObject car = Instantiate(carPrefab, spawnPoint.position, spawnPoint.rotation);

         // Instantiate the Car prefab at a specific location
        //GameObject car = Instantiate(carPrefab, waypoints[0].position, Quaternion.identity);

        // Assign the waypoints to the spawned Car
        CarMove carScript = car.GetComponent<CarMove>();
        if (carScript != null)
        {
            carScript.pattern = waypoints; // Assign waypoints dynamically
        }

        Destroy(car, lifetime);


        
    }

}
