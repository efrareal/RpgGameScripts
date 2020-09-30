using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chest : MonoBehaviour
{
    public string rewardType;
    public string rewardWeaponName;
    public int goldValue;
    
    private WeaponManager weaponManager;
    private ArmorManager armorManager;
    private ItemsManager itemsManager;
    private MoneyManager moneyManager;
    private AccesoryManager accManager;
    private List<GameObject> notInInventory;
    public string chestID;
    public string chestText;
    public Sprite rewardSprite;

    private Animator _animator;
    private DialogueManager dialogueManager;

    public GameObject questItem;



    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        _animator = GetComponent<Animator>();
        weaponManager = FindObjectOfType<WeaponManager>();
        armorManager = FindObjectOfType<ArmorManager>();
        itemsManager = FindObjectOfType<ItemsManager>();
        moneyManager = FindObjectOfType<MoneyManager>();
        accManager = FindObjectOfType<AccesoryManager>();

        if (PlayerPrefs.GetString(chestID)== "" || PlayerPrefs.GetString(chestID) == "closed")
        {
            PlayerPrefs.SetString(chestID, "closed");
        }
        else { _animator.SetBool("isOpened", true);
            PlayerPrefs.SetString(chestID, "opened");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Player") && Input.GetMouseButtonDown(1))
        {
            if (PlayerPrefs.GetString(chestID) == "closed")
            {
                
                _animator.SetBool("isOpened", true);
                PlayerPrefs.SetString(chestID, "opened");
                itemsManager.chestsDict.Add(chestID, "opened");
                Debug.Log(itemsManager.chestsDict[chestID]);
                SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.RECEIVE_ITEM);
                dialogueManager.ShowDialogue(new string[] { "Chest: \n" + chestText }, rewardSprite);

                if(rewardType == "Gold")
                {
                    moneyManager.AddMoney(goldValue);
                }

                if(rewardType == "QuestItem")
                {
                    itemsManager.AddQuestItem(questItem.GetComponent<QuestItem>().itemName);
                }

                if(rewardType == "Potion")
                {
                    itemsManager.AddPotions(1);
                }

                if (rewardType == "Ether")
                {
                    itemsManager.AddEthers(1);
                }

                if(rewardType == "PD")
                {
                    itemsManager.AddPD(1);
                }

                if (rewardType == "Weapon")
                {
                    notInInventory = weaponManager.GetAllNotInInvetoryWeapons();
                    foreach (GameObject nweapon in notInInventory)
                    {
                        if (nweapon.GetComponent<WeaponDamage>().weaponName == rewardWeaponName)
                        {
                            nweapon.GetComponent<WeaponDamage>().inInventory = true;
                        }
                    }
                    
                }
                if(rewardType == "Armor")
                {
                    notInInventory = armorManager.GetAllNotInInvetoryArmors();
                    foreach (GameObject narmor in notInInventory)
                    {
                        if (narmor.GetComponent<Armor>().armorName == rewardWeaponName)
                        {
                            narmor.GetComponent<Armor>().inInventory = true;
                        }
                    }

                }
                if (rewardType == "Accesory")
                {
                    notInInventory = accManager.GetAllNotInInvetoryAccesory();

                    foreach (GameObject nacc in notInInventory)
                    {
                        if (nacc.GetComponent<Accesory>().accesoryName == rewardWeaponName)
                        {
                            nacc.GetComponent<Accesory>().inInventory = true;
                        }
                    }

                }
            }

        }
    }
}
