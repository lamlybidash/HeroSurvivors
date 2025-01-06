using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
	private float _dame = 0;
	private float _speedAttack;
	private float _areaAttack;
	private bool _inAreAttack = false;
	private bool _canGiveDame = true;
	private EnemyController _ec;

	private void Start()
	{
		_ec = transform.parent.GetComponent<EnemyController>();
	}
	// List<BulletEnemy> // BulletEnemy chua dc tao
	public void GiveDame(GameObject target, bool isLongRange)
	{
		if (!_canGiveDame) 
		{
			return;
		}
		if(isLongRange)
		{
			// active bullet, sinh new if so dan toi da
			_ec.CreateBullet(transform, target.transform, _dame);
		}
		else
		{
			target.GetComponent<Health>().TakeDame(_dame);
		}
		StartCoroutine(CantGiveDame());
	}

	private IEnumerator CantGiveDame()
	{
		_canGiveDame = false;
		yield return new WaitForSeconds(1 / _speedAttack);
		_canGiveDame = true;
	}

	public void ResetCanGiveDame()
	{
		_canGiveDame = true;
	}

	public void SetUpData(float dame, float speedAttack)
	{
		_dame = dame;
		_speedAttack = speedAttack;
	}


	//private void OnCollisionEnter2D(Collision2D collision)
	//{
	//	if (collision.gameObject.tag == "Player" && _canGiveDame)
	//	{
	//		collision.gameObject.GetComponent<Health>().TakeDame(_dame);
	//		StartCoroutine(CantGiveDame());
	//	}
	//}

	//private void OnTriggerStay2D(Collider2D collision)
	//{
	//	if(collision.gameObject.tag == "Player" && _canGiveDame)
	//	{
	//		collision.gameObject.GetComponent<Health>().TakeDame(_dame);
	//		StartCoroutine(CantGiveDame());
	//	}
	//}
}
