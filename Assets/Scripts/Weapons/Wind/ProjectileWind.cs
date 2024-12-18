using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWind : Weapons
{
	private Animator _animator;
	private BoxCollider2D _boxCollider;
	private float angle;
	private float timeTemp = 0f;
	//private float percentCDs = 0f;
	private float x;
	private float y;
	private bool isActive;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
		_boxCollider = GetComponent<BoxCollider2D>();
		InitData();
	}

	private void Start()
	{
		timeTemp = countdown;
		isActive = true;
	}

	private void FixedUpdate()
	{
		timeTemp += Time.fixedDeltaTime;
		if (timeTemp >= countdown)
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
		x = player.position.x + Mathf.Cos(angle) * area;
		y = player.position.y + Mathf.Sin(angle) * area;
		transform.position = new Vector3(x, y, transform.position.z);
		angle += speed * Time.fixedDeltaTime;
	}

	private IEnumerator activeWeapon()
	{
		isActive = false;
		_boxCollider.enabled = true;
		_animator.SetBool("isActive", true);
		yield return new WaitForSeconds(duration);
		_animator.SetBool("isActive", false);
	}

	//public void UpDateLevel(int attributef, float amountf)
	//{
	//	switch(attributef)
	//	{
	//		case 1:
	//			{
	//				damage += amountf;
	//				break;
	//			}

	//		case 3:
	//			{
	//				percentCDs += amountf/100;
	//				break;
	//			}

	//		case 4:
	//			{
	//				area += amountf;
	//				break;
	//			}
	//		case 5:
	//			{
	//				speed += amountf;
	//				break;
	//			}
	//		case 6:
	//			{
	//				duration += amountf;
	//				break;
	//			}
	//	}	
	//}

	public void SetUpAngle(float x)
	{
		isActive = false;
		_boxCollider.enabled = false;
		_animator.SetBool("isActive", false);
		timeTemp = countdown;
		angle = x;
	}
	private void setInActiveCollider()
	{
		_boxCollider.enabled = false;
	}


	private void GiveDame(float dame, GameObject target)
	{
		target.GetComponent<HealthEnemy>().TakeDame(dame);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Enemy")
		{
			GiveDame(damage, collision.gameObject);
		}
	}
}
