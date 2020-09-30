using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class MainScreen : MonoBehaviour
{
    public Button startGameButton;
    public Button exitGameButton;
    private PlayerController thePlayer;

    private UIManager uiManager;
    private bool alreadyLoading;


    private HealthManager healthManager;
    private CharacterStats stats;
    private MPManager mpManager;
    private ItemsManager itemsManager;
    private WeaponManager weaponManager;
    private ArmorManager armorManager;
    private AccesoryManager accesoryManager;
    private QuestManager questManager;
    private SceneTransition sceneTransition;
    private MoneyManager moneyManager;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        uiManager = FindObjectOfType<UIManager>();
        //uiManager.ToggleHUD();

        healthManager = GameObject.Find("Player").GetComponent<HealthManager>();
        stats = GameObject.Find("Player").GetComponent<CharacterStats>();
        mpManager = GameObject.Find("Player").GetComponent<MPManager>();
        itemsManager = FindObjectOfType<ItemsManager>();
        weaponManager = FindObjectOfType<WeaponManager>();
        armorManager = FindObjectOfType<ArmorManager>();
        accesoryManager = FindObjectOfType<AccesoryManager>();
        itemsManager = FindObjectOfType<ItemsManager>();
        questManager = FindObjectOfType<QuestManager>();
        sceneTransition = FindObjectOfType<SceneTransition>();
        uiManager = FindObjectOfType<UIManager>();
        moneyManager = FindObjectOfType<MoneyManager>();

        alreadyLoading = false;

    }

    public void StartGame()
    {
        ResetPlayer();
        PlayerPrefs.DeleteAll();
        if (File.Exists(Application.persistentDataPath + "/slot1.dat"))
        {
            File.Delete(Application.persistentDataPath + "/slot1.dat");
        }
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.UI_START_MENU_SELECT);
        thePlayer.isTalking = false;
        thePlayer.canMove = true;
        //SceneManager.LoadScene("Kings Room");
        /*stats.AddStatsAtLevelUP(0);
        stats.AddStatsToCharacter(stats.strengthLevels[stats.level], stats.defenseLevels[stats.level],
                                  stats.magicAttLevels[stats.level], stats.magicDefLevels[stats.level],
                                  stats.speedLevels[stats.level], stats.luckLevels[stats.level], stats.accuracyLevels[stats.level]);*/

        weaponManager.DeactivateWeapon(true);
        weaponManager.ResetToInitialWeapon();
        armorManager.ResetArmorsToInitial();
        accesoryManager.ResetAccesoriesToInitial();

        uiManager.WeaponEq();
        //armorManager.ChangeArmor(0);
        uiManager.ArmorEq();
        //accesoryManager.ChangeAccesory(0);
        uiManager.AccesoryEq();
        sceneTransition.Transition("Kings Room");
        //uiManager.ToggleHUD();

    }

    public void LoadGame()
    {
        if (!alreadyLoading)
        {
            ResetPlayer();
            Load();
            alreadyLoading = true;
        }
        
    }

    public void ExitGame()
    {
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.UI_START_MENU_SELECT);
        Application.Quit();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/slot1.dat"))
        {
            thePlayer.isTalking = false;
            thePlayer.canMove = true;
            PlayerPrefs.DeleteAll();
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.UI_START_MENU_SELECT);

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/slot1.dat", FileMode.Open);

            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            thePlayer.nextUuid = data.savePointId;
            stats.level = data.level;
            moneyManager.AddMoney(data.gold);
            healthManager.UpdateMaxHealth(stats.hpLevels[data.level]);
            mpManager.UpdateMaxMP(stats.mpLevels[data.level]);
            uiManager.LevelChanged(stats.level, stats.expToLevelUp.Length,
                                   stats.expToLevelUp[stats.level], 
                                   stats.expToLevelUp[stats.level -1]);
            stats.exp = data.exp;
            uiManager.ExpChanged(data.exp);
            
            stats.newstrengthLevels = data.str;
            stats.newdefenseLevels = data.def;
            stats.newmagicAttLevels = data.mat;
            stats.newmagicDefLevels = data.mdf;
            stats.newspeedLevels = data.spd;
            stats.newluckLevels = data.lck;
            stats.newaccuracyLevels = data.acc;


            //stats.AddStatsAtLevelUP(0);
            mpManager.ChangeMP(data.mp);
            healthManager.ChangeHealth(data.health);
            itemsManager.currentPotions = data.potions;
            itemsManager.currentEthers = data.ethers;
            itemsManager.currentPhoenixDown = data.pd;

            //Recupera weapons
            for(int i = 0; i < data.weaponsInInventory.Count; i++)
            {
                weaponManager.SearchWeaponByName(data.weaponsInInventory[i]);
            }
            //Activa el arma que tenia activa
            //weaponManager.ChangeWeapon(data.activeWeapon);
            weaponManager.activeWeapon = data.activeWeapon;
            weaponManager.DeactivateWeapon(true);
            uiManager.WeaponEq();
            //Recupera Armors
            for (int i = 0; i < data.armorsInInventory.Count; i++)
            {
                armorManager.SearchArmorByName(data.armorsInInventory[i]);
            }
            //armorManager.ChangeArmor(data.activeArmor);
            armorManager.activeArmor = data.activeArmor;
            armorManager.ActivateArmor(true);
            uiManager.ArmorEq();
            //Recupera Accesorios
            for (int i = 0; i < data.accesoriesInInventory.Count; i++)
            {
                accesoryManager.SearchAccByName(data.accesoriesInInventory[i]);
            }
            //accesoryManager.ChangeAccesory(data.activeAccesory);
            accesoryManager.activeAccesory = data.activeAccesory;
            accesoryManager.ActivateAcc(true);
            uiManager.AccesoryEq();

            //Recupera quests terminadas
            for (int i = 0; i < data.questsCompleted.Count; i++)
            {
                questManager.SearchQuestByIDSetCompleted(data.questsCompleted[i]);
            }

            //Recupera quests arrancadas
            for (int i = 0; i < data.questsStarted.Count; i++)
            {
                questManager.SearchQuestByIDSetStarted(data.questsStarted[i]);
            }

            //Recupera Items Quest
            
            for (int i = 0; i < data.questItems.Count; i++)
            {
                //Debug.Log(data.questItems[i]);
                itemsManager.AddQuestItemByName(data.questItems[i]);
            }

            //Recupera skills activas
            for (int i = 0; i < data.activeSkills.Count; i++)
            {
                uiManager.ActivateSkill(data.activeSkills[i]);
            }

            itemsManager.chestsDict = data.chests;
            // Recupera los chests
            foreach (KeyValuePair<string, string> entry in data.chests)
            {
                PlayerPrefs.SetString(entry.Key, entry.Value);
            }

            sceneTransition.Transition(data.sceneName);
            //SceneManager.LoadScene(data.sceneName)
        }
        else
        {
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.UI_MENU_ERROR);
        }

    }

    void ResetPlayer()
    {
        thePlayer.isDead = false;
        thePlayer.DeactivateDeadAnimation();
        thePlayer.isTalking = true;
        thePlayer.canMove = false;
        weaponManager.DeactivateWeapon(false);
        //thePlayer.nextUuid = "StartGame";
        CharacterStats thePlayerStats = thePlayer.GetComponent<CharacterStats>();
        thePlayerStats.level = 1;
        thePlayerStats.exp = 0;
        uiManager.ExpChanged(thePlayerStats.exp);
        thePlayerStats.InitialStats();
        thePlayer.GetComponent<HealthManager>().UpdateMaxHealth(thePlayerStats.hpLevels[thePlayerStats.level]);
        thePlayer.GetComponent<MPManager>().UpdateMaxMP(thePlayerStats.mpLevels[thePlayerStats.level]);
        uiManager.LevelChanged(thePlayerStats.level, thePlayerStats.expToLevelUp.Length,
                                   thePlayerStats.expToLevelUp[thePlayerStats.level],
                                   thePlayerStats.expToLevelUp[thePlayerStats.level - 1]);



        moneyManager.ResetMoney();
        itemsManager.currentEthers = 0;
        itemsManager.currentPotions = 0;
        itemsManager.currentPhoenixDown = 0;
        itemsManager.RemoveQuestItems();

        FindObjectOfType<QuestManager>().ResetAllQuests();
        uiManager.ResetAllSkills();

        weaponManager.ResetAllWeapons();
        armorManager.ResetAllArmors();
        accesoryManager.ResetAllAccesories();
    }
}
