using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
	private PlayerExp _playerExp;
	private float multiplier = 0;
	private void Awake()
	{
		_playerExp = transform.parent.GetComponent<PlayerExp>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Exp")
		{
			collision.gameObject.GetComponent<ExpMovement>().SetPlayer(transform);
			collision.gameObject.GetComponent<ExpMovement>().enabled = true;
		}

		if (collision.tag == "ItemDrop")
		{
			collision.gameObject.GetComponent<ItemDropMovement>().SetPlayer(transform);
			collision.gameObject.GetComponent<ItemDropMovement>().enabled = true;
		}
	}

	public void IncreaseArea(float multiplierx)
	{
		multiplier = Mathf.Clamp(multiplier + multiplierx,0.001f,9999f);
		transform.localScale = new Vector3(1, 1, 1);
		transform.localScale *= multiplier;
	}

	public void ResetArea()
	{
		Debug.Log("Reset Area");
		multiplier = 1;
		transform.localScale = new Vector3(1, 1, 1);
	}	
}
