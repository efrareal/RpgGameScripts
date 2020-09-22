using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaBlocked : MonoBehaviour
{
    public bool blockIsActive;
    public int questID;
    private QuestManager questManager;
    private Quest quest;
    public string dialogueText;

    public bool needsQuestStarted;
    public bool needsQuestCompleted;

    private ItemsManager itemsManager;
    public bool needsItem;
    public QuestItem item;

    // Start is called before the first frame update
    void Start()
    {
        itemsManager = FindObjectOfType<ItemsManager>();
        questManager = FindObjectOfType<QuestManager>();
        quest = questManager.QuestWithID(questID);
        if (!blockIsActive && !needsItem)
        {
             this.gameObject.SetActive(false);
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (needsItem)
        {
            if (itemsManager.HasTheQuestItem(item))
            {
                SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.LOCK_DOOR);
                this.gameObject.SetActive(false);
                blockIsActive = false;
            }
            else
            {
                if (collision.gameObject.tag.Equals("Player"))
                {
                    questManager.ShowQuestText(dialogueText);
                }
            }
            return;

        }
        
        if (blockIsActive)
        {
            //Si el area esta bloqueada
            if (needsQuestStarted)
            {
                //Si necesita solo que la quest este iniciada
                if (!quest.questCompleted)
                {
                    if (!quest.questStarted)
                    {
                        if (collision.gameObject.tag.Equals("Player"))
                        {
                            questManager.ShowQuestText(dialogueText);
                        }
                    }
                    else
                    {
                        this.gameObject.SetActive(false);
                        blockIsActive = false;
                    }
                }
                else
                {
                    this.gameObject.SetActive(false);
                    blockIsActive = false;
                }
            }
            if (needsQuestCompleted)
            {
                //Si necesita que la quest este terminada
                if (!quest.questCompleted)
                {
                    if (collision.gameObject.tag.Equals("Player"))
                    {
                        questManager.ShowQuestText(dialogueText);
                    }
                }
                else
                {
                    this.gameObject.SetActive(false);
                    blockIsActive = false;
                }

            }
        }

        //Si el area NO esta bloqueada
        else
        {
            this.gameObject.SetActive(false);
            blockIsActive = false;
        }
        
    }

}
