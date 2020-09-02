using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quests;
    private DialogueManager dialogueManager;
    public QuestItem itemCollected;
    public QuestEnemy enemyKilled;

    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();

        foreach(Transform t in transform)
        {
            quests.Add(t.gameObject.GetComponent<Quest>());
        }
    }


    public void ShowQuestText(string questText)
    {
        dialogueManager.ShowDialogue(new string[] { questText });
    }

    public Quest QuestWithID(int questID)
    {
        Quest q = null;
        foreach(Quest temp in quests)
        {
            //Debug.LogFormat("Quest temporal {0}, questID por parametro {1}", temp.questID,questID);
            if(temp.questID == questID)
            {
                q = temp;
            }
        }
        return q;
    }

    public List<int> QuestsStarted()
    {
        List<int> questsStarted = new List<int>();

        foreach (Quest q in quests)
        {
            if (q.questStarted)
            {
                questsStarted.Add(q.questID);
            }
        }
        return questsStarted;
    }

    public List<int> QuestsCompleted()
    {
        List<int> questsCompleted = new List<int>();
        foreach (Quest q in quests)
        {
            if (q.questCompleted)
            {
                questsCompleted.Add(q.questID);
            }
        }
        return questsCompleted;
    }

    public void SearchQuestByIDSetStarted(int id)
    {
        foreach (Quest q in quests)
        {
            if (q.questID == id)
            {
                if (!q.questCompleted)
                {
                    q.gameObject.SetActive(true);
                    q.StartQuest();
                    //q.questStarted = true;
                }
            }
        }
    }

    public void SearchQuestByIDSetCompleted(int id)
    {
        foreach (Quest q in quests)
        {
            if (q.questID == id)
            {
                q.questCompleted = true;
            }
        }
    }

    public void ResetAllQuests()
    {
        foreach (Quest q in quests)
        {
            q.questStarted = false;
            q.questCompleted = false;
            q.gameObject.SetActive(false);
            if (q.killsEnemy)
            {
                Debug.Log("Match a Quest de matar enemigos");
                q.ResetNumberOfEnemies();
            }
            if (q.needsItem)
            {
                q.ResetNumberOfItems();
            }
        }
    }
}
