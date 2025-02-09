using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/Create Character Data", fileName = "CharacterData")]
public class CharacterData : ScriptableObject
{
	public string nameChar;
	public float HP;
	public float def;
	public float dame; //* 1.x
	public float speedMove;
	public float speedAttack;
	public string startingWeapon;


}

