using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomDame : MonoBehaviour
{
	[SerializeField] private CircleCollider2D _collider;
	private float damage;
	private float duration;
	//default scale = 2
	public void SetUpData(float damagex, float durationx)
	{
		damage = damagex;
		duration = durationx;
	}	

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Enemy")
		{
			BurnEffect burn = new BurnEffect(damage, duration, 0.5f);
			collision.GetComponent<EffectManager>().ExcuteEffect(burn);
		}
	}
}
