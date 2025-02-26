using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileBaseBallBat : Weapons
{
	private Coroutine _coroutine;
	private float _angelTemp;
	private float _timeTemp;
	private float _timeDuration = 0.25f;
	public Transform playertest;
	float angel;
	Vector3 _dirPlayer;
	private void Start()
	{
		StartAttack();
		countdown = 2;
	}

	private IEnumerator MoveBonk() // chuyển động của gậy
	{

		float angelTemp;
		_timeTemp = 0;
		angelTemp = 0;
		_dirPlayer = playertest.GetComponent<PlayerMovement>().GetDirectionPlayer();
		if (_dirPlayer.x < 0)
		{
			angel = 180;
		}
		if (_dirPlayer.x >= 0)
		{
			angel = -180;
		}
		transform.rotation = Quaternion.Euler(0, 0, 0);
		while (_timeTemp < _timeDuration)
		{
			//Cần bám theo player
			transform.position = playertest.position;
			_timeTemp += Time.deltaTime;
			angelTemp += (angel) * (Time.deltaTime / _timeDuration);
			transform.rotation = Quaternion.Euler(0, 0, angelTemp);
			yield return null;
		}
		transform.rotation = Quaternion.Euler(0, 0, 0);
		gameObject.SetActive(false);
	}

	public void StartAttack()
	{
		_coroutine = StartCoroutine(MoveBonk());
	}

	public void StopAttack()
	{
		if (_coroutine != null)
		{
			StopCoroutine(_coroutine);
		}
	}
}
