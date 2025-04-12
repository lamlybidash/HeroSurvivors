using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileBaseBallBat : Weapons
{
	private Coroutine _coroutine;
	private float _angelTemp;
	private float _timeTemp;
	float angel;
	Vector2 _dirPlayer;
	private void Start()
	{
		//StartAttack();
	}

	private IEnumerator MoveBonk() // chuyển động của gậy
	{

		float angelTemp;
		_timeTemp = 0;
		angelTemp = 0;
		_dirPlayer = player.GetComponent<PlayerMovement>().GetDirectionPlayer();
		if (_dirPlayer.x < 0)
		{
			angel = 180;
		}
		if (_dirPlayer.x >= 0)
		{
			angel = -180;
		}
		transform.rotation = Quaternion.Euler(0, 0, 0);
		while (_timeTemp < duration)
		{
			//Cần bám theo player
			transform.position = player.position;
			_timeTemp += Time.deltaTime;
			angelTemp += (angel) * (Time.deltaTime / duration);
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

	public override void SetUpData(Weapons x)
	{
		base.SetUpData(x);
		transform.localScale = new Vector3(area, area, 1);
	}

	private void GiveDame(float dame, GameObject target)
	{
		target.GetComponent<HealthEnemy>().TakeDame(dame);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Enemy")
		{
			GiveDame(damage, collision.gameObject);
		}
	}
}
