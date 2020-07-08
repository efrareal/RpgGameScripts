using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueText;
    public Image avatarImage;
    public bool dialogueActive;

    private void Start()
    {
        dialogueActive = false;
        dialogueBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            dialogueActive = false;
            avatarImage.enabled = false;
            dialogueBox.SetActive(false);
        }
    }

    public void ShowDialogue(string text)
    {
        dialogueActive = true;
        dialogueBox.SetActive(true);
        dialogueText.text = text;
    }
    public void ShowDialogue(string text, Sprite sprite)
    {
        ShowDialogue(text);
        avatarImage.enabled = true;
        avatarImage.sprite = sprite;
    }
}
