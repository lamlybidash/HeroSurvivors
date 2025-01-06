using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	private Vector2 _direction;
	private float _speed;
	private float _dame;
	private float _timeDuration = 4f;
	private float _timeTemp;
	private void Start()
	{
		_speed = 4;
	}

	private void Update()
	{
		transform.Translate(_direction * _speed * Time.deltaTime);
		_timeTemp += Time.deltaTime;
		if(_timeTemp >= _timeDuration)
		{
			gameObject.SetActive(false);
			_timeTemp = 0;
		}
	}

	// bắn theo 1 hướng (!= đạn đuổi)
	public void ShootBullet(Transform startPoint, Transform target, float dame)
	{
		_timeTemp = 0;
		transform.position = startPoint.position;
		_dame = dame;
		_direction = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y).normalized;
		gameObject.SetActive(true);
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			collision.gameObject.GetComponent<Health>().TakeDame(_dame);
			gameObject.SetActive(false);
		}
		if(collision.gameObject.tag == "WeaponDame")
		{
			gameObject.SetActive(false);
		}
	}
}