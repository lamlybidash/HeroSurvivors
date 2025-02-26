using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCDs : MonoBehaviour
{
	[SerializeField] private Image _imageCDs;

	private Coroutine _coroutine;
	private float _timeCDs;
	private float _timeCount = 0;
	private bool _canUseSkill;
	void Start()
	{
		ResetSkillI();
	}

	public void UseSkill()
	{
		_canUseSkill = false;
		_imageCDs.fillAmount = 1;
		_coroutine = StartCoroutine(CDsSkillF());
	}

	private IEnumerator CDsSkillF()
	{
		while (_timeCount < _timeCDs && _canUseSkill == false)
		{
			_timeCount += Time.deltaTime;
			_imageCDs.fillAmount = Mathf.Clamp(_imageCDs.fillAmount - (Time.deltaTime / _timeCDs), 0, 1);
			yield return null;
		}
		_imageCDs.fillAmount = 0;
		_canUseSkill = true;
		_timeCount = 0;
	}
	public void ResetSkillI()
	{
		_canUseSkill = true;
		_imageCDs.fillAmount = 0;
		_timeCount = 0;
		if(_coroutine != null)
		{
			StopCoroutine(_coroutine);
		}
	}

	public void SetupDataSkill(float timeCDsx)
	{
		_timeCDs = timeCDsx;
	}

	public bool CanUseSkill()
	{
		return _canUseSkill;
	}	
}
