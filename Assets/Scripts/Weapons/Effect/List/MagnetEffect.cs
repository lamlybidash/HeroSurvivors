using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetEffect : StatusEffect
{
	private Loot _lootComponent;
	public MagnetEffect(float durationx, float stepTimex) : base(durationx, stepTimex){}

	public override void ApplyEffect(EffectManager manager)
	{
		if (isActive == false)
		{
			_lootComponent = manager.GetComponentInChildren<Loot>();
			isActive = true;

		}
	}

	public override void RemoveEffect()
	{
		_lootComponent.IncreaseArea(-10);
	}

	public override void UpdateEffect()
	{
		if (isActive == true)
		{
			_lootComponent.IncreaseArea(10);
		}
		base.UpdateEffect();
	}
}
