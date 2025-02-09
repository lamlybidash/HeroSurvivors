using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMushroom : Weapons
{
	[SerializeField] private GameObject areaDame;
	[SerializeField] private CircleCollider2D _cld;
	[SerializeField] private SpriteRenderer _sprite;
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
			SoundManager.instance.PlayOneSound(ac);
			Explode();
		}
	}

	private void Explode()
	{
		_cld.enabled = false;
		_sprite.enabled = false;
		//LayerMask enemyLayer = LayerMask.GetMask("Enemy");
		//enemies = Physics2D.OverlapCircleAll(transform.position, area, enemyLayer);
		StartCoroutine(ActiveArea());
	}

	private IEnumerator ActiveArea()
	{
		areaDame.transform.localScale = new Vector3(1, 1, 1);
		areaDame.transform.localScale *= area;
		areaDame.GetComponent<MushroomDame>().SetUpData(damage,duration);
		areaDame.gameObject.SetActive(true);
		yield return new WaitForSeconds(0.3f);
		areaDame.gameObject.SetActive(false);
		gameObject.SetActive(false);
	}

	public void ActiveProjectileMushroom()
	{
		areaDame.transform.localScale = new Vector3(1, 1, 1);
		_cld.enabled = true;
		_sprite.enabled = true;
		transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
		gameObject.SetActive(true);
	}
}
