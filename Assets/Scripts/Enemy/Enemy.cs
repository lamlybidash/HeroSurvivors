using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	private HealthEnemy _health;
	private double _expDrop { get; set; }

	private void Awake()
	{
		_health = GetComponent<HealthEnemy>();
	}
	private void Start()
	{
		_expDrop = 3;
		_health.SetExpDrop(_expDrop);
	}
	public void ActiveEnemy(Vector3 pos)
	{
		transform.position = pos;
		gameObject.SetActive(true);
		_health.setupEnemy();
	}

}
