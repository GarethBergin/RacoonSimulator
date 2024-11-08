using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{

    public Transform player, destination;
    public GameObject Racoon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            Racoon.SetActive(false);
            player.position = destination.position;
            Racoon.SetActive(true);
        }
    }
}
