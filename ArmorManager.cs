using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorManager : MonoBehaviour
{
    private List<GameObject> armors;
    public int activeArmor;

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

        GetAllArmors();
        for (int i = 0; i < armors.Count; i++)
        {
            armors[i].SetActive(false);
        }
    }

    public void ChangeArmor(int newArmor)
    {
        armors[activeArmor].SetActive(false);
        armors[newArmor].SetActive(true);
        activeArmor = newArmor;
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
}
