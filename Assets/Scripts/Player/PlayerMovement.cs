using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float _speed;
	[SerializeField] private Vector2 _velocity;
	private Animator _animator;
	private Vector3 _target;
	private float _xRunVt;
	private float _yRunVt;
	private float _xIdleVt;
	private float _yIdleVt;



	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	private void Start()
	{
		_target = transform.position;
	}

	private void Update()
	{
		if(Input.GetMouseButtonDown(1))
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
		//Debug.Log( new Vector2(_xVt, _yVt).magnitude);
		Debug.Log($"{_target.x} / {_target.y} - {transform.position.x} / {transform.position.y}" );
		Debug.Log($"{_xRunVt} / {_yRunVt}");

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

	private void FixedUpdate()
	{
		transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.fixedDeltaTime);
	}

}
