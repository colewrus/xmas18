using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trainMove : MonoBehaviour {

    public float speed;
    public float lineDist;

    public bool leftDir;

    Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (leftDir)
        {
            rb.velocity = new Vector2(1 * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-1 * speed, rb.velocity.y);
        }
      

	}


    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "trainTunnel")
        {
            gameObject.SetActive(false);
        }
    }
}
