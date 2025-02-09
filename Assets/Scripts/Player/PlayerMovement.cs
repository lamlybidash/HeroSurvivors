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
		Debug.Log("Awake : " + name);
		_animator = GetComponent<Animator>();
		_camera = Camera.main.GetComponent<CameraController>();
		_rg = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		Debug.Log("Start : " + name);
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
		if (Input.GetMouseButton(1))
		{
			_target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			_target.z = transform.position.z;
		}
		setupDirection(_target);

		_animator.SetBool("isRun", (Mathf.Abs(_xRunVt) > 0.01 || Mathf.Abs(_yRunVt) > 0.01));
		_animator.SetFloat("RunHor", _xRunVt);
		_animator.SetFloat("RunVer", _yRunVt);
		_animator.SetFloat("IdleHor", _xIdleVt);
		_animator.SetFloat("IdleVer", _yIdleVt);
	}
	private void FixedUpdate()
	{
		_targetRG = Vector3.MoveTowards(transform.position, _target, _speed * Time.fixedDeltaTime);
		_rg.MovePosition(_targetRG);
	}


	private void setupDirection(Vector3 target)
	{
		_xRunVt = target.x - transform.position.x;
		_yRunVt = target.y - transform.position.y;
		if (Mathf.Abs(_xRunVt) < 0.001 && Mathf.Abs(_yRunVt) < 0.01)
		{
			return;
		}

		if (_xRunVt == 0)
		{
			_yRunVt = Mathf.Sign(_yRunVt);
			_xIdleVt = _xRunVt;
			_yIdleVt = _yRunVt;
			return;
		}

		if (_yRunVt == 0)
		{
			_xRunVt = Mathf.Sign(_xRunVt);
			_xIdleVt = _xRunVt;
			_yIdleVt = _yRunVt;
			return;
		}

		if (Mathf.Abs(_xRunVt) > Mathf.Abs(_yRunVt))
		{
			_yRunVt = _yRunVt / Mathf.Abs(_xRunVt);
			_xRunVt = _xRunVt / Mathf.Abs(_xRunVt);
		}
		else
		{
			_xRunVt = _xRunVt / Mathf.Abs(_yRunVt);
			_yRunVt = _yRunVt / Mathf.Abs(_yRunVt);
		}
		_xIdleVt = _xRunVt;
		_yIdleVt = _yRunVt;
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
}
