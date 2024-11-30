using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	private HealthEnemy _health;
	private void Awake()
	{
		_health = GetComponent<HealthEnemy>();
	}
	public void ActiveEnemy(Vector3 pos)
	{
		transform.position = pos;
		gameObject.SetActive(true);
		_health.setupEnemy();
	}

}
