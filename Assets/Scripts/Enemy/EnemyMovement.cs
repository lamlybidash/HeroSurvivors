using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private GameController _gc;
    private Transform _player;
	private Rigidbody2D _rg;
	private Vector2 _direction;
	private float speed;

	private void Awake()
	{
		_rg = GetComponent<Rigidbody2D>();
		speed = 2f;
	}

	private void Start()
	{
		SetPlayer();
	}

	private void FixedUpdate()
	{
		_direction = (_player.position - transform.position).normalized;
		_rg.MovePosition((Vector2)transform.position + _direction * speed * Time.fixedDeltaTime);
	}

	private void SetPlayer()
	{
		_player = _gc.CharActive().transform;
	}	



}
