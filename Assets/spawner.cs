using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CitySim;


public class spawner : MonoBehaviour
{
    public GameObject [] npcPrefabs;
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

        // Choose a random NPC prefab
        int randomNPCIndex = Random.Range(0, npcPrefabs.Length);
        GameObject npcPrefab = npcPrefabs[randomNPCIndex];

        // Instantiate the NPC at the chosen spawn point
        GameObject npc = Instantiate(npcPrefab, spawnPoint.position, spawnPoint.rotation);

         // Instantiate the NPC prefab at a specific location
        //GameObject npc = Instantiate(npcPrefab, waypoints[0].position, Quaternion.identity);

        // Assign the waypoints to the spawned NPC
        CityPeople npcScript = npc.GetComponent<CityPeople>();
        if (npcScript != null)
        {
            npcScript.pattern = waypoints; // Assign waypoints dynamically
        }

        Destroy(npc, lifetime);


        
    }

}
