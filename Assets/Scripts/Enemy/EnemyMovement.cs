using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private GameController _gc;
	[SerializeField] private Camera _cam;
	private Transform _player;
	private Rigidbody2D _rg;
	private Vector2 _direction;
	private float speed;
	private Vector3 _camBL;
	private Vector3 _camTR;

	private void Awake()
	{
		_rg = GetComponent<Rigidbody2D>();
		speed = 2f;
	}

	private void Start()
	{
		SetPlayer();
	}
	private void Update()
	{
		CamCalc();
		CheckOutCam();
	}

	private void FixedUpdate()
	{
		_direction = (_player.position - transform.position).normalized;
		_rg.MovePosition((Vector2)transform.position + _direction * speed * Time.fixedDeltaTime);
		FlipF();
	}

	private void SetPlayer()
	{
		_player = _gc.CharActive().transform;
	}

	private void CamCalc()
	{
		_camBL = _cam.ViewportToWorldPoint(new Vector3(0, 0, _cam.nearClipPlane));
		_camTR = _cam.ViewportToWorldPoint(new Vector3(1, 1, _cam.nearClipPlane));
	}

	private void CheckOutCam()
	{
		if((_camBL.x - 1 <= transform.position.x && transform.position.x <= _camTR.x + 1) && (_camBL.y - 1 <= transform.position.y && transform.position.y <= _camTR.y + 1))
		{
			//Debug.Log("Trong cam");
			return;
		}
		gameObject.SetActive(false);
	}	

	private void FlipF()
	{
		if(_direction.x <0)
		{
			transform.localScale = Vector3.one;
		}
		else
		{
			transform.localScale = new Vector3(-1,1,1);
		}	
	}	
}
