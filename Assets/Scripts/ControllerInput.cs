using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInput : MonoBehaviour
{

    public static ControllerInput _instance;
    public float xInput;
    public bool jump;
    public bool moving;
    float xModifier;

    // Start is called before the first frame update
    void Start()
    {
        _instance = GetComponent<ControllerInput>();
        jump = false;
        xInput = 0;
        moving = false;
    }


    private void FixedUpdate()
    {
        if (moving)
        {
            xInput = Mathf.Clamp(xInput + (0.1f*xModifier), -1, 1);
            //Debug.Log("xInput value: " + xInput);
        }
        else
        {
            xInput = 0;
        }
    }

    public void Xincrease()
    {
        xModifier = 1;
        moving = true;

    }

    public void Xdecrease()
    {
        xModifier = -1;
        moving = true;
    }

    public void Xrelease()
    {
        moving = false;
    }

    public void JumpDown()
    {
        jump = true;
        Debug.Log("Jumping: " + jump);
    }

    public void JumpRelease()
    {
        jump = false;
        Debug.Log("Jumping: " + jump);
    }

}
