using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    public enum ItemType { WEAPON = 0, ITEM = 1, ARMOR = 2, ACCESORY = 3, SPECIAL_ITEMS = 4};

    public int itemIdx;
    public ItemType type;

    public void ActivateButton()
    {
        switch (type)
        {
            case ItemType.WEAPON:
                FindObjectOfType<WeaponManager>().ChangeWeapon(itemIdx);
                break;
            case ItemType.ITEM:
                //Consumir item
                FindObjectOfType<ItemsManager>().UsePotion(itemIdx);
                Destroy(this.gameObject);
                break;
            case ItemType.ARMOR:
                Debug.Log("En construccion...");
                break;
            case ItemType.ACCESORY:
                Debug.Log("En construccion...");
                break;
            case ItemType.SPECIAL_ITEMS:
                QuestItem item =FindObjectOfType<ItemsManager>().GetItemAt(itemIdx);
                Debug.Log(item.itemName);
                break;

        }
    }
}
