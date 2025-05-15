using System;
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
    [SerializeField] public WeaponsData data;
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
    public string nameW;
    protected float damage;
    protected int projectile;
	protected float countdown;
	protected float area;
	protected float speed;
	protected float duration;
    public int _level { get; set; } = 0;
    protected Sprite imgSprite;
	protected GameObject projectilePf;
	protected AudioClip ac;
	protected Transform player;
	private float _CDCount;
	// -------------------------- Bonus Stat --------------------
    protected float bonusDamage = 0;
    protected int bonusProjectile = 0;
    protected float bonusCountdown = 0;
    protected float bonusArea = 0;
    protected float bonusSpeed = 0;
    protected float bonusDuration = 0;
    // ------------------------- Multiple Stat -------------------
    protected float multiDamage = 1;
    protected float multiProjectile = 1;
    protected float multiCountdown = 1;
    protected float multiArea = 1;
    protected float multiSpeed = 1;
    protected float multiDuration = 1;

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
					countdown = data.countdown;
					_CDCount += amountf;
					countdown = countdown * (1 - _CDCount / 100);
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

	private void InitData()
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
		_CDCount = 0;
		gameObject.SetActive(false);
	}
	public void SetBonus(float? bonusDamagex = null, int? bonusProjectilex = null, float? bonusCountdownx = null, float? bonusAreax = null, float? bonusSpeedx = null, float? bonusDurationx = null)
	{
		if (bonusDamagex.HasValue)
		{
			bonusDamage = bonusDamagex.Value;
        }
        if (bonusProjectilex.HasValue)
        {
			bonusProjectile = bonusProjectilex.Value;
        }
        if (bonusCountdownx.HasValue)
        {
            bonusCountdown = bonusCountdownx.Value;
        }
        if (bonusAreax.HasValue)
        {
            bonusArea = bonusAreax.Value;
        }
        if (bonusSpeedx.HasValue)
        {
			bonusSpeed = bonusSpeedx.Value;
        }
        if (bonusDurationx.HasValue)
        {
            bonusDuration = bonusDurationx.Value;
        }
        UpdateStat();
    }
    public void SetMulti(float? multiDamagex = null, float? multiProjectilex = null, float? multiCountdownx = null, float? multiAreax = null, float? multiSpeedx = null, float? multiDurationx = null)
    {
        if (multiDamagex.HasValue)
        {
            multiDamage = multiDamagex.Value;
        }
        if (multiProjectilex.HasValue)
        {
			multiProjectile = multiProjectilex.Value;
        }
        if (multiCountdownx.HasValue)
        {
			multiCountdown = multiCountdownx.Value;
        }
        if (multiAreax.HasValue)
        {
			multiArea = multiAreax.Value;
        }
        if (multiSpeedx.HasValue)
        {
			multiSpeed = multiSpeedx.Value;
        }
        if (multiDurationx.HasValue)
        {
			multiDuration = multiDurationx.Value;
        }
        UpdateStat();
    }
    public void AddBonus(float? bonusDamagex = null, int? bonusProjectilex = null, float? bonusCountdownx = null, float? bonusAreax = null, float? bonusSpeedx = null, float? bonusDurationx = null)
    {
        if (bonusDamagex.HasValue)
        {
            bonusDamage += bonusDamagex.Value;
        }
        if (bonusProjectilex.HasValue)
        {
            bonusProjectile += bonusProjectilex.Value;
        }
        if (bonusCountdownx.HasValue)
        {
            bonusCountdown += bonusCountdownx.Value;
        }
        if (bonusAreax.HasValue)
        {
            bonusArea += bonusAreax.Value;
        }
        if (bonusSpeedx.HasValue)
        {
            bonusSpeed += bonusSpeedx.Value;
        }
        if (bonusDurationx.HasValue)
        {
            bonusDuration += bonusDurationx.Value;
        }
        UpdateStat();
    }
    public void AddMulti(float? multiDamagex = null, float? multiProjectilex = null, float? multiCountdownx = null, float? multiAreax = null, float? multiSpeedx = null, float? multiDurationx = null)
    {
        if (multiDamagex.HasValue)
        {
            multiDamage += multiDamagex.Value;
        }
        if (multiProjectilex.HasValue)
        {
            multiProjectile += multiProjectilex.Value;
        }
        if (multiCountdownx.HasValue)
        {
            multiCountdown += multiCountdownx.Value;
        }
        if (multiAreax.HasValue)
        {
            multiArea += multiAreax.Value;
        }
        if (multiSpeedx.HasValue)
        {
            multiSpeed += multiSpeedx.Value;
        }
        if (multiDurationx.HasValue)
        {
            multiDuration += multiDurationx.Value;
        }
        UpdateStat();
    }
    public void ResetBonusAndMulti()
    {
        bonusDamage = 0;
        bonusProjectile = 0;
        bonusCountdown = 0;
        bonusArea = 0;
        bonusSpeed = 0;
        bonusDuration = 0;
        multiDamage = 1;
        multiProjectile = 1;
        multiCountdown = 1;
        multiArea = 1;
        multiSpeed = 1;
        multiDuration = 1;
        UpdateStat();
    }    
    protected virtual void UpdateStat()
    {
        damage = data.damage * multiDamage + bonusDamage;
        projectile = (int)(data.projectile * multiProjectile + bonusProjectile);
        countdown = data.countdown * multiCountdown + bonusCountdown;
        area = data.area * multiArea + bonusArea;
        speed = data.speed * multiSpeed + bonusSpeed;
        duration = data.duration * multiDuration + bonusDuration;
    }
    public void SetPlayer(Transform pl)
	{
		player = pl;
	}
	public virtual void SetUpStartGame(Character charx) {
        InitData();
        SetupBM(charx);
        UpdateStat();
    }
    private void SetupBM(Character charx) // Setup Bonus and Multi
    {
        switch (charx.data.SA)
        {
            case "damage":
                {
                    if (charx.data.SAType == "Bonus") AddBonus(bonusDamagex: charx.data.SAAmount);
                    if (charx.data.SAType == "Multi") AddMulti(multiDamagex: charx.data.SAAmount);
                    break;
                }

            case "projectile":
                {
                    if (charx.data.SAType == "Bonus") AddBonus(bonusProjectilex: (int)charx.data.SAAmount);
                    if (charx.data.SAType == "Multi") AddMulti(multiProjectilex: charx.data.SAAmount);
                    break;
                }

            case "countdown":
                {
                    if (charx.data.SAType == "Bonus") AddBonus(bonusCountdownx: charx.data.SAAmount);
                    if (charx.data.SAType == "Multi") AddMulti(multiCountdownx: charx.data.SAAmount);
                    break;
                }

            case "area":
                {
                    if (charx.data.SAType == "Bonus") AddBonus(bonusAreax: charx.data.SAAmount);
                    if (charx.data.SAType == "Multi") AddMulti(multiAreax: charx.data.SAAmount);
                    break;
                }

            case "speed":
                {
                    if (charx.data.SAType == "Bonus") AddBonus(bonusSpeedx: charx.data.SAAmount);
                    if (charx.data.SAType == "Multi") AddMulti(multiSpeedx: charx.data.SAAmount);
                    break;
                }

            case "duration":
                {
                    if (charx.data.SAType == "Bonus") AddBonus(bonusDurationx: charx.data.SAAmount);
                    if (charx.data.SAType == "Multi") AddMulti(multiDurationx: charx.data.SAAmount);
                    break;
                }
        }
    }    
}
