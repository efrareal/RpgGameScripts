using System.Collections;
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
    public Slider playerMPBar;
    public Text playerMPText;
    public GameObject hudObject;

    public HealthManager playerHealthManager;
    public MPManager playerMPManager;
    public CharacterStats playerStats;
    private WeaponManager weaponManager;
    private ItemsManager itemsManager;
    private ArmorManager armorManager;
    private AccesoryManager accesoryManager;
    private PlayerController thePlayer;
    private MoneyManager moneyManager;

    private void Start()
    {
        weaponManager = FindObjectOfType<WeaponManager>();
        itemsManager = FindObjectOfType<ItemsManager>();
        armorManager = FindObjectOfType<ArmorManager>();
        accesoryManager = FindObjectOfType<AccesoryManager>();
        thePlayer = FindObjectOfType<PlayerController>();
        moneyManager = FindObjectOfType<MoneyManager>();
        inventoryPanel.SetActive(false);
        menuPanel.SetActive(false);
        menuStats.SetActive(false);
        descriptionMenu.SetActive(false);
    }

    public Image mpFillBar;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(thePlayer.isTalking || thePlayer.isDead || thePlayer.inTransition || !thePlayer.canMove)
            {
                return;
            }
            ToggleInventory();
        }

        playerHealthBar.maxValue = playerHealthManager.maxHealth;
        playerHealthBar.value = playerHealthManager.Health;
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("HP: ").Append(playerHealthManager.Health).Append("/").Append(playerHealthManager.maxHealth);
        playerHealthText.text = stringBuilder.ToString();

        if (playerMPManager.mpCharge || playerMPManager.MagicPoints <=0)
        {
            mpFillBar.color = Color.magenta;
            playerMPBar.maxValue = playerMPManager.mpChargerTime;
            playerMPBar.value = playerMPManager.timeCounter;
            StringBuilder stringBuilder3 = new StringBuilder();
            stringBuilder3.Append("Charging: ").Append((int)playerMPManager.timeCounter).Append("/").Append(playerMPManager.mpChargerTime);
            playerMPText.text = stringBuilder3.ToString();
        }
        else
        {
            mpFillBar.color = Color.blue;
            playerMPBar.maxValue = playerMPManager.maxMP;
            playerMPBar.value = playerMPManager.MagicPoints;
            StringBuilder stringBuilder2 = new StringBuilder();
            stringBuilder2.Append("MP: ").Append(playerMPManager.MagicPoints).Append("/").Append(playerMPManager.maxMP);
            playerMPText.text = stringBuilder2.ToString();
        }

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
        if (thePlayer.isTalking || thePlayer.isDead || thePlayer.inTransition || !thePlayer.canMove)
        {
            return;
        }
        thePlayer.inInventory = !thePlayer.inInventory;
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

        List<GameObject> accesories = accesoryManager.GetAllAccesories();
        i = 0;
        foreach (GameObject ac in accesories)
        {
            AddItemToInventory(ac, InventoryButton.ItemType.ACCESORY, i, "");
            i++;
        }

        //Potions
        if (itemsManager.currentPotions > 0)
        {
            //Obtiene del ItemManager el GameObject de la potion, para poder usar su sprite
            GameObject potionObject = itemsManager.potionObject;
            int potionsQuantity = itemsManager.currentPotions;
            i = 0;
            AddItemToInventory(potionObject, InventoryButton.ItemType.ITEM, i, "" + potionsQuantity);
        }

        //Ethers
        if (itemsManager.currentEthers > 0)
        {
            //Obtiene del ItemManager el GameObject de la potion, para poder usar su sprite
            GameObject etherObject = itemsManager.etherObject;
            int ethersQuantity = itemsManager.currentEthers;
            i = 1;
            AddItemToInventory(etherObject, InventoryButton.ItemType.ITEM, i, "" + ethersQuantity);
        }

        //PDs
        if (itemsManager.currentPhoenixDown > 0)
        {
            //Obtiene del ItemManager el GameObject de la potion, para poder usar su sprite
            GameObject pdObject = itemsManager.phoenixDownObject;
            int pdQuantity = itemsManager.currentPhoenixDown;
            i = 2;
            AddItemToInventory(pdObject, InventoryButton.ItemType.ITEM, i, "" + pdQuantity);
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
    public Text weaponEq;
    public Text armorEq;
    public Text accesoryEq;

    public void WeaponEq()
    {
        weaponEq.text = "Weapon: " + weaponManager.GetWeaponAt(weaponManager.activeWeapon).weaponName;
    }

    public void ArmorEq()
    {
        armorEq.text = "Armor: " + armorManager.GetArmorAt(armorManager.activeArmor).armorName;
    }

    public void AccesoryEq()
    {
        accesoryEq.text = "Accesory: " + accesoryManager.GetAccesoryAt(accesoryManager.activeAccesory).accesoryName;
    }
    public void MenuStatsFill()
    {
        levelStatText.text = "Level: " + playerStats.level;
        expStatText.text = "Exp: " + playerStats.exp;
        exptoNextLevelText.text = "Exp to next level: " + playerStats.expToLevelUp[playerStats.level];




        strText.text = "STR: " + playerStats.newstrengthLevels;
        defText.text = "DEF: " + playerStats.newdefenseLevels;
        matText.text = "MAT: " + playerStats.newmagicAttLevels;
        mdfText.text = "MDF: " + playerStats.newmagicDefLevels;
        spdText.text = "SPD: " + playerStats.newspeedLevels;
        lckText.text = "LCK: " + playerStats.newluckLevels;
        accText.text = "ACC: " + playerStats.newaccuracyLevels;
    }

    /// <summary>
    /// HUD
    /// </summary>

    public void LevelChanged(int newlevel, int expToLevelUpLength, int maxValue, int minValue)
    {
        playerLevelText.text = "Level: " + newlevel; //UI LEvel text

        if (newlevel >= expToLevelUpLength)
        {
            playerExpBar.enabled = false;
            return;
        }
        playerExpBar.maxValue = maxValue;
        playerExpBar.minValue = minValue;

    }
    public void ExpChanged(int exp)
    {
        playerExpBar.value = playerStats.exp;
    }

    /// <summary>
    /// Inventory Description
    /// </summary>
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
    /// Skills Panel
    /// </summary>
    public GameObject skillPanel;
    //public Button dashSkill;
    //public Button fireSkill;
    //public Button thunderSkill;
    //public Button iceSkill;
    public List<Button> skillList = new List<Button>();

    public void ActivateSkill(string skillname)
    {
        foreach(Button skill in skillList)
        {
            if(skill.GetComponent<SkillButton>().type.ToString() == skillname)
            {
                skill.gameObject.SetActive(true);
                skill.GetComponent<SkillButton>().inInventory = true;
            }
        }
    }

    public List<string> ReturnAllActiveSkills()
    {
        List<string> activeSkillList = new List<string>();
        foreach (Button skill in skillList)
        {
            if (skill.GetComponent<SkillButton>().inInventory)
            {
                activeSkillList.Add(skill.GetComponent<SkillButton>().type.ToString());
            }
        }
        return activeSkillList;
    }

    public void ResetAllSkills()
    {
        foreach (Button skill in skillList)
        {
            skill.gameObject.SetActive(false);
            skill.GetComponent<SkillButton>().inInventory = false;
        }
    }
    public void ActivateSkillPanel()
    {
        skillPanel.SetActive(true);
    }



    /// <summary>
    /// GameOver
    /// </summary>
    public GameObject GameOverPanel;
    public Button newStartButton;
    public Button phoenixDownButton;
    public Button ExitGameButton;

    public void LaunchGameOver()
    {
        GameOverPanel.SetActive(true);
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.GAME_OVER);
        FindObjectOfType<AudioManager>().audioCanBePlayed = false;
    }


    public void NewStart()
    {
        //ResetPlayer();
        thePlayer.nextUuid = "StartGame";
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.UI_START_MENU_SELECT);

        FindObjectOfType<SceneTransition>().Transition("MainScreen");

        GameOverPanel.SetActive(false);
        FindObjectOfType<AudioManager>().audioCanBePlayed = true;
    }
    
    public void ExitGame()
    {
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.UI_START_MENU_SELECT);
        Application.Quit();
    }

    public void PhoenixDown()
    {
        if (itemsManager.currentPhoenixDown <= 0)
        {
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.UI_MENU_ERROR);
            return;
        }
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.UI_START_MENU_SELECT);
        itemsManager.UsePD();
        thePlayer.isDead = false;
        thePlayer.DeactivateDeadAnimation();
        thePlayer.isTalking = false;
        thePlayer.canMove = true;
        thePlayer.GetComponent<CircleCollider2D>().enabled = true;

        GameOverPanel.SetActive(false);
        FindObjectOfType<AudioManager>().audioCanBePlayed = true;
    }

    void ResetPlayer()
    {
        thePlayer.isDead = false;
        thePlayer.DeactivateDeadAnimation();
        thePlayer.isTalking = true;
        thePlayer.canMove = false;
        weaponManager.DeactivateWeapon(false);
        thePlayer.nextUuid = "StartGame";
        CharacterStats thePlayerStats = thePlayer.GetComponent<CharacterStats>();
        thePlayerStats.level = 1;
        thePlayerStats.exp = 0;
        ExpChanged(thePlayerStats.exp);
        thePlayerStats.InitialStats();
        thePlayer.GetComponent<HealthManager>().UpdateMaxHealth(thePlayerStats.hpLevels[thePlayerStats.level]);
        thePlayer.GetComponent<MPManager>().UpdateMaxMP(thePlayerStats.mpLevels[thePlayerStats.level]);
        LevelChanged(thePlayerStats.level, thePlayerStats.expToLevelUp.Length,
                                   thePlayerStats.expToLevelUp[thePlayerStats.level],
                                   thePlayerStats.expToLevelUp[thePlayerStats.level - 1]);



        moneyManager.ResetMoney();
        itemsManager.currentEthers = 0;
        itemsManager.currentPotions = 0;
        itemsManager.currentPhoenixDown = 0;
        itemsManager.RemoveQuestItems();

        FindObjectOfType<QuestManager>().ResetAllQuests();
        ResetAllSkills();

        weaponManager.ResetAllWeapons();
        armorManager.ResetAllArmors();
        accesoryManager.ResetAllAccesories();
    }
}
