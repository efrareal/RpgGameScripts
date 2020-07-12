using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    private QuestManager questManager;
    public int questID;
    public bool startPoint, endPoint;
    private bool playerInZone;
    public bool automaticCatch;
    // Start is called before the first frame update
    void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Debug.Log("Player en zona de quest");
            playerInZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Debug.Log("Player salio de la zona de quest");
            playerInZone = false;
        }
    }

    private void Update()
    {
        if (playerInZone)
        {
            if(automaticCatch || (!automaticCatch && Input.GetButtonDown("Jump")))
            {
                Quest q = questManager.QuestWithID(questID);
                if(q == null)
                {
                    Debug.LogErrorFormat("La mision con ID {0} no existe", questID);
                    return;
                }
                //Si llego aqui, la mision existe!
                Debug.Log("LA quest existe");
                if (!q.questCompleted) // Si quitamos esta linea , la mision es repetible!
                {
                    Debug.Log("No se ha completado la mision");
                    //No se ha completado la mision actual
                    if (startPoint)
                    {
                        Debug.Log("zona de empezar la mision");
                        //Estoy en la zona donde empieza la mision
                        if (!q.questStarted)
                        {
                            Debug.Log("Activa la mision en el jerarquia");
                            q.gameObject.SetActive(true);
                            q.StartQuest();
                        }
                    }
                    if (endPoint)
                    {
                        Debug.Log("En zona de completacion de mision");
                        //Estoy en la zona de completacion de la mision
                        if (q.questStarted)
                        {
                            Debug.Log("Se ha completado la mision");
                            q.CompleteQuest();
                        }
                    }
                }
            }
        }
    }


}
