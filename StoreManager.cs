using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public GameObject storePanel;

    public Button potionButton;
    public Text potionValueText;
    public int potionCost;

    public Button etherButton;
    public Text etherValueText;
    public int etherCost;

    public Button pdButton;
    public Text pdValueText;
    public int pdCost;

    private ItemsManager itemsManager;
    private MoneyManager moneyManager;

    public Button exitButton;
    public Text inStockText;

    public bool inStore;
    private PlayerController thePlayer;


    // Start is called before the first frame update
    void Start()
    {
        itemsManager = FindObjectOfType<ItemsManager>();
        moneyManager = FindObjectOfType<MoneyManager>();
        thePlayer = FindObjectOfType<PlayerController>();

        potionValueText.text = potionCost.ToString();
        etherValueText.text = etherCost.ToString();
        pdValueText.text = pdCost.ToString();
    }

    public void BuyPotion()
    {
        if (moneyManager.currentMoney >= potionCost)
        {
            itemsManager.AddPotions(1);
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.RECEIVE_ITEM);
            moneyManager.SustractMoney(potionCost);
        }
        else
        {
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.UI_MENU_ERROR);
        }

    }

    public void BuyEther()
    {
        if (moneyManager.currentMoney >= etherCost)
        {
            itemsManager.AddEthers(1);
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.RECEIVE_ITEM);
            moneyManager.SustractMoney(etherCost);
        }
        else
        {
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.UI_MENU_ERROR);
        }

    }
    public void BuyPD()
    {
        if (moneyManager.currentMoney >= pdCost)
        {
            itemsManager.AddPD(1);
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.RECEIVE_ITEM);
            moneyManager.SustractMoney(pdCost);
        }
        else
        {
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.UI_MENU_ERROR);
        }

    }

    public void ShowPotionsInStock()
    {
        inStockText.text = "In Stock; " + itemsManager.currentPotions.ToString();
    }

    public void ShowEthersInStock()
    {
        inStockText.text = "In Stock; " + itemsManager.currentEthers.ToString();
    }
    public void ShowPDsInStock()
    {
        inStockText.text = "In Stock; " + itemsManager.currentPhoenixDown.ToString();
    }

    public void ClearInStock()
    {
        inStockText.text = "In Stock: ";
    }

    public void CloseStoreGUI()
    {
        storePanel.SetActive(false);
        thePlayer = FindObjectOfType<PlayerController>();
        thePlayer.canMove = true;
    }

}
