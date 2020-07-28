﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using UnityEditor;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Slider playerHealthBar;
    public Text playerHealthText;
    public Text playerLevelText;
    public Slider playerExpBar;
    public Image playerAvatar;

    public HealthManager playerHealthManager;
    public CharacterStats playerStats;
    private WeaponManager weaponManager;
    private ItemsManager itemsManager;
    private ArmorManager armorManager;
    private PlayerController thePlayer;

    private void Start()
    {
        weaponManager = FindObjectOfType<WeaponManager>();
        itemsManager = FindObjectOfType<ItemsManager>();
        armorManager = FindObjectOfType<ArmorManager>();
        thePlayer = FindObjectOfType<PlayerController>();
        inventoryPanel.SetActive(false);
        menuPanel.SetActive(false);
        menuStats.SetActive(false);
        descriptionMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }

        playerHealthBar.maxValue = playerHealthManager.maxHealth;
        playerHealthBar.value = playerHealthManager.Health;
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("HP: ").Append(playerHealthManager.Health).Append("/").Append(playerHealthManager.maxHealth);
        playerHealthText.text = stringBuilder.ToString();

        playerLevelText.text = "Level: " + playerStats.level; //UI LEvel text

        if(playerStats.level >= playerStats.expToLevelUp.Length)
        {
            playerExpBar.enabled = false;
            return;
        }
        playerExpBar.maxValue = playerStats.expToLevelUp[playerStats.level];
        playerExpBar.minValue = playerStats.expToLevelUp[playerStats.level -1];
        playerExpBar.value = playerStats.exp;

        
    }

    public GameObject inventoryPanel, menuPanel, menuStats, descriptionMenu;
    public Button inventoryButton;
    
    //DescriptionBox Info
    public Text descriptionText;
    public Text strTextDesc;
    public Text defTextDesc;
    public Text matTextDesc;
    public Text mdfTextDesc;
    public Text spdTextDesc;
    public Text lckTextDesc;
    public Text accTextDesc;
    public Text baseDamageDesc;

    public void ToggleInventory()
    {
        thePlayer.isTalking = !thePlayer.isTalking;
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.UI_START_MENU_SELECT);
        ChangeDescriptionText();
        inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
        menuPanel.SetActive(!menuPanel.activeInHierarchy);
        menuStats.SetActive(!menuStats.activeInHierarchy);
        descriptionMenu.SetActive(!descriptionMenu.activeInHierarchy);

        if (inventoryPanel.activeInHierarchy)
        {
            Time.timeScale = 0;
            foreach (Transform t in inventoryPanel.transform)
            {
                Destroy(t.gameObject);
            }
            FillInventory();
            MenuStatsFill();
        }
        else { Time.timeScale = 1; }
    }

    public void FillInventory()
    {
        
        List<GameObject> weapons =weaponManager.GetAllWeapons();
        int i = 0;
        foreach(GameObject w in weapons)
        {
            AddItemToInventory(w, InventoryButton.ItemType.WEAPON, i, "");
            i++;
        }

        List<GameObject> keyItems = itemsManager.GetQuestItems();
        i= 0;
        foreach(GameObject item in keyItems)
        {
            AddItemToInventory(item, InventoryButton.ItemType.SPECIAL_ITEMS, i, "");
            i++;
        }

        List<GameObject> armors = armorManager.GetAllArmors();
        i = 0;
        foreach (GameObject a in armors)
        {
            AddItemToInventory(a, InventoryButton.ItemType.ARMOR, i, "");
            i++;
        }

        /*List<GameObject> potionsItems = itemsManager.GetRegularItems();
        i = 0;
        foreach (GameObject ritem in potionsItems)
        {
            AddItemToInventory(ritem, InventoryButton.ItemType.ITEM, i);
            i++;
        }*/

        if (itemsManager.currentPotions > 0)
        {
            //Obtiene del ItemManager el GameObject de la potion, para poder usar su sprite
            GameObject potionObject = itemsManager.potionObject;
            int potionsQuantity = itemsManager.currentPotions;
            i = 0;
            AddItemToInventory(potionObject, InventoryButton.ItemType.ITEM, i, "" + potionsQuantity);
        }
    }

    private void AddItemToInventory(GameObject item, InventoryButton.ItemType type, int pos, string valuetext)
    {
        Button tempB = Instantiate(inventoryButton, inventoryPanel.transform);
        tempB.GetComponent<InventoryButton>().type = type;
        tempB.GetComponent<InventoryButton>().itemIdx = pos;
        tempB.GetComponent<InventoryButton>().itemText.text = valuetext;
        tempB.onClick.AddListener(() => tempB.GetComponent<InventoryButton>().ActivateButton());
        tempB.image.sprite = item.GetComponent<SpriteRenderer>().sprite;
    }


    public void ShowOnly(int type)
    {
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.UI_MENU_SELECT);
        foreach (Transform t in inventoryPanel.transform)
        {
            t.gameObject.SetActive((int)t.GetComponent<InventoryButton>().type == type);

        }
    }

    public void ShowAll()
    {
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.UI_MENU_SELECT);
        foreach (Transform t in inventoryPanel.transform)
        {
            t.gameObject.SetActive(true);
        }
    }

    public void ChangeAvatarImage(Sprite sprite)
    {
        playerAvatar.sprite = sprite;
    }

    public Text levelStatText;
    public Text expStatText;
    public Text exptoNextLevelText;
    public Text strText;
    public Text defText;
    public Text matText;
    public Text mdfText;
    public Text spdText;
    public Text lckText;
    public Text accText;
    public void MenuStatsFill()
    {
        levelStatText.text = "Level: " + playerStats.level;
        expStatText.text = "Exp: " + playerStats.exp;
        exptoNextLevelText.text = "Exp to next level: " + playerStats.expToLevelUp[playerStats.level];

        /*
        strText.text = "STR: " + playerStats.strengthLevels[playerStats.level];
        defText.text = "DEF: " + playerStats.defenseLevels[playerStats.level];
        matText.text = "MAT: " + playerStats.magicAttLevels[playerStats.level];
        mdfText.text = "MDF: " + playerStats.magicDefLevels[playerStats.level];
        spdText.text = "SPD: " + playerStats.speedLevels[playerStats.level];
        lckText.text = "LCK: " + playerStats.luckLevels[playerStats.level];
        accText.text = "ACC: " + playerStats.accuracyLevels[playerStats.level];
        */
        strText.text = "STR: " + playerStats.newstrengthLevels;
        defText.text = "DEF: " + playerStats.newdefenseLevels;
        matText.text = "MAT: " + playerStats.newmagicAttLevels;
        mdfText.text = "MDF: " + playerStats.newmagicDefLevels;
        spdText.text = "SPD: " + playerStats.newspeedLevels;
        lckText.text = "LCK: " + playerStats.newluckLevels;
        accText.text = "ACC: " + playerStats.newaccuracyLevels;
    }
    public void HealthChanged()
    {

    }
    public void LevelChanged()
    {

    }
    public void ExpChanged()
    {

    }

    public void ChangeDescriptionText()
    {
        descriptionText.text = "";
        strTextDesc.text = "";
        defTextDesc.text = "";
        matTextDesc.text = "";
        mdfTextDesc.text = "";
        spdTextDesc.text = "";
        lckTextDesc.text = "";
        accTextDesc.text = "";
        baseDamageDesc.text = "";
    }

    public void ChangeDescriptionText(string description)
    {
        descriptionText.text = "Description: " + description;
    }

    public void ChangeDescriptionText(string desc, string strdesc, string defdesc, string matdesc, string mdfdesc,
                                      string spddesc, string lckdesc, string accdesc, string damagedesc)
    {
        descriptionText.text = "Description: " + desc;
        strTextDesc.text = "STR: " + strdesc;
        defTextDesc.text = "DEF: " + defdesc;
        matTextDesc.text = "MAT: " +matdesc;
        mdfTextDesc.text = "MDF: " +mdfdesc;
        spdTextDesc.text = "SPD: " +spddesc;
        lckTextDesc.text = "LCK: " +lckdesc;
        accTextDesc.text = "ACC: " +accdesc;
        baseDamageDesc.text = "Damage: " +damagedesc;
    }

    /// <summary>
    /// GameOver
    /// </summary>
    public GameObject GameOverPanel;
    public Button newStartButton;
    public Button LoadGameButton;
    public Button ExitGameButton;

    public void LaunchGameOver()
    {
        GameOverPanel.SetActive(true);
    }
    public void NewStart()
    {
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.UI_START_MENU_SELECT);
        SceneManager.LoadScene("MainScreen");
        thePlayer.isDead = false;
        thePlayer.DeactivateDeadAnimation();
        thePlayer.isTalking = true;
        thePlayer.canMove = false;
        weaponManager.ResetAllWeapons();
        thePlayer.GetComponent<CircleCollider2D>().enabled = true;
        CharacterStats thePlayerStats = thePlayer.GetComponent<CharacterStats>();
        thePlayerStats.level = 1;
        thePlayer.GetComponent<HealthManager>().UpdateMaxHealth(thePlayerStats.hpLevels[thePlayerStats.level]);
        GameOverPanel.SetActive(false);

    }

    public void LoadGame()
    {
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.UI_START_MENU_SELECT);
        Debug.Log("En Desarrollo");
    }
    
    public void ExitGame()
    {
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.UI_START_MENU_SELECT);
        Application.Quit();
    }

}
