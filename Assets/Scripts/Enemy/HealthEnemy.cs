using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
	private float _healthTotal = 100f;
	private float _healthCurrent;
	private double _expDrop;

	private void Start()
	{
		_healthCurrent = _healthTotal;
	}
	public void setupEnemy()
	{
		if(_healthCurrent <= 0)
		{
			_healthCurrent = _healthTotal;
		}
	}	
	public void TakeDame(float dame)
	{
		_healthCurrent = Mathf.Clamp(_healthCurrent - dame, 0, _healthTotal);
		PopupController.instance.PopupDame(transform, dame);
		if (_healthCurrent == 0 )
		{
			//Debug.Log("hehe");
			ExpController.instance.DropExp(transform,_expDrop);
			gameObject.SetActive(false);
		}
	}

	public void SetExpDrop(double expAmount)
	{
		_expDrop = expAmount;
	}	

}
