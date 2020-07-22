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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if(itemName == "Potion")
            {
                itemsManager.AddPotions(1);
                //this.transform.parent = GameObject.Find("Inventory Player").transform;
                gameObject.SetActive(false);
                itemHasBeenCollected = true;
                //Destroy(gameObject);
            }

            if (itemName == "HP point")
            {
                int hPvalue;
                hPvalue = Random.Range(5, 10);
                itemsManager.AddHP(hPvalue);
                gameObject.SetActive(false);
                var clone2 = (GameObject)Instantiate(displayInfo, this.transform.position, Quaternion.Euler(Vector3.zero));
                clone2.GetComponent<DamageNumber>().damagePoints = "+" + hPvalue;
                clone2.GetComponent<DamageNumber>().damageText.color = Color.green;
            }

            if(itemName == "weapon")
            {
                this.transform.parent = GameObject.Find("Weapon").transform;
                gameObject.SetActive(false);
                itemHasBeenCollected = true;
            }
        }
    }
}
