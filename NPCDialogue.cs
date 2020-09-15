using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class NPCDialogue : MonoBehaviour
{
    public string npcName;
    public string[] npcDialogueLines;
    public Sprite npcSprite;
    public int[] questID;
    public bool isQuestNPC;
    public int currentQuestId;

    public Dialogues[] npcQuestDialogues;

    private DialogueManager dialogueManager;
    private bool playerInTheZone;

    public bool isStore;
    public GameObject storeGUI;
    private PlayerController thePlayer;

    public bool givesQuest;


    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (currentQuestId < questID.Length)
            {
                if (isQuestNPC && dialogueManager.IsQuestCompleted(this.questID[currentQuestId]))
                {
                    currentQuestId++;
                }
            }
            playerInTheZone = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {

            playerInTheZone = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (playerInTheZone && Input.GetMouseButtonDown(1))
        {
            if (isStore)
            {
                Invoke("ActivateStoreGUI", 1.5f);
            }

            if (currentQuestId >= questID.Length)
            {
                DialoguePrint(npcDialogueLines);
                return;
            }
            if (isQuestNPC && !dialogueManager.IsQuestCompleted(this.questID[currentQuestId]))
            {
                //Aqui la quest no ha sido Completada
                if (!dialogueManager.IsQuestStarted(this.questID[currentQuestId]))
                {
                    if (givesQuest)
                    {
                        //Aqui la quest no ha sido disparada por dialogo
                        dialogueManager.isQuestDialogue = true;
                        dialogueManager.questId = this.questID[currentQuestId];
                        DialoguePrint(npcQuestDialogues[currentQuestId].questDialogues);
                        return;
                        //currentQuestId++;
                    }
                }

                //Aqui la quest si ha sido disparada por dialogo pero no ha sido completada
                DialoguePrint(npcQuestDialogues[currentQuestId].questDialogues);
                return;


            }
            if (isQuestNPC && dialogueManager.IsQuestCompleted(this.questID[currentQuestId]))
            {
                currentQuestId++;
            }
            else
            {
                string[] finalDialogue = new string[npcDialogueLines.Length];

                int i = 0;
                foreach (string line in npcDialogueLines)
                {
                    finalDialogue[i++] = (npcName != null ? npcName + "\n" : "") + line;
                }


                if (npcSprite != null)
                {
                    dialogueManager.ShowDialogue(finalDialogue, npcSprite);

                }
                else
                {
                    dialogueManager.ShowDialogue(finalDialogue);
                }

                if (gameObject.GetComponentInParent<NPCMovement>() != null)
                {
                    gameObject.GetComponentInParent<NPCMovement>().isTalking = true;
                }
            }
            
        }
    }

    void DialoguePrint(string[] npcDialogueLines)
    {
        string[] finalDialogue = new string[npcDialogueLines.Length];

        int i = 0;
        foreach (string line in npcDialogueLines)
        {
            finalDialogue[i++] = (npcName != null ? npcName + "\n" : "") + line;
        }


        if (npcSprite != null)
        {
            dialogueManager.ShowDialogue(finalDialogue, npcSprite);

        }
        else
        {
            dialogueManager.ShowDialogue(finalDialogue);
        }

        if (gameObject.GetComponentInParent<NPCMovement>() != null)
        {
            gameObject.GetComponentInParent<NPCMovement>().isTalking = true;
        }

    }

    void ActivateStoreGUI()
    {
        storeGUI.SetActive(true);
        thePlayer = FindObjectOfType<PlayerController>();
        thePlayer.canMove = false;
    }
}
