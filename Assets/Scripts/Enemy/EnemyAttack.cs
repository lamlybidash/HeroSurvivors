using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
	private float _dame = 5;
	private bool _canGiveDame = true;

	//private void OnCollisionEnter2D(Collision2D collision)
	//{
	//	if (collision.gameObject.tag == "Player" && _canGiveDame)
	//	{
	//		collision.gameObject.GetComponent<Health>().TakeDame(_dame);
	//		StartCoroutine(CantGiveDame());
	//	}
	//}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player" && _canGiveDame)
		{
			collision.gameObject.GetComponent<Health>().TakeDame(_dame);
			StartCoroutine(CantGiveDame());
		}
	}

	//private void OnTriggerStay2D(Collider2D collision)
	//{
	//	if(collision.gameObject.tag == "Player" && _canGiveDame)
	//	{
	//		collision.gameObject.GetComponent<Health>().TakeDame(_dame);
	//		StartCoroutine(CantGiveDame());
	//	}
	//}

	private IEnumerator CantGiveDame()
	{
		_canGiveDame = false;
		yield return new WaitForSeconds(0.5f);
		_canGiveDame = true;
	}
}
