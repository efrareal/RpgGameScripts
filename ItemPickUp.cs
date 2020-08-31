using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemPickUp : MonoBehaviour
{
    public string itemName;
    public bool itemHasBeenCollected;

    public GameObject displayInfo;
    //public int potionHealthPoints = 100;
    //public int etherMagicPoints = 10;

    private ItemsManager itemsManager;
    private MoneyManager moneyManager;


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (itemHasBeenCollected)
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        itemsManager = FindObjectOfType<ItemsManager>();
        moneyManager = FindObjectOfType<MoneyManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if(itemName == "Potion")
            {
                SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.GATHER_DROPS);
                itemsManager.AddPotions(1);
                //this.transform.parent = GameObject.Find("Inventory Player").transform;
                gameObject.SetActive(false);
                itemHasBeenCollected = true;
                //Destroy(gameObject);
            }

            if (itemName == "HP point")
            {
                int hPvalue;
                hPvalue = collision.GetComponent<CharacterStats>().level * Random.Range(5, 10);
                itemsManager.AddHP(hPvalue);
                gameObject.SetActive(false);
                var clone2 = (GameObject)Instantiate(displayInfo, this.transform.position, Quaternion.Euler(Vector3.zero));
                clone2.GetComponent<DamageNumber>().damagePoints = "+" + hPvalue;
                clone2.GetComponent<DamageNumber>().damageText.color = Color.green;
                SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.GATHER_DROPS);
            }

            if(itemName == "Money")
            {
                SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.GATHER_DROPS);
                int moneyValue;
                moneyValue = collision.GetComponent<CharacterStats>().level * Random.Range(5, 10);
                moneyManager.AddMoney(moneyValue);
                gameObject.SetActive(false);
                var clone3 = (GameObject)Instantiate(displayInfo, this.transform.position, Quaternion.Euler(Vector3.zero));
                clone3.GetComponent<DamageNumber>().damagePoints = "+" + moneyValue;
                clone3.GetComponent<DamageNumber>().damageText.color = Color.yellow;
                SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.GATHER_DROPS);

            }
        }
    }
}
