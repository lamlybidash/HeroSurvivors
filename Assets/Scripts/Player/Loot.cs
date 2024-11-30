using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
	private PlayerExp _playerExp;


	private void Awake()
	{
		_playerExp = transform.parent.GetComponent<PlayerExp>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Exp")
		{
		}	
	}
}
