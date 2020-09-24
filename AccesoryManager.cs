using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccesoryManager : MonoBehaviour
{
    private List<GameObject> accesories;
    public int activeAccesory;
    private CharacterStats thePlayerStats;

    public List<GameObject> GetAllAccesories()
    {
        accesories = new List<GameObject>();
        foreach (Transform accesory in transform)
        {
            if (accesory.GetComponent<Accesory>().inInventory)
            {
                accesories.Add(accesory.gameObject);
            }
        }
        return accesories;
    }

    // Start is called before the first frame update
    void Start()
    {
        thePlayerStats = FindObjectOfType<PlayerController>().GetComponent<CharacterStats>();
        GetAllAccesories();
        for (int i = 0; i < accesories.Count; i++)
        {
            accesories[i].SetActive(false);
        }
    }

    public void ChangeAccesory(int newAccesory)
    {

        thePlayerStats.RemoveStatsToCharacter(accesories[activeAccesory].GetComponent<WARStats>().strength,
                                          accesories[activeAccesory].GetComponent<WARStats>().defense,
                                          accesories[activeAccesory].GetComponent<WARStats>().magicAtt,
                                          accesories[activeAccesory].GetComponent<WARStats>().magicDef,
                                          accesories[activeAccesory].GetComponent<WARStats>().speed,
                                          accesories[activeAccesory].GetComponent<WARStats>().luck,
                                          accesories[activeAccesory].GetComponent<WARStats>().accuracy);
        accesories[activeAccesory].SetActive(false);
        accesories[newAccesory].SetActive(true);
        activeAccesory = newAccesory;

        thePlayerStats.AddStatsToCharacter(accesories[newAccesory].GetComponent<WARStats>().strength,
                                          accesories[newAccesory].GetComponent<WARStats>().defense,
                                          accesories[newAccesory].GetComponent<WARStats>().magicAtt,
                                          accesories[newAccesory].GetComponent<WARStats>().magicDef,
                                          accesories[newAccesory].GetComponent<WARStats>().speed,
                                          accesories[newAccesory].GetComponent<WARStats>().luck,
                                          accesories[newAccesory].GetComponent<WARStats>().accuracy);
    }

    public Accesory GetAccesoryAt(int pos)
    {
        return accesories[pos].GetComponent<Accesory>();
    }

    public WARStats GetAccesoryStatsAt(int pos)
    {
        return accesories[pos].GetComponent<WARStats>();
    }

    public List<GameObject> GetAllNotInInvetoryAccesory()
    {
        accesories = new List<GameObject>();
        foreach (Transform accesory in transform)
        {
            if (!accesory.GetComponent<Accesory>().inInventory)
            {
                accesories.Add(accesory.gameObject);
            }
        }
        return accesories;
    }

    public void ResetAllAccesories()
    {
            for (int i = 0; i < accesories.Count; i++)
            {
                accesories[i].SetActive(false);
                accesories[i].GetComponent<Accesory>().inInventory = false;
            }
    }

    public void ResetAccesoriesToInitial()
    {
        for (int i = 1; i < accesories.Count; i++)
        {
            accesories[i].SetActive(false);
            accesories[i].GetComponent<Accesory>().inInventory = false;
        }
        accesories[0].SetActive(true);
        activeAccesory = 0;
        accesories[0].GetComponent<Accesory>().inInventory = true;
    }

    public List<string> GetAllAccsName()
    {
        List<string> accsName = new List<string>();
        foreach (Transform acc in transform)
        {
            if (acc.GetComponent<Accesory>().inInventory)
            {
                accsName.Add(acc.GetComponent<Accesory>().accesoryName);
            }
        }
        return accsName;
    }

    public void SearchAccByName(string name)
    {
        foreach (Transform acc in transform)
        {
            if (acc.GetComponent<Accesory>().accesoryName == name)
            {
                acc.GetComponent<Accesory>().inInventory = true;
            }
        }
        GetAllAccesories();
    }

    public void ActivateAcc(bool condition)
    {
        accesories[activeAccesory].SetActive(condition);
    }
}
