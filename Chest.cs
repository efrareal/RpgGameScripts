using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chest : MonoBehaviour
{
    public string rewardWeaponName;
    private ItemsManager itemsManager;
    private WeaponManager weaponManager;
    private List<GameObject> notInInventoryWeapons;
    public string chestID;

    private Animator _animator;



    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        weaponManager = FindObjectOfType<WeaponManager>();

        if(PlayerPrefs.GetString(chestID)== "" || PlayerPrefs.GetString(chestID) == "closed")
        {
            PlayerPrefs.SetString(chestID, "closed");
        }
        else { _animator.SetBool("isOpened", true);
            PlayerPrefs.SetString(chestID, "opened");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Player") && Input.GetMouseButtonDown(1))
        {
            if (PlayerPrefs.GetString(chestID) == "closed")
            {
                
                _animator.SetBool("isOpened", true);
                notInInventoryWeapons = weaponManager.GetAllNotInInvetoryWeapons();
                foreach (GameObject nweapon in notInInventoryWeapons)
                {
                    if (nweapon.GetComponent<WeaponDamage>().weaponName == rewardWeaponName)
                    {
                        nweapon.GetComponent<WeaponDamage>().inInventory = true;
                    }
                }
                PlayerPrefs.SetString(chestID, "opened");
            }

        }
    }
}
