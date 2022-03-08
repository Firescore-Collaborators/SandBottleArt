using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandSpawn : MonoBehaviour
{
    public GameObject sand;
    public float spawnInterval;


    private void Update() {
        
        if (Input.GetKeyDown(KeyCode.Space)) {
            InvokeRepeating("SpawnSand", 0, spawnInterval);
        }

        if (Input.GetKeyUp(KeyCode.Space)) {
            CancelInvoke("SpawnSand");
        }

    }


    void SpawnSand()
    {
        GameObject temp = Instantiate(sand, transform.position, Quaternion.identity);
        temp.transform.parent = transform;
    }
}
