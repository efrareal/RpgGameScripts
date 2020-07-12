using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class QuestItem : MonoBehaviour
{
    public int questID;
    private QuestManager questManager;
    public string itemName;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            questManager = FindObjectOfType<QuestManager>();
            Quest q = questManager.QuestWithID(questID);
            if(q == null)
            {
                Debug.LogErrorFormat("La mision con ID {0} no existe", questID);
            }

            if (q.questStarted && !q.questCompleted)
            {
                questManager.itemCollected = this;
                gameObject.SetActive(false);
            }
        }
    }


}
