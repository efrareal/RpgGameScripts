using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectIf : MonoBehaviour
{
    private QuestManager questManager;
    public GameObject target;
    private float speed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag.Equals("Player"))
        {

            if (questManager.quests[7].questCompleted &&
               questManager.quests[8].questCompleted &&
               questManager.quests[9].questCompleted &&
               questManager.quests[10].questCompleted &&
               questManager.quests[11].questCompleted)
            {

                MoveObjectTo();
            }
        }
    }

    void MoveObjectTo()
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, speed* Time.fixedDeltaTime);
    }
}
