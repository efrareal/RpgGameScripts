using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPManager : MonoBehaviour
{
    public int maxMP;
    private int currentMP;


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
        
        if(currentMP < 0)
        {
            currentMP = 0;
        }
    }

    public void UpdateMaxMP(int newMaxMP)
    {
        maxMP = newMaxMP;
        currentMP = maxMP;
    }

    public void AddMPPoints(int value)
    {
        if ((currentMP + value) >= maxMP)
        {
            currentMP = maxMP;
            return;
        }
        currentMP += value;
    }


}
