using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorManager : MonoBehaviour
{
    private List<GameObject> armors;
    public int activeArmor;
    private CharacterStats thePlayerStats;

    public List<GameObject> GetAllArmors()
    {
        armors = new List<GameObject>();
        foreach (Transform armor in transform)
        {
            if (armor.GetComponent<Armor>().inInventory)
            {
                armors.Add(armor.gameObject);
            }
        }
        return armors;
    }

    // Start is called before the first frame update
    void Start()
    {
        thePlayerStats = FindObjectOfType<PlayerController>().GetComponent<CharacterStats>();
        GetAllArmors();
        for (int i = 0; i < armors.Count; i++)
        {
            armors[i].SetActive(false);
        }
    }

    public void ChangeArmor(int newArmor)
    {
        
        thePlayerStats.RemoveStatsToCharacter(armors[activeArmor].GetComponent<WARStats>().strength,
                                          armors[activeArmor].GetComponent<WARStats>().defense,
                                          armors[activeArmor].GetComponent<WARStats>().magicAtt,
                                          armors[activeArmor].GetComponent<WARStats>().magicDef,
                                          armors[activeArmor].GetComponent<WARStats>().speed,
                                          armors[activeArmor].GetComponent<WARStats>().luck,
                                          armors[activeArmor].GetComponent<WARStats>().accuracy);
        armors[activeArmor].SetActive(false);
        armors[newArmor].SetActive(true);
        activeArmor = newArmor;

        thePlayerStats.AddStatsToCharacter(armors[newArmor].GetComponent<WARStats>().strength,
                                          armors[newArmor].GetComponent<WARStats>().defense,
                                          armors[newArmor].GetComponent<WARStats>().magicAtt,
                                          armors[newArmor].GetComponent<WARStats>().magicDef,
                                          armors[newArmor].GetComponent<WARStats>().speed,
                                          armors[newArmor].GetComponent<WARStats>().luck,
                                          armors[newArmor].GetComponent<WARStats>().accuracy);
    }

    public Armor GetArmorAt(int pos)
    {
        return armors[pos].GetComponent<Armor>();
    }

    public WARStats GetArmorStatsAt(int pos)
    {
        return armors[pos].GetComponent<WARStats>();
    }

    public List<GameObject> GetAllNotInInvetoryArmors()
    {
        armors = new List<GameObject>();
        foreach (Transform armor in transform)
        {
            if (!armor.GetComponent<Armor>().inInventory)
            {
                armors.Add(armor.gameObject);
            }
        }
        return armors;
    }

    public void ResetAllArmors()
    {
            for (int i = 1; i < armors.Count; i++)
            {
                armors[i].SetActive(false);
                armors[i].GetComponent<Armor>().inInventory = false;
            }
        armors[0].SetActive(true);
        activeArmor = 0;
        armors[0].GetComponent<Armor>().inInventory = true;
    }

    public List<string> GetAllArmorsName()
    {
        List<string> armorsName = new List<string>();
        foreach (Transform armor in transform)
        {
            if (armor.GetComponent<Armor>().inInventory)
            {
                armorsName.Add(armor.GetComponent<Armor>().armorName);
            }
        }
        return armorsName;
    }

    public void SearchArmorByName(string name)
    {
        foreach (Transform armor in transform)
        {
            if (armor.GetComponent<Armor>().armorName == name)
            {
                armor.GetComponent<Armor>().inInventory = true;
            }
        }
        GetAllArmors();
    }

}
