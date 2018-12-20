using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {

    //Movement
    float direction;
    Rigidbody2D rb;
    public float speed;
    public float jumpMin; //minimum swipe distance to jump
    public float jumpPower;
    public float xMoveMin; //how far does x input need to be from X position to move?

    public bool jump;

    //Touch vars
    float tapTimer;

    float width;
    float height;


    //Camera stuph
    public Camera cam;

    Vector3 position;

    //VFX
    Animator anim;

    private void Awake()
    {
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;
    }


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        jump = false;
	}
	
	// Update is called once per frame
	void Update () {

        Mousemovement();

	}


    void Mousemovement()
    {

        if (Input.GetMouseButtonDown(1))
        {
            jump = true;
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }

        if (Input.GetMouseButton(0))
        {

          

            Vector3 pos = Input.mousePosition;
            Vector3 camPos = cam.ScreenToWorldPoint(new Vector3(pos.x, pos.y, 1));
            Debug.Log(camPos);
            if (!jump)
            {
                if (Mathf.Abs(transform.position.x - camPos.x) > xMoveMin)
                {
                    direction = (pos.x > (Screen.width / 2)) ? 1 : -1;
                    rb.velocity = new Vector3(direction * speed, rb.velocity.y);
                }
            }
        
        }

  
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "ground")
        {
            jump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "heart")
        {

        }


    }

    void TapMovement()
    {



    }
}
