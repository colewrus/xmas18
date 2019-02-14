using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour {

    public static playerMovement instance = null;

    //Movement
    float direction;
    Rigidbody2D rb;
    public float moveSpeed;
    public float jumpMin; //minimum swipe distance to jump
    public float jumpPower;
    public float xMoveMin; //how far does x input need to be from X position to move?
    Vector3 position;
    public bool jump;
    Vector2 startTouch;
    Vector2 touchEnd;
    bool move;
    Vector2 dir;
        

    [Tooltip("Control the sprite direction flip here?")]
    public bool flipSprite;

    //spawn
    public GameObject spawner;
    public Transform respawn;

    //Touch vars
    float tapTimer;

    float width;
    float height;


    //Score stuff
    public Text hearts;
    public Text coins;
    float coinCount;
    float heartCount;

    //Audio stuff
    public AudioClip[] sfx;


    //Intro vars
    public  bool gameStart;
    public GameObject[] selectors;

    //Camera stuph
    public Camera cam;

    

    //VFX
    Animator anim;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;
    }


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        jump = false;
        gameStart = false;
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {



        TapMovement();

        if (transform.position.y <= -18)
            transform.position = respawn.position;

    }


    void Mousemovement()
    {

        if (Input.GetMouseButtonDown(1))
        {
            anim.SetBool("jumping", true);
            
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }

        if (Input.GetMouseButton(0))
        {

          

            Vector3 pos = Input.mousePosition;
            Vector3 camPos = cam.ScreenToWorldPoint(new Vector3(pos.x, pos.y, 1));
            
          
            if (Mathf.Abs(transform.position.x - camPos.x) > xMoveMin)
            {
                anim.SetBool("running", true);
                direction = (pos.x > (Screen.width / 2)) ? 1 : -1;
                rb.velocity = new Vector3(direction * moveSpeed, rb.velocity.y);
            }
            
        
        }

        if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("running", false);
        }

        
  
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "ground")
        {
            jump = false;
            anim.SetBool("jumping", false);
        }

        if(collision.gameObject.name == "train")
        {
            rb.AddForce(transform.up * 10, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<trainMove>().speed = 6;
        }

        if(collision.transform.tag == "heart")
        {
            collision.gameObject.SetActive(false);
            heartCount++;
            hearts.text = "" + heartCount;
        }

        if(collision.transform.tag == "coin")
        {
            collision.gameObject.SetActive(false);
            coinCount++;
            coins.text = "" + coinCount;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       

        if(collision.tag == "start" && !gameStart)
        {
           
            if(collision.name == "heart")
            {
                Debug.Log("hit start object");
                music_script.instance.PlaySong(0);
            }
            if(collision.name == "target")
            {
                music_script.instance.PlaySong(1);
            }
            if(collision.name == "axe")
            {
                music_script.instance.PlaySong(2);
            }
            if(collision.name == "beer")
            {
                music_script.instance.PlaySong(3);
            }

            for (int i = 0; i < selectors.Length; i++) {
                selectors[i].SetActive(false);
            }
            gameStart = true;
            spawner.GetComponent<ObjSpawn>().SpawnRunner();
        }


    }

    void TapMovement()
    {

        if (Input.touchCount > 0)
        {
            Touch myTouch = Input.touches[0];

            if (move)
            {
                rb.velocity = new Vector2(dir.x, rb.velocity.y);
                anim.SetBool("running", true);
            }

            if (myTouch.phase == TouchPhase.Began) //--------------BEGIN
            {
                startTouch = myTouch.position;


            }
            else if (myTouch.phase == TouchPhase.Moved) //-----------------MOVED
            {
                if ((myTouch.position.x - startTouch.x) >= xMoveMin)
                {

                    move = true;
                    dir = new Vector2(1 * moveSpeed, 0);

                    if (flipSprite)
                        this.GetComponent<SpriteRenderer>().flipX = false;

                }
                else if ((myTouch.position.x - startTouch.x) <= -xMoveMin)
                {

                    move = true;
                    dir = new Vector2(-1 * moveSpeed, 0);

                    if (flipSprite)
                        this.GetComponent<SpriteRenderer>().flipX = true;

                }
            }
            else if (myTouch.phase == TouchPhase.Ended) //-----------------------ENDED
            {
                touchEnd = myTouch.position;
                move = false;
                float x = touchEnd.x - startTouch.x;
                float y = touchEnd.y - startTouch.y;

                if (y >= jumpMin)
                {
                    //Debug.Log("Jump");
                    Vector2 worldRelease = cam.ScreenToWorldPoint(touchEnd);
                    Vector2 direction = worldRelease - new Vector2(transform.position.x, transform.position.y);
                    direction = new Vector2(Mathf.Clamp(direction.x, -1, 1), direction.y * jumpPower);
                    rb.AddForce(direction, ForceMode2D.Impulse);
                    anim.SetBool("jumping", true);

                }
                else if (y <= -jumpMin)
                {
                    Vector2 worldRelease = cam.ScreenToWorldPoint(touchEnd);
                    Vector2 direction = worldRelease - new Vector2(transform.position.x, transform.position.y);
                    //rb.AddForce(direction * jumpPower, ForceMode2D.Impulse);

                }
                anim.SetBool("running", false);

            }
        }

        /*
        if (Input.touchCount > 0)
        {
      
            Touch touch = Input.GetTouch(0);
            tapTimer += 1 * Time.deltaTime;

            if (cam.ScreenToWorldPoint(touch.position).x < transform.position.x)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }


            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 pos = touch.position;

                direction = (pos.x > (Screen.width / 2)) ? 1 : -1;
                anim.SetBool("running", true);
                rb.velocity = new Vector2((direction * speed), rb.velocity.y);
            }

            if(touch.phase == TouchPhase.Ended)
            {

                Vector3 worldRelease = cam.ScreenToWorldPoint(touch.position);
                float yDist = Vector2.Distance(new Vector3(0, transform.position.y, 0), new Vector3(0, worldRelease.y, 0));

                if(yDist > jumpMin)
                {
                    anim.SetBool("jumping", true);
                    rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                    
                }

                tapTimer = 0;
            }

        }
        */

    }
}
