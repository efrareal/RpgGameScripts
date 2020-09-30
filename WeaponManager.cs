using System.Collections;
using System.Collections.Generic;
//using System.Security.Policy;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private List<GameObject> weapons;
    public int activeWeapon;
    private CharacterStats thePlayerStats;

    public List<GameObject> GetAllWeapons()
    {
        weapons = new List<GameObject>();
        foreach (Transform weapon in transform)
        {
            if (weapon.GetComponent<WeaponDamage>().inInventory)
            {
                weapons.Add(weapon.gameObject);
            }
        }

        /*for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].SetActive(false);
        }*/
        return weapons;
    }
    // Start is called before the first frame update
    void Start()
    {
        thePlayerStats = FindObjectOfType<PlayerController>().GetComponent<CharacterStats>();
        weapons = new List<GameObject>();
        foreach(Transform weapon in transform)
        {
            if (weapon.GetComponent<WeaponDamage>().inInventory)
            {
                weapons.Add(weapon.gameObject);
            }
        }
        
        for (int i=0; i < weapons.Count; i++)
        {
            weapons[i].SetActive(false);
        }

        //weapons[0].SetActive(true);
    }

    public void ChangeWeapon(int newWeapon)
    {
        thePlayerStats.RemoveStatsToCharacter(weapons[activeWeapon].GetComponent<WARStats>().strength,
                                           weapons[activeWeapon].GetComponent<WARStats>().defense,
                                           weapons[activeWeapon].GetComponent<WARStats>().magicAtt,
                                           weapons[activeWeapon].GetComponent<WARStats>().magicDef,
                                           weapons[activeWeapon].GetComponent<WARStats>().speed,
                                           weapons[activeWeapon].GetComponent<WARStats>().luck,
                                           weapons[activeWeapon].GetComponent<WARStats>().accuracy);
        weapons[activeWeapon].SetActive(false);

        weapons[newWeapon].SetActive(true);
        activeWeapon = newWeapon;

        thePlayerStats.AddStatsToCharacter(weapons[newWeapon].GetComponent<WARStats>().strength,
                                           weapons[newWeapon].GetComponent<WARStats>().defense,
                                           weapons[newWeapon].GetComponent<WARStats>().magicAtt,
                                           weapons[newWeapon].GetComponent<WARStats>().magicDef,
                                           weapons[newWeapon].GetComponent<WARStats>().speed,
                                           weapons[newWeapon].GetComponent<WARStats>().luck,
                                           weapons[newWeapon].GetComponent<WARStats>().accuracy);
        

        FindObjectOfType<UIManager>().ChangeAvatarImage(weapons[activeWeapon].GetComponent<SpriteRenderer>().sprite);
    }
    public void DeactivateWeapon(bool condition)
    {
        weapons[activeWeapon].SetActive(condition);
        FindObjectOfType<UIManager>().ChangeAvatarImage(weapons[activeWeapon].GetComponent<SpriteRenderer>().sprite);
    }

    public List<GameObject> GetAllNotInInvetoryWeapons()
    {
        weapons = new List<GameObject>();
        foreach (Transform weapon in transform)
        {
            if (!weapon.GetComponent<WeaponDamage>().inInventory)
            {
                weapons.Add(weapon.gameObject);
            }
        }
        return weapons;
    }

    public WeaponDamage GetWeaponAt(int pos)
    {
        return weapons[pos].GetComponent<WeaponDamage>();
    }

    public WARStats GetWeaponStatsAt(int pos)
    {
        return weapons[pos].GetComponent<WARStats>();
    }

    public void ResetAllWeapons()
    {
            for (int i = 0; i < weapons.Count; i++)
            {
                weapons[i].SetActive(false);
                weapons[i].GetComponent<WeaponDamage>().inInventory = false;
            }
    }

    public void ResetToInitialWeapon()
    {
        for (int i = 1; i < weapons.Count; i++)
        {
            weapons[i].SetActive(false);
            weapons[i].GetComponent<WeaponDamage>().inInventory = false;
        }
        weapons[0].SetActive(true);
        activeWeapon = 0;
        weapons[0].GetComponent<WeaponDamage>().inInventory = true;
    }

    public List<string> GetAllWeaponsName()
    {
        List<string> weaponsName = new List<string>();
        foreach (Transform weapon in transform)
        {
            if (weapon.GetComponent<WeaponDamage>().inInventory)
            {
                weaponsName.Add(weapon.GetComponent<WeaponDamage>().weaponName);
            }
        }
        return weaponsName;
    }

    public void SearchWeaponByName(string name)
    {
        foreach (Transform weapon in transform)
        {
            if (weapon.GetComponent<WeaponDamage>().weaponName == name)
            {
                weapon.GetComponent<WeaponDamage>().inInventory = true;
            }
        }
        GetAllWeapons();
    }
}



