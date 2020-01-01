using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



//From the Brackeys tutorial
public class DialogueManager : MonoBehaviour
{
    //panel objects
    public GameObject speechPanel;
    public Text nameText;
    public Text dialogueText;
    public Image portrait;

    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        speechPanel.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue)
    {

        //set animation bool & make panel appear
        speechPanel.SetActive(true);

        Debug.Log("starting convo with " + dialogue.name);
        nameText.text = dialogue.name;
        portrait.sprite = dialogue.portrait;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

        //dialogueText.text = sentence;
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        Debug.Log("end");
        speechPanel.SetActive(false);
    }
}
