using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public int currentPotions;
    private HealthManager playerHealthManager;
    public int hpPointsValue= 5;
    private int potionvalue = 150;
    public GameObject potionObject;

    public int currentEthers;
    private MPManager playerMPManager;
    public int etherValue = 20;
    public GameObject etherObject;

    public int currentPhoenixDown;
    public GameObject phoenixDownObject;

    public Dictionary<string, string> chestsDict = new Dictionary<string, string>();

    private void Start()
    {
        playerHealthManager = GetComponentInParent<HealthManager>();
        playerMPManager = GetComponentInParent<MPManager>();
    }


    public List<GameObject> questItems = new List<GameObject>();
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

    public void AddPD(int pd)
    {
        currentPhoenixDown += pd;
    }

    public void UsePD()
    {
        if (currentPhoenixDown <= 0)
        {
            currentPhoenixDown = 0;
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.UI_MENU_ERROR);
            return;
        }
        currentPhoenixDown--;
        playerHealthManager.AddHealthPoints(50);
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.USE_ITEM);
        if (currentPhoenixDown < 0)
        {
            currentPhoenixDown = 0;
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
        List<GameObject> questItemInInventory = new List<GameObject>();
        foreach(GameObject qitem in questItems)
        {
            if (qitem.GetComponent<QuestItem>().inInventory)
            {
                questItemInInventory.Add(qitem);
            }
        }
        return questItemInInventory;
    }

    public List<string> GetQuestItemsByName()
    {
        List<string> listQI = new List<string>();
        foreach(GameObject g in questItems)
        {
            if (g.GetComponent<QuestItem>().inInventory)
            {
                listQI.Add(g.GetComponent<QuestItem>().itemName);
            }
        }

        return listQI;
    }

    public QuestItem GetItemAt(int idx)
    {
        return questItems[idx].GetComponent<QuestItem>();
    }

    public bool HasTheQuestItem(QuestItem item)
    {
        foreach(GameObject qi in questItems)
        {
            if((qi.GetComponent<QuestItem>().itemName == item.itemName) && qi.GetComponent<QuestItem>().inInventory)
            {
                return true;
            }
        }
        return false;
    }

    public void AddQuestItem(string iname)
    {
        foreach (GameObject qitem in questItems)
        {
            if (qitem.GetComponent<QuestItem>().itemName == iname)
            {
                qitem.GetComponent<QuestItem>().inInventory = true;
            }
        }

    }

    public void RemoveQuestItems()
    {
        foreach (GameObject qitem in questItems)
        {
             qitem.GetComponent<QuestItem>().inInventory = false;

        }
    }

    public void AddQuestItemByName(string itemname)
    {
        foreach (GameObject item in questItems)
        {
            if (item.GetComponent<QuestItem>().itemName == itemname)
            {
                item.GetComponent<QuestItem>().inInventory = true;
            }
        }
    }
    

}
