using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
	public List<StatusEffect> listEffectAcive = new List<StatusEffect>();
	public void ExcuteEffect(StatusEffect effectx)
	{
		listEffectAcive.Add(effectx);
		effectx.ApplyEffect(this);
		Coroutine coroutine;
		if (gameObject.activeInHierarchy == true)
		{
			coroutine = StartCoroutine(UpdateEffect(effectx));
		}
		//effectx.SetCoroutine(coroutine);
	}

	private IEnumerator UpdateEffect(StatusEffect effectx)
	{
		while (effectx.isActive)
		{
			effectx.UpdateEffect();
			yield return new WaitForSeconds(effectx.stepTime);
		}
		effectx.RemoveEffect();
		try
		{
            listEffectAcive.Remove(effectx); // tam thoi chua co tac dung
        }
		catch
		{
			Debug.Log("Gỡ Effect thất bại. Effect đã bị gỡ trước đó rồi");
		}

    }
	public void ClearAllEffect()
	{ 
		StopAllCoroutines();
		foreach (var effect in listEffectAcive)
		{
			effect.RemoveEffect();
		}
		listEffectAcive.Clear();
	}
}
