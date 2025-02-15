using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Character : MonoBehaviour
{
	private string _nameChar;
	private float _HP;
	private float _def;
	private float _dame; //* 1.x
	private float _speedMove;
	private float _speedAttack;
	private string _startingWeapon;

	[SerializeField] private PlayerExp _playerExp;
	[SerializeField] private Health _playerHealth;
	[SerializeField] private Loot _playerLoot;
	[SerializeField] private PlayerMovement _playerMovement;
	[SerializeField] private WeaponsController _wec;
	[SerializeField] public CharacterData data;


	private void Awake()
	{
		InitData();
		SetUpData();
	}

	protected void InitData()
	{
		_nameChar = data.nameChar;
		_HP = data.HP;
		_def = data.def;
		_dame = data.dame;
		_speedMove = data.speedMove;
		_speedAttack = data.speedAttack;
		_startingWeapon = data.startingWeapon;
	}

	private void SetUpData()
	{
		_playerHealth.SetUpTotalHealth(_HP);
		_playerMovement.SetUpSpeed(_speedMove);
	}

	public string GetStartingWeapon()
	{
		return _startingWeapon;
	}
	public void ResetData()
	{
		GetComponent<PlayerExp>().ResetLevelChar();
		GetComponentInChildren<Loot>().ResetArea();
	}	
}
