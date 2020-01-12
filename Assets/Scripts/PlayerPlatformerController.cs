using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void ComputerVelocity()
    {
        Vector2 move = Vector2.zero;
       
        move.x = ControllerInput._instance.xInput;
        //move.x = Input.GetAxis("Horizontal"); 
        //This was the default input method 
        /*
        if(Input.GetButtonDown("Jump") && grounded)   //jump method
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
                velocity.y = velocity.y * 0.5f;
        }*/

        if (ControllerInput._instance.jump && grounded)   //jump method
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if (!ControllerInput._instance.jump)
        {
            if (velocity.y > 0)
                velocity.y = velocity.y * 0.5f;
        }



        targetVelocity = move * maxSpeed;

    }


}
