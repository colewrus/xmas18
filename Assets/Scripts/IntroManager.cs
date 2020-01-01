using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    public GameObject speechPanel;
    public GameObject Chelsea;

    private void FixedUpdate()
    {
        if (speechPanel.active)
        {
            Chelsea.GetComponent<PlayerPlatformerController>().enabled = false;
        }
        else
        {
            Chelsea.GetComponent<PlayerPlatformerController>().enabled = true;
        }
       
        

    }
}
