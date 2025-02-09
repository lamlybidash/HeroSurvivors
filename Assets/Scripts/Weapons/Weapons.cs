using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class WLevelUp
{
	public int level;
	public int attribute;
	public float amount;
}

//abstract
public abstract class Weapons : MonoBehaviour
{
	public string nameW;
	protected float damage;
	protected int projectile;
	protected float countdown;
	protected float area;
	protected float speed;
	protected float duration;
	protected Sprite imgSprite;
	protected GameObject projectilePf;
	protected AudioClip ac;
	protected Transform player;
	public int _level { get; set; } = 0;
	protected List<WLevelUp> levelup
	{
		get
		{
			return data.levelup;
		}
		set
		{
			data.levelup = value;
		}
	}
	[SerializeField] public WeaponsData data;

	//public int bonusDame;
	//public int bonusDameMultiple;
	//public float Damage => data.damage * bonusDameMultiple + bonusDame;

	public virtual void LevelUp(int attributef, float amountf)
	{
		_level++;
		switch (attributef)
		{
			case 1:
				{
					damage += amountf;
					break;
				}
			case 2:
				{
					projectile += (int)amountf;
					break;
				}
			case 3:
				{
					countdown = countdown * (1 - amountf / 100);
					break;
				}
			case 4:
				{
					area += amountf;
					break;
				}
			case 5:
				{
					speed += amountf;
					break;
				}
			case 6:
				{
					duration += amountf;
					break;
				}
		}
	}

	public void InitData()
	{
		nameW = data.nameW;
		damage = data.damage;
		projectile = data.projectile;
		countdown = data.countdown;
		area = data.area;
		speed = data.speed;
		duration = data.duration;
		imgSprite = data.imgSprite;
		projectilePf = data.projectilePf;
		ac = data.ac;
	}

	public virtual void SetUpData(Weapons x)
	{
		nameW = x.nameW;
		damage = x.damage;
		projectile = x.projectile;
		countdown = x.countdown;
		area = x.area;
		speed = x.speed;
		duration = x.duration;
		imgSprite = x.imgSprite;
		projectilePf = x.projectilePf;
		ac = x.ac;
		player = x.player;
	}

	public virtual void ResetWeapon()
	{
		InitData();
		_level = 0;
		gameObject.SetActive(false);
	}	
	public void SetPlayer(Transform pl)
	{
		player = pl;
	}
	public virtual void SetUpStartGame() { }
}



