using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName ="GameData/Create Weapon Data", fileName ="WeaponData")]
public class WeaponsData : ScriptableObject
{
	public string nameW;
	public float damage;
	public int projectile;
	public float countdown;
	public float area;
	public float speed;
	public float duration;
	public Sprite imgSprite;
	public GameObject projectilePf;
	public AudioClip ac;
	public List<WLevelUp> levelup;
}


