using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    public enum ItemType { WEAPON, ITEM, ARMOR, ACCESORY};

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
                Debug.Log("En construccion...");
                break;
            case ItemType.ARMOR:
                Debug.Log("En construccion...");
                break;
            case ItemType.ACCESORY:
                Debug.Log("En construccion...");
                break;

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
