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
		Debug.Log("Set up Enemy");
	}	
	public void TakeDame(float dame)
	{
		_healthCurrent = Mathf.Clamp(_healthCurrent - dame, 0, _healthTotal);
		if (_healthCurrent == 0 )
		{
			Debug.Log("Enemy die");
			gameObject.SetActive(false);
		}	
	}	



}
