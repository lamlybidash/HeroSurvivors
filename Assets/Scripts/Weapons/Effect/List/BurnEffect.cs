using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnEffect : StatusEffect
{
	private float _damage;
	private HealthEnemy healthEnemy;
	public BurnEffect(float damage, float durationx, float stepTimex) : base(durationx, stepTimex)
	{
		_damage = damage;
	}	

	public override void ApplyEffect(EffectManager manager)
	{
		if(isActive == false) 
		{
			healthEnemy = manager.GetComponent<HealthEnemy>();
			isActive = true;
		}
	}

	public override void RemoveEffect()
	{
		if(isActive)
		{
			isActive = false;
		}
	}

	public override void UpdateEffect()
	{
		duration -= stepTime;
		if (isActive == true)
		{
			if (healthEnemy.isDieF() == false)
			{
				healthEnemy.TakeDame((_damage * stepTime));
			}
			else
			{
				duration = 0;
				RemoveEffect();
			}
		}
	}
}