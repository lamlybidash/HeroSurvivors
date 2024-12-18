using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
	public void ExcuteEffect(StatusEffect effectx)
	{
		effectx.ApplyEffect(this);
		Coroutine coroutine;
		coroutine = StartCoroutine(UpdateEffect(effectx));
		//effectx.SetCoroutine(coroutine);
	}

	private IEnumerator UpdateEffect(StatusEffect effectx)
	{
		while (effectx.isActive)
		{
			effectx.UpdateEffect();
			yield return new WaitForSecondsRealtime(effectx.stepTime);
		}
	}	
}
