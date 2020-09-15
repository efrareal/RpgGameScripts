using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNewPlace : MonoBehaviour
{
    public string newPlaceName = "New place here!!!";
    public bool needsClick = false;
    public string uuid;
    private QuestManager questManager;
    public int questID;
    public bool needsQuestStarted;
    public bool needsQuestCompleted;


    private SceneTransition sceneTransition;

    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        sceneTransition = FindObjectOfType<SceneTransition>();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (needsQuestStarted)
        {
            Quest quest = questManager.QuestWithID(questID);
            Debug.Log("NeedsQuestStarted");
            if (!quest.questCompleted)
            {
                if (quest.questStarted)
                {
                    Teleport(collision.gameObject.name);
                }
            }
            
        }
        if (needsQuestCompleted)
        {
            Quest quest = questManager.QuestWithID(questID);
            if (quest.questCompleted)
            {
                Debug.Log("NeedsQuestCompleted");
                Teleport(collision.gameObject.name);
            }

        }
        else {
            Debug.Log("NeedsNothing");
            Teleport(collision.gameObject.name);
        }
        /*
        if(!needsQuestStarted && !needsQuestCompleted)
        {
            Debug.Log("NeedsNothing");
            Teleport(collision.gameObject.name);
        }*/
    }


    private void Teleport(string objName)
    {
        if (objName == "Player")
        {
            if (!needsClick || (needsClick && Input.GetAxis("Fire1") > 0.2))
            {
                SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.CHANGE_SCENE);
                FindObjectOfType<PlayerController>().nextUuid = uuid;
                FindObjectOfType<PlayerController>().inTransition = true;
                //SceneManager.LoadScene(newPlaceName);
                sceneTransition.Transition(newPlaceName);

            }
        }
    }
}