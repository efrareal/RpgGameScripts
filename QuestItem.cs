using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class QuestItem : MonoBehaviour
{
    public int questID;
    private QuestManager questManager;
    public string itemName;
    private ItemsManager itemsManager;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Debug.Log("Colisione con el QuestItem");
            questManager = FindObjectOfType<QuestManager>();
            itemsManager = FindObjectOfType<ItemsManager>();
            Quest q = questManager.QuestWithID(questID);
            if(q == null)
            {
                Debug.LogErrorFormat("La mision con ID {0} no existe", questID);
            }

            if (q.questStarted && !q.questCompleted)
            {
                questManager.itemCollected = this;
                itemsManager.AddQuestItem(this.gameObject);
                //this.transform.parent = collision.gameObject.transform;
                gameObject.SetActive(false);
                Debug.Log("recoge el item");
            }
        }
    }


}
