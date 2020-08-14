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
    public bool needsQuest;

    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (needsQuest)
        {
            Quest quest = questManager.QuestWithID(questID);
            if (quest.questStarted || quest.questCompleted)
            {
                Teleport(collision.gameObject.name);
            }
        }
        else
        {
            Teleport(collision.gameObject.name);
        }
    }


    private void Teleport(string objName)
    {
        if (objName == "Player")
        {
            if (!needsClick || (needsClick && Input.GetAxis("Fire1") > 0.2))
            {
                SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.CHANGE_SCENE);
                FindObjectOfType<PlayerController>().nextUuid = uuid;
                SceneManager.LoadScene(newPlaceName);
            }
        }
    }
}