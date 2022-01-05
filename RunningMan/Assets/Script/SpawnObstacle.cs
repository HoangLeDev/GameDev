using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{
    public GameObject[] obstacle;
    private PlayerController playerControllerScript;
    private int index;
    // Update is called once per frame
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    void Update()
    {
        index = Random.Range(0,obstacle.Length);
    }

    void spawnObject()
    {
        if(playerControllerScript.gameOver==false)
        {
            Vector3 spawnPos = new Vector3(24, 0, 0);
            Instantiate(obstacle[index], spawnPos, obstacle[index].transform.rotation);
        }
    }

    public void StartSpawn()
    {
        InvokeRepeating("spawnObject",3.5f,4f);
    }
}
