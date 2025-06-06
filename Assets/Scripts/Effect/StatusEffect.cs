using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect
{
	public float duration;
	public bool isActive;
	public float stepTime;
	//public Coroutine coroutine;

	public StatusEffect(float durationx, float stepTimex)
	{
		duration = durationx;
		stepTime = stepTimex;
	}

	public abstract void ApplyEffect(EffectManager manager);

	public virtual void UpdateEffect()
	{
		duration -= stepTime;
		if(duration <= 0)
		{
			isActive = false;
		}	
	}

	public abstract void RemoveEffect();

	//public void SetCoroutine(Coroutine coroutinex)
	//{
	//	coroutine = coroutinex;
	//}	
}
