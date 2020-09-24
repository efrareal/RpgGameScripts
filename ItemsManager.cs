using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public int currentPotions;
    private HealthManager playerHealthManager;
    public int hpPointsValue= 5;
    private int potionvalue = 100;
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
        return questItems;
    }

    public List<string> GetQuestItemsByName()
    {
        List<string> listQI = new List<string>();
        foreach(GameObject g in questItems)
        {
            listQI.Add(g.GetComponent<QuestItem>().itemName);
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
        newItem.transform.parent = this.transform;
        newItem.SetActive(false);
    }

    public void RemoveQuestItems()
    {
        
        foreach(Transform t in transform)
        {
            if(t.gameObject.GetComponent<QuestItem>() != null)
            {
                Destroy(t.gameObject);
            }
            
        }

        questItems.Clear();
        /*GameObject go = gameObject.transform.Find("Special Material Variant").gameObject;
        if(go != null)
        {
            Destroy(go);
        }*/

    }

    public void AddQuestItemByName(string itemname)
    {
        Object[] items = Resources.FindObjectsOfTypeAll<QuestItem>();
        foreach (QuestItem item in items)
        {
            Debug.Log("Item guardado: " + itemname);
            Debug.Log("Item En Resources: " + item.itemName);
            if (item.itemName == itemname)
            {
                Debug.Log("Item En resources: " + item.itemName + "es igual al item por parametro: " + itemname);
                questItems.Add(item.gameObject);
            }
        }
    }
    

}
