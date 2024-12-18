using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMushroom : Weapons
{
	[SerializeField] private GameObject areaDame;
	private CircleCollider2D _cld;
	private SpriteRenderer _sprite;
	private Collider2D[] enemies;

	private void Awake()
	{
		_cld = GetComponent<CircleCollider2D>();
		_sprite = GetComponent<SpriteRenderer>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Enemy")
		{
			Explode();
		}
	}

	private void Explode()
	{
		_cld.enabled = false;
		_sprite.enabled = false;
		LayerMask enemyLayer = LayerMask.GetMask("Enemy");
		enemies = Physics2D.OverlapCircleAll(transform.position, area, enemyLayer);
		StartCoroutine(ActiveArea());
	}

	private IEnumerator ActiveArea()
	{
		float scaleFactor = area / areaDame.transform.localScale.x;

		//areaDame.transform.localScale *= area;
		areaDame.transform.localScale = new Vector3(scaleFactor,scaleFactor,1);

		areaDame.gameObject.SetActive(true);
		//dame enemy
		foreach (Collider2D enemy in enemies)
		{
			BurnEffect burn = new BurnEffect(damage, duration,0.5f);
			enemy.GetComponent<EffectManager>().ExcuteEffect(burn);
		}

		yield return new WaitForSecondsRealtime(0.3f);
		//areaDame.transform.localScale *= (1 / area);
		areaDame.transform.localScale = new Vector3(1, 1, 1);
		areaDame.gameObject.SetActive(false);

		gameObject.SetActive(false);

	}

}
