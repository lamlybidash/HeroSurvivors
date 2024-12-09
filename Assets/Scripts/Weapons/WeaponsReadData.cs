using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsReadData : MonoBehaviour
{
	[System.Serializable]
	public class WLevelUp
	{
		public string level;
		public string attribute;
		public string amount;
	}

	[System.Serializable]
	public class WDataUpdate
	{
		public List<WeaponsData> ListWeapon;
	}

	[System.Serializable]
	public class WeaponsData : MonoBehaviour
	{
		public string nameW;
		public float damage;
		public int projectile;
		public float countdown;
		public float area;
		public float speed;
		public float duration;
		public List<WLevelUp> levelup;
	}

	[SerializeField] private TextAsset weaponAsset;
	private testWeaponList listWeapon = new testWeaponList();
	private void Awake()
	{
		ReadData();
	}

	private void ReadData()
	{
		listWeapon = JsonUtility.FromJson<testWeaponList>(weaponAsset.text);
	}

	public testWeaponList getListWeapon()
	{
		return listWeapon;
	}	
}
