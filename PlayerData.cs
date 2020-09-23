using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerData
{
	public string savePointId;
	public string sceneName;
	public int gold;
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
	public int pd;
	public List<int> questsStarted;
	public List<int> questsCompleted;
	public List<string> questItems;
	public List<string> activeSkills;
	public Dictionary<string, string> chests;

	public int str;
	public int def;
	public int mat;
	public int mdf;
	public int spd;
	public int lck;
	public int acc;

}
