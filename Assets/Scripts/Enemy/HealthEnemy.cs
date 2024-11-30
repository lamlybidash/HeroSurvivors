using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
	private float _healthTotal = 100f;
	private float _healthCurrent;

	private void Start()
	{
		_healthCurrent = _healthTotal;
	}
	public void setupEnemy()
	{
		_healthCurrent = _healthTotal;
	}	
	public void TakeDame(float dame)
	{
		_healthCurrent = Mathf.Clamp(_healthCurrent - dame, 0, _healthTotal);
		PopupController.instance.PopupDame(transform, dame);
		if (_healthCurrent == 0 )
		{
			gameObject.SetActive(false);
		}	
	}	

}
