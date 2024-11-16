using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
	[SerializeField] private Transform _player;
	private Animator _animator;
	private BoxCollider2D _boxCollider;
	public float radius = 1.5f;
	public float speed = 2f;
	[SerializeField] private float angle;
	private float timeDeploy = 3f;
	private float timeTemp = 0f;
	private float CDs = 5f;
	private float x;
	private float y;
	private bool isActive;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
		_boxCollider = GetComponent<BoxCollider2D>();
	}

	private void Start()
	{
		timeTemp = 0;
		isActive = true;
	}

	private void FixedUpdate()
	{
		timeTemp += Time.fixedDeltaTime;
		if(timeTemp >= CDs)
		{
			isActive = true;
			timeTemp = 0;
		}	

		if (isActive)
		{
			StartCoroutine(activeWeapon());
		}
		CalcPotison();
	}

	private void CalcPotison()
	{
		if (angle >= Mathf.PI * 2f)
		{
			angle -= Mathf.PI * 2f;
		}
		x = _player.position.x + Mathf.Cos(angle) * radius;
		y = _player.position.y + Mathf.Sin(angle) * radius;
		transform.position = new Vector3(x, y, transform.position.z);
		angle += speed * Time.fixedDeltaTime;
	}

	private IEnumerator activeWeapon()
	{
		isActive = false;
		_boxCollider.enabled = true;
		_animator.SetBool("isActive",true);
		yield return new WaitForSeconds(timeDeploy);
		_animator.SetBool("isActive", false);
	}

	private void setInActiveCollider()
	{
		_boxCollider.enabled = false;
	}
}
