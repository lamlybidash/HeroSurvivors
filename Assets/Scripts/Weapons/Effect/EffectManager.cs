using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
	private List<StatusEffect> listEffectAcive = new List<StatusEffect>();
	public void ExcuteEffect(StatusEffect effectx)
	{
		listEffectAcive.Add(effectx);
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
			yield return new WaitForSeconds(effectx.stepTime);
		}
		listEffectAcive.Remove(effectx); // tam thoi chua co tac dung
	}	
}
