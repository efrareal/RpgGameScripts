using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPManager : MonoBehaviour
{
    public int maxMP;
    private int currentMP;

    public float timeCounter;
    public float mpChargerTime = 12;
    public bool mpCharge;

    private void Start()
    {
        UpdateMaxMP(maxMP);
    }

    public int MagicPoints
    {
        get
        {
            return currentMP;
        }
    }

    public void UseMP(int value)
    {

        currentMP -= value;
        
        if(currentMP <= 0)
        {
            currentMP = 0;
            mpCharge = true;
        }
    }

    public void UpdateMaxMP(int newMaxMP)
    {
        maxMP = newMaxMP;
        currentMP = maxMP;
    }

    public void AddMPPoints(int value)
    {
        mpCharge = false;
        timeCounter = 0;
        if ((currentMP + value) >= maxMP)
        {
            currentMP = maxMP;
            return;
        }
        currentMP += value;
    }

    public void ChangeMP(int value)
    {
        currentMP = value;
    }

    public void ChargeMP()
    {
        mpCharge = false;
        currentMP = maxMP;
        timeCounter = 0;


    }

    void Update()
    {
        if (mpCharge || currentMP <= 0)
        {
            timeCounter += Time.deltaTime;
            if(timeCounter > mpChargerTime)
            {
                ChargeMP();
            }
        }
    }

}
