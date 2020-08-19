using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerData
{
	public string savePointId;
	public string sceneName;
	public int health;
	public int mp;
	public int exp;
	public int level;
	public List<string> weaponsInInventory;
	public int activeWeapon;
	public List<string> armorsInInventory;
	public int activeArmor;
	public List<string> accesoriesInInventory;
	public int activeAccesory;
	public int potions;
	public int ethers;
	public List<int> questsStarted;
	public List<int> questsCompleted;

}
