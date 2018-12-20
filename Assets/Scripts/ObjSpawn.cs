using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSpawn : MonoBehaviour {

    public GameObject[] Spawnables;
    public float minTime, maxTime;
    BoxCollider2D spawnBox;
    float timer;
	// Use this for initialization
	void Start () {
        spawnBox = GetComponent<BoxCollider2D>();
        timer = Random.Range(minTime, maxTime);
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void SpawnStuff()
    {
        Vector3 spawnPos = new Vector3(Random.Range(spawnBox.bounds.min.x, spawnBox.bounds.max.x), transform.position.y, transform.position.z);
        Instantiate(Spawnables[Random.Range(0, Spawnables.Length)], spawnPos, Quaternion.identity);
    }

    public void SpawnRunner()
    {
        InvokeRepeating("SpawnStuff", timer, timer);
    }
}
