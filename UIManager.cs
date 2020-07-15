﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using UnityEditor;

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

    private void Start()
    {
        weaponManager = FindObjectOfType<WeaponManager>();
        itemsManager = FindObjectOfType<ItemsManager>();
        inventoryPanel.SetActive(false);
        menuPanel.SetActive(false);
        menuStats.SetActive(false);
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

    public GameObject inventoryPanel, menuPanel, menuStats;
    public Button inventoryButton;
    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
        menuPanel.SetActive(!menuPanel.activeInHierarchy);
        menuStats.SetActive(!menuStats.activeInHierarchy);
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
            AddItemToInventory(w, InventoryButton.ItemType.WEAPON, i);
            i++;
        }

        List<GameObject> keyItems = itemsManager.GetQuestItems();
        i= 0;
        foreach(GameObject item in keyItems)
        {
            AddItemToInventory(item, InventoryButton.ItemType.SPECIAL_ITEMS, i);
            i++;
        }

        List<GameObject> potionsItems = itemsManager.GetRegularItems();
        i = 0;
        foreach (GameObject ritem in potionsItems)
        {
            AddItemToInventory(ritem, InventoryButton.ItemType.ITEM, i);
            i++;
        }
    }

    private void AddItemToInventory(GameObject item, InventoryButton.ItemType type, int pos)
    {
        Button tempB = Instantiate(inventoryButton, inventoryPanel.transform);
        tempB.GetComponent<InventoryButton>().type = type;
        tempB.GetComponent<InventoryButton>().itemIdx = pos;
        tempB.onClick.AddListener(() => tempB.GetComponent<InventoryButton>().ActivateButton());
        tempB.image.sprite = item.GetComponent<SpriteRenderer>().sprite;
    }

    public void ShowOnly(int type)
    {
        foreach(Transform t in inventoryPanel.transform)
        {
            t.gameObject.SetActive((int)t.GetComponent<InventoryButton>().type == type);

        }
    }

    public void ShowAll()
    {
        foreach(Transform t in inventoryPanel.transform)
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

        strText.text = "STR: " + playerStats.strengthLevels[playerStats.level];
        defText.text = "DEF: " + playerStats.defenseLevels[playerStats.level];
        matText.text = "MAT: " + playerStats.magicAttLevels[playerStats.level];
        mdfText.text = "MDF: " + playerStats.magicDefLevels[playerStats.level];
        spdText.text = "SPD: " + playerStats.speedLevels[playerStats.level];
        lckText.text = "LCK: " + playerStats.luckLevels[playerStats.level];
        accText.text = "ACC: " + playerStats.accuracyLevels[playerStats.level];
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
}
