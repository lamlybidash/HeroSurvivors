using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/Create Character Data", fileName = "CharacterData")]
public class CharacterData : ScriptableObject
{
	public string nameChar;
	public float HP;
	public float def;
	public float speedMove;
	public float speedAttack;
	public string startingWeapon;
	public string SA; //special Ability
	public string SAType; //special Ability Type (Bonus / Multi)
    public float SAAmount; //special Ability Amount
    public string skillE;
	public string skillQ;
	public int cost;
}

