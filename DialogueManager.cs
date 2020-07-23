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

    public string[] dialogueLines;
    public int currentDialogueLine;

    private PlayerController thePlayer;

    private void Start()
    {
        dialogueActive = false;
        dialogueBox.SetActive(false);
        thePlayer = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueActive && Input.GetButtonDown("Jump"))
        {
            currentDialogueLine++;

            if (currentDialogueLine >= dialogueLines.Length)
            {
                thePlayer.isTalking = false;
                currentDialogueLine = 0;

                dialogueActive = false;
                avatarImage.enabled = false;
                dialogueBox.SetActive(false);
            }
            else
            {
                dialogueText.text = dialogueLines[currentDialogueLine];
            }
        }
    }

    public void ShowDialogue(string[] lines)
    {
        currentDialogueLine = 0;
        dialogueLines = lines;
        dialogueActive = true;
        dialogueBox.SetActive(true);
        dialogueText.text = dialogueLines[currentDialogueLine];
        thePlayer.isTalking = true;

    }
    public void ShowDialogue(string[] lines, Sprite sprite)
    {
        ShowDialogue(lines);
        avatarImage.enabled = true;
        avatarImage.sprite = sprite;
    }
}
