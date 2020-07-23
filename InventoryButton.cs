using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    public enum ItemType { WEAPON = 0, ITEM = 1, ARMOR = 2, ACCESORY = 3, SPECIAL_ITEMS = 4};

    public int itemIdx;
    public ItemType type;
    //private UIManager uiManager;
    public Text itemText;


    public void ActivateButton()
    {
        switch (type)
        {
            case ItemType.WEAPON:
                FindObjectOfType<WeaponManager>().ChangeWeapon(itemIdx);
                FindObjectOfType<UIManager>().MenuStatsFill();

                //Limpia antes de mostrar
                FindObjectOfType<UIManager>().ChangeDescriptionText();
                //FindObjectOfType<UIManager>().ChangeDescriptionText(FindObjectOfType<WeaponManager>().GetWeaponAt(itemIdx).weaponName);
                FindObjectOfType<UIManager>().ChangeDescriptionText
                    (
                        FindObjectOfType<WeaponManager>().GetWeaponAt(itemIdx).weaponName,
                        ""+ FindObjectOfType<WeaponManager>().GetWeaponStatsAt(itemIdx).strength,
                        "" + FindObjectOfType<WeaponManager>().GetWeaponStatsAt(itemIdx).defense,
                        "" + FindObjectOfType<WeaponManager>().GetWeaponStatsAt(itemIdx).magicAtt,
                        "" + FindObjectOfType<WeaponManager>().GetWeaponStatsAt(itemIdx).magicDef,
                        "" + FindObjectOfType<WeaponManager>().GetWeaponStatsAt(itemIdx).speed,
                        "" + FindObjectOfType<WeaponManager>().GetWeaponStatsAt(itemIdx).luck,
                        "" + FindObjectOfType<WeaponManager>().GetWeaponStatsAt(itemIdx).accuracy,
                        "" + FindObjectOfType<WeaponManager>().GetWeaponAt(itemIdx).damage
                    );
                break;
            case ItemType.ITEM:
                //Consumir item
                FindObjectOfType<ItemsManager>().UsePotion();
                itemText.text = "" + FindObjectOfType<ItemsManager>().currentPotions;
                break;
            case ItemType.ARMOR:
                Debug.Log("En construccion...");
                break;
            case ItemType.ACCESORY:
                Debug.Log("En construccion...");
                break;
            case ItemType.SPECIAL_ITEMS:
                QuestItem item =FindObjectOfType<ItemsManager>().GetItemAt(itemIdx);
                //Limpia la descripcion;
                FindObjectOfType<UIManager>().ChangeDescriptionText();
                FindObjectOfType<UIManager>().ChangeDescriptionText(item.itemName);
                break;

        }
    }

    void ShowDescription()
    {

    }
}
