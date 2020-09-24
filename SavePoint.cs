using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Data.Common;

public class SavePoint : MonoBehaviour
{
    public string savePointId;
    public string currentScene;

    private PlayerController player;
    private CameraFollow theCamera;

    private HealthManager healthManager;
    private CharacterStats stats;
    private MPManager mpManager;
    private ItemsManager itemsManager;
    private WeaponManager weaponManager;
    private ArmorManager armorManager;
    private AccesoryManager accesoryManager;
    private QuestManager questManager;
    private MoneyManager moneyManager;
    private UIManager uiManager;

    public GameObject displayInfo;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        theCamera = FindObjectOfType<CameraFollow>();

        healthManager = GameObject.Find("Player").GetComponent<HealthManager>();
        stats = GameObject.Find("Player").GetComponent<CharacterStats>();
        mpManager = GameObject.Find("Player").GetComponent<MPManager>();
        itemsManager = FindObjectOfType<ItemsManager>();
        weaponManager = FindObjectOfType<WeaponManager>();
        armorManager = FindObjectOfType<ArmorManager>();
        accesoryManager = FindObjectOfType<AccesoryManager>();
        itemsManager = FindObjectOfType<ItemsManager>();
        questManager = FindObjectOfType<QuestManager>();
        moneyManager = FindObjectOfType<MoneyManager>();
        uiManager = FindObjectOfType<UIManager>();

        if (!player.nextUuid.Equals(savePointId))
        {
            return;
        }
        player.transform.position = this.transform.position;
        theCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, theCamera.transform.position.z);

    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/slot1.dat");

        PlayerData data = new PlayerData();
        data.savePointId = savePointId;
        data.sceneName = currentScene;
        data.gold = moneyManager.currentMoney;
        data.health = healthManager.Health;
        data.level = stats.level;
        data.exp = stats.exp;
        data.mp = mpManager.MagicPoints;
        data.potions = itemsManager.currentPotions;
        data.ethers = itemsManager.currentEthers;
        data.pd = itemsManager.currentPhoenixDown;

        List<string> weapons = weaponManager.GetAllWeaponsName();
        data.weaponsInInventory = weapons;
        data.activeWeapon = weaponManager.activeWeapon;

        List<string> armors = armorManager.GetAllArmorsName();
        data.armorsInInventory = armors;
        data.activeArmor = armorManager.activeArmor;

        List<string> accs = accesoryManager.GetAllAccsName();
        data.accesoriesInInventory = accs;
        data.activeAccesory = accesoryManager.activeAccesory;

        List<int> qsStarted = questManager.QuestsStarted();
        data.questsStarted = qsStarted;

        List<int> qsCompleted = questManager.QuestsCompleted();
        data.questsCompleted = qsCompleted;

        List<string> qItems = itemsManager.GetQuestItemsByName();
        data.questItems = qItems;

        for (int i = 0; i < qItems.Count; i++)
        {
            Debug.Log(qItems[i]);
        }


        data.str = stats.newstrengthLevels;
        data.def = stats.newdefenseLevels;
        data.mat = stats.newmagicAttLevels;
        data.mdf = stats.newmagicDefLevels;
        data.spd = stats.newspeedLevels;
        data.lck = stats.newluckLevels;
        data.acc = stats.newaccuracyLevels;

        data.activeSkills = uiManager.ReturnAllActiveSkills();
        data.chests = itemsManager.chestsDict;

        bf.Serialize(file, data);
        file.Close();

        /*
        Debug.Log(data.savePointId);
        Debug.Log(data.health);
        Debug.Log(data.level);
        Debug.Log(data.exp);
        Debug.Log(data.mp);
        Debug.Log(data.potions);
        Debug.Log(data.ethers);
        for (int i = 0; i < weapons.Count; i++)
        {
            Debug.Log(data.weaponsInInventory[i]);
        }
        for (int i = 0; i < armors.Count; i++)
        {
            Debug.Log(data.armorsInInventory[i]);
        }
        for (int i = 0; i < accs.Count; i++)
        {
            Debug.Log(data.accesoriesInInventory[i]);
        }
        for (int i = 0; i < qsStarted.Count; i++)
        {
            Debug.Log(data.questsStarted[i]);
        }
        for (int i = 0; i < qsCompleted.Count; i++)
        {
            Debug.Log(data.questsCompleted[i]);
        }
        */

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && Input.GetMouseButtonDown(1))
        {
            Save();
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.LEVEL_UP);
            //Animacion del letrero de puntos
            var clone1 = (GameObject)Instantiate(displayInfo, this.transform.position, Quaternion.Euler(Vector3.zero));
            clone1.GetComponent<DamageNumber>().damagePoints = "Game Saved!";
            clone1.GetComponent<DamageNumber>().damageText.color = Color.blue;

        }
    }




}
