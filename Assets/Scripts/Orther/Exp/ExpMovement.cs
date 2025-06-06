using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpMovement : MonoBehaviour
{
	private float _speed;
	private Transform _player;
	private Vector2 _direction;
	private Rigidbody2D _rg;
	private Exp _exp;

	private void Awake()
	{
		_rg = GetComponent<Rigidbody2D>();
		_exp = GetComponent<Exp>();
	}

	private void Start()
	{
		_speed = 10f;
	}
	
	
	public void SetPlayer(Transform player)
	{
		_player = player;
	}

	private void FixedUpdate()
	{
		_direction = (_player.position - transform.position).normalized;
		_rg.MovePosition((Vector2)transform.position + _direction * _speed * Time.fixedDeltaTime);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			collision.GetComponent<PlayerExp>().GainExp(_exp.GetExp());
			enabled = false;
			gameObject.SetActive(false); 
		}
	}

}
