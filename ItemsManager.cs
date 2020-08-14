using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public int currentPotions;
    private HealthManager playerHealthManager;
    public int hpPointsValue= 5;
    public int potionvalue = 50;
    public GameObject potionObject;

    public int currentEthers;
    private MPManager playerMPManager;
    public int etherValue = 20;
    public GameObject etherObject;

    private void Start()
    {
        playerHealthManager = GetComponentInParent<HealthManager>();
        playerMPManager = GetComponentInParent<MPManager>();
    }


    private List<GameObject> questItems = new List<GameObject>();
    private List<GameObject> regularItems = new List<GameObject>();

    public void AddPotions(int potionCollected)
    {
        currentPotions += potionCollected;
        //regularItems.Add(potion);
    }

    public void UsePotion()
    {
        if (currentPotions <= 0)
        {
            currentPotions = 0;
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.UI_MENU_ERROR);
            return;
        }
        currentPotions--;
        playerHealthManager.AddHealthPoints(potionvalue);
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.USE_ITEM);
        if (currentPotions < 0)
        {
            currentPotions = 0;
        }
    }

    public void AddEthers(int etherCollected)
    {
        currentEthers += etherCollected;
    }

    public void UseEther()
    {
        if(currentEthers <= 0)
        {
            currentEthers = 0;
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.UI_MENU_ERROR);
            return;
        }
        currentEthers--;
        playerMPManager.AddMPPoints(etherValue);
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.USE_ITEM);
        if (currentEthers < 0)
        {
            currentEthers = 0;
        }
    }

    public List<GameObject> GetRegularItems()
    {
        return regularItems;
    }

    public GameObject GetRegularItemAt(int pos)
    {
        return regularItems[pos];
    }

    public void AddHP(int hpvalue)
    {
        
        playerHealthManager.AddHealthPoints(hpvalue);
    }

    public List<GameObject> GetQuestItems()
    {
        return questItems;
    }

    public QuestItem GetItemAt(int idx)
    {
        return questItems[idx].GetComponent<QuestItem>();
    }

    public bool HasTheQuestItem(QuestItem item)
    {
        foreach(GameObject qi in questItems)
        {
            if(qi.GetComponent<QuestItem>().itemName == item.itemName)
            {
                return true;
            }
        }
        return false;
    }

    public void AddQuestItem(GameObject newItem)
    {
        questItems.Add(newItem);
    }
}
