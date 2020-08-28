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
    public bool isQuestDialogue;
    public int questId;


    public string[] dialogueLines;
    public int currentDialogueLine;

    private PlayerController thePlayer;
    private QuestManager questManager;


    private void Start()
    {
        dialogueActive = false;
        dialogueBox.SetActive(false);
        thePlayer = FindObjectOfType<PlayerController>();
        questManager = FindObjectOfType<QuestManager>();
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
                if (isQuestDialogue)
                {
                    Quest q;
                    q = questManager.QuestWithID(questId);
                    q.gameObject.SetActive(true);
                    q.StartQuest();
                    isQuestDialogue = false;
                    questId = 0;
                }
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

    public bool IsQuestStarted(int qid)
    {
        Quest qi;
        qi = questManager.QuestWithID(qid);
        return qi.questStarted;
    }

    public bool IsQuestCompleted(int qid)
    {
        Quest qi;
        qi = questManager.QuestWithID(qid);
        return qi.questCompleted;
    }
}
