using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="GameData/Create Weapon Data", fileName ="Create WeaponData")]
public class WeaponsData : ScriptableObject
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


