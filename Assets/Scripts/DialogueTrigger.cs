using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    public Dialogue dialogue;
    public bool contactTrigger; //set true if you want to activate when object enters
    public GameObject triggerObject;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (contactTrigger)
        {
            if (collision.gameObject == triggerObject)
            {
                TriggerDialogue();
                contactTrigger = false;
            }
                
        }
    }
}
