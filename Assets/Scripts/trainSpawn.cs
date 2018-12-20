using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trainSpawn : MonoBehaviour {

    public Transform left;
    public Transform right;
    public GameObject train;
    public float timerMin;
    public float timerMax;
    float tick;
    float timer;
    int direction;

	// Use this for initialization
	void Start () {
        timer = Random.Range(timerMin, timerMax);
	}
	
	// Update is called once per frame
	void Update () {
        if (playerMovement.instance.gameStart)
        {
            tick += 1 * Time.deltaTime;
        }
       
        if(tick >= timer)
        {
            if (direction == 0)
            {
                direction = 1;
                train.transform.position = left.position;
                train.GetComponent<trainMove>().leftDir = true;
            }
                
            else if (direction == 1)
            {
                direction = 0;
                train.transform.position = right.position;
                train.GetComponent<trainMove>().leftDir = false;

            }
                

            Debug.Log("direction " + direction);
         
            train.SetActive(true);
            timer = Random.Range(timerMin, timerMax);
            Debug.Log(timer);
            train.GetComponent<trainMove>().speed += 0.5f;
            tick = 0;
        }

    }
}
