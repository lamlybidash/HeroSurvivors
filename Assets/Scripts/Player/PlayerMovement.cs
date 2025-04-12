using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float _speed;
	//private int i = 0;
	private CameraController _camera;
	private Rigidbody2D _rg;
	private Animator _animator;
	private Vector3 _target;
	private Vector3 _targetRG;
	private Vector2 _direction;
	private float _xRunVt;
	private float _yRunVt;
	private float _xIdleVt;
	private float _yIdleVt;




	private void Awake()
	{
		//Debug.Log("Awake : " + name);
		_animator = GetComponent<Animator>();
		_camera = Camera.main.GetComponent<CameraController>();
		_rg = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		//Debug.Log("Start : " + name);
		_camera.SetFollower(transform);
		_target = transform.position;
	}

	public void resetCam()
	{
		_camera.SetFollower(transform);
		_target = transform.position;
	}	

	private void Update()
	{
		if (Input.GetMouseButton(1) && Time.timeScale != 0)
		{
			_target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			_target.z = transform.position.z;
			_direction = new Vector2(_target.x - transform.position.x, _target.y - transform.position.y).normalized;
			_animator.SetBool("isRun", true);
			_animator.SetFloat("directX", _direction.x);
			_animator.SetFloat("directY", _direction.y);
		}
	}
	private void FixedUpdate()
	{
		_targetRG = Vector3.MoveTowards(transform.position, _target, _speed * Time.fixedDeltaTime);
		_rg.MovePosition(_targetRG);

		if (Mathf.Abs(transform.position.x - _target.x) < 0.01 && Mathf.Abs(transform.position.y - _target.y) < 0.01) // Người chơi chạy tới vị trí chuột click
		{
			_animator.SetBool("isRun", false);
		}
	}

	private void checkInput()
	{
		if (Input.GetMouseButton(1))
		{
			_target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			_target.z = transform.position.z;
		}
	}	

	public void SetUpSpeed(float x)
	{
		_speed = x;
	}

	public void DontMove()
	{
		_target = transform.position;
	}
	
	public Vector2 GetDirectionPlayer()
	{
		return _direction;
	}	
}
