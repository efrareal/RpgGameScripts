using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public int currentMoney = 0;
    public Text moneyText;
    // Start is called before the first frame update
    void Start()
    {
        AddMoney(currentMoney);
    }

    public void AddMoney(int moneyCollected)
    {
        currentMoney += moneyCollected;
        moneyText.text = currentMoney.ToString();
        //PlayerPrefs.SetInt("Money", currentMoney);
    }

    public void SustractMoney(int susmoney)
    {
        currentMoney -= susmoney;
        moneyText.text = currentMoney.ToString();
    }

    public void ResetMoney()
    {
        currentMoney = 0;
        moneyText.text = currentMoney.ToString();
    }
}
