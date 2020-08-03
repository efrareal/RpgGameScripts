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
                SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.UI_CHANGE_EQ);
                FindObjectOfType<WeaponManager>().ChangeWeapon(itemIdx);
                FindObjectOfType<UIManager>().WeaponEq();
                //FindObjectOfType<UIManager>().MenuStatsFill();
                break;
            case ItemType.ITEM:
                //Consumir item
                SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.USE_ITEM);
                FindObjectOfType<ItemsManager>().UsePotion();
                itemText.text = "" + FindObjectOfType<ItemsManager>().currentPotions;
                break;
            case ItemType.ARMOR:
                SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.UI_CHANGE_EQ);
                FindObjectOfType<ArmorManager>().ChangeArmor(itemIdx);
                FindObjectOfType<UIManager>().ArmorEq();
                break;
            case ItemType.ACCESORY:
                SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.UI_CHANGE_EQ);
                FindObjectOfType<AccesoryManager>().ChangeAccesory(itemIdx);
                FindObjectOfType<UIManager>().AccesoryEq();
                break;
            case ItemType.SPECIAL_ITEMS:
                Debug.Log("Equipar anillo, pendiente");
                break;

        }
        ShowDescription();
    }

    public void ShowDescription()
    {
        switch (type)
        {
            case ItemType.WEAPON:
                SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.UI_MENU_SELECT);
                //Limpia antes de mostrar
                FindObjectOfType<UIManager>().ChangeDescriptionText();
                FindObjectOfType<UIManager>().ChangeDescriptionText
                    (
                        FindObjectOfType<WeaponManager>().GetWeaponAt(itemIdx).weaponName,
                        "" + FindObjectOfType<WeaponManager>().GetWeaponStatsAt(itemIdx).strength,
                        "" + FindObjectOfType<WeaponManager>().GetWeaponStatsAt(itemIdx).defense,
                        "" + FindObjectOfType<WeaponManager>().GetWeaponStatsAt(itemIdx).magicAtt,
                        "" + FindObjectOfType<WeaponManager>().GetWeaponStatsAt(itemIdx).magicDef,
                        "" + FindObjectOfType<WeaponManager>().GetWeaponStatsAt(itemIdx).speed,
                        "" + FindObjectOfType<WeaponManager>().GetWeaponStatsAt(itemIdx).luck,
                        "" + FindObjectOfType<WeaponManager>().GetWeaponStatsAt(itemIdx).accuracy,
                        "" + FindObjectOfType<WeaponManager>().GetWeaponAt(itemIdx).damage
                    );
                //Cambia el panel de Stats del personaje
                FindObjectOfType<UIManager>().MenuStatsFill();
                break;
            case ItemType.ITEM:
                SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.UI_MENU_SELECT);
                //Limpia antes de mostrar
                FindObjectOfType<UIManager>().ChangeDescriptionText();
                FindObjectOfType<UIManager>().ChangeDescriptionText("consumable item");

                break;
            case ItemType.ARMOR:
                SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.UI_MENU_SELECT);
                FindObjectOfType<UIManager>().ChangeDescriptionText();
                FindObjectOfType<UIManager>().ChangeDescriptionText
                    (
                        FindObjectOfType<ArmorManager>().GetArmorAt(itemIdx).armorName,
                        "" + FindObjectOfType<ArmorManager>().GetArmorStatsAt(itemIdx).strength,
                        "" + FindObjectOfType<ArmorManager>().GetArmorStatsAt(itemIdx).defense,
                        "" + FindObjectOfType<ArmorManager>().GetArmorStatsAt(itemIdx).magicAtt,
                        "" + FindObjectOfType<ArmorManager>().GetArmorStatsAt(itemIdx).magicDef,
                        "" + FindObjectOfType<ArmorManager>().GetArmorStatsAt(itemIdx).speed,
                        "" + FindObjectOfType<ArmorManager>().GetArmorStatsAt(itemIdx).luck,
                        "" + FindObjectOfType<ArmorManager>().GetArmorStatsAt(itemIdx).accuracy,
                        "" + 0
                    );
                FindObjectOfType<UIManager>().MenuStatsFill();

                break;
            case ItemType.ACCESORY:
                SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.UI_MENU_SELECT);
                FindObjectOfType<UIManager>().ChangeDescriptionText();
                FindObjectOfType<UIManager>().ChangeDescriptionText
                    (
                        FindObjectOfType<AccesoryManager>().GetAccesoryAt(itemIdx).accesoryName,
                        "" + FindObjectOfType<AccesoryManager>().GetAccesoryStatsAt(itemIdx).strength,
                        "" + FindObjectOfType<AccesoryManager>().GetAccesoryStatsAt(itemIdx).defense,
                        "" + FindObjectOfType<AccesoryManager>().GetAccesoryStatsAt(itemIdx).magicAtt,
                        "" + FindObjectOfType<AccesoryManager>().GetAccesoryStatsAt(itemIdx).magicDef,
                        "" + FindObjectOfType<AccesoryManager>().GetAccesoryStatsAt(itemIdx).speed,
                        "" + FindObjectOfType<AccesoryManager>().GetAccesoryStatsAt(itemIdx).luck,
                        "" + FindObjectOfType<AccesoryManager>().GetAccesoryStatsAt(itemIdx).accuracy,
                        "" + 0
                    );
                FindObjectOfType<UIManager>().MenuStatsFill();
                break;
            case ItemType.SPECIAL_ITEMS:
                SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.UI_MENU_SELECT);
                QuestItem item = FindObjectOfType<ItemsManager>().GetItemAt(itemIdx);
                //Limpia la descripcion;
                FindObjectOfType<UIManager>().ChangeDescriptionText();
                FindObjectOfType<UIManager>().ChangeDescriptionText(item.itemName);
                break;

        }
    }

    public void ClearDescription()
    {
        //Limpia la descripcion cuando no estas sobre el boton
        FindObjectOfType<UIManager>().ChangeDescriptionText();
    }
}
