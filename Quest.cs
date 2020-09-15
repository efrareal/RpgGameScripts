using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quest : MonoBehaviour
{
    public int questID;
    public bool questStarted;
    public bool questCompleted;
    private QuestManager questManager;

    public bool needsItem;
    public List<QuestItem> itemsNeeded;
    public List<QuestItem> itemsNeededConfigured;

    public bool killsEnemy;
    public List<QuestEnemy> enemies;
    public List<QuestEnemy> enemiesConfigured = new List<QuestEnemy> { };
    public List<int> numberOfEnemies;
    public List<int> numberOfEnemiesConfigured = new List<int> { };

    public string title;
    public string startText;
    public string completeText;

    public Quest nextQuest;
    public bool givesReward;
    public int moneyValue;
    public string[] rewardType;
    public string[] rewardName;
    private List<GameObject> notInInventory;

    private AccesoryManager accManager;

    /*private void OnLevelWasLoaded(int level)
    {
        if (needsItem)
        {
            ActivateItems();
        }
        if (killsEnemy)
        {
            ActivateEnemies();
        }
    }*/

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (needsItem)
        {
            ActivateItems();
        }
        if (killsEnemy)
        {
            ActivateEnemies();
        }
    }

    public void StartQuest()
    {
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.M_START);
        questManager = FindObjectOfType<QuestManager>();
        questManager.ShowQuestText(title + "\n" + startText);
        questStarted = true;
        if (needsItem)
        {
            ActivateItems();

        }
        if (killsEnemy)
        {
            ActivateEnemies();
        }
    }

    void ActivateItems()
    {
        Object[] items =Resources.FindObjectsOfTypeAll<QuestItem>();
        foreach (QuestItem item in items)
        {
            if (item.questID == questID)
            {
                Debug.Log("Se activa el item");
                item.gameObject.SetActive(true);
            }
        }
    }

    void ActivateEnemies()
    {
        Object[] qenemies = Resources.FindObjectsOfTypeAll<QuestEnemy>();
        foreach (QuestEnemy enemy in qenemies)
        {
            if (enemy.questID == questID)
            {
                enemy.gameObject.SetActive(true);
            }
        }
    }

    public void CompleteQuest()
    {
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.M_END);
        questManager = FindObjectOfType<QuestManager>();
        questManager.ShowQuestText(title + "\n" + completeText);
        if (givesReward)
        {
            int i = 0;
            foreach (string type in rewardType)
            {

                if (type == "Accesory")
                {
                    accManager = FindObjectOfType<AccesoryManager>();
                    notInInventory = accManager.GetAllNotInInvetoryAccesory();

                    foreach (GameObject nacc in notInInventory)
                    {
                        if (nacc.GetComponent<Accesory>().accesoryName == rewardName[i])
                        {
                            nacc.GetComponent<Accesory>().inInventory = true;
                        }
                    }
                }

                if(type == "Skills")
                {
                    FindObjectOfType<UIManager>().ActivateSkillPanel();
                }
                if (type == "Money")
                {
                    FindObjectOfType<MoneyManager>().AddMoney(moneyValue);
                }
                i++;
            }
        }
        questCompleted = true;
        if(nextQuest != null)
        {
            Invoke("ActivateNextQuest", 5.0f);
        }
        gameObject.SetActive(false); //Desactiva la quest y ya no es repetible
    }

    void ActivateNextQuest()
    {
        nextQuest.gameObject.SetActive(true);
        nextQuest.StartQuest();
    }

    public void ResetNumberOfEnemies()
    {
        numberOfEnemies.Clear();
        for (int i = 0; i < numberOfEnemiesConfigured.Count; i++)
        {
            numberOfEnemies.Add(numberOfEnemiesConfigured[i]);
        }

        enemies.Clear();
        for (int i = 0; i < enemiesConfigured.Count; i++)
        {
            enemies.Add(enemiesConfigured[i]);
        }

    }


    public void ResetNumberOfItems()
    {
        for(int i = 0; i < itemsNeededConfigured.Count; i++)
        {
            itemsNeeded.Add(itemsNeededConfigured[i]);
        }
    }


    private void Update()
    {
        if(needsItem && (questManager.itemCollected != null))
        {
            for(int i =0; i < itemsNeeded.Count; i++)
            {
                if(itemsNeeded[i].itemName == questManager.itemCollected.itemName)
                {
                    itemsNeeded.RemoveAt(i);
                    questManager.itemCollected = null;
                    break;
                }
            }
            if(itemsNeeded.Count == 0)
            {
                
                CompleteQuest();
                
            }
        }

        if(killsEnemy && questManager.enemyKilled != null)
        {
            Debug.Log("Tenemos un enemigo recien matado");
            for(int i = 0; i < enemies.Count; i++)
            {
                if(enemies[i].enemyName == questManager.enemyKilled.enemyName)
                {
                    numberOfEnemies[i]--;
                    questManager.enemyKilled = null;
                    if (numberOfEnemies[i] <= 0)
                    {
                        enemies.RemoveAt(i);
                        numberOfEnemies.RemoveAt(i);
                    }
                    break;
                }
            }
            if(enemies.Count == 0)
            {
                CompleteQuest();
            }
        }
    }
}
