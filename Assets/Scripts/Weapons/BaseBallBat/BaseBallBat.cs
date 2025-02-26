using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBallBat : Weapons
{
	[SerializeField] private List<ProjectileBaseBallBat> _projectileBaseBallBats;

	void Start()
    {
        //InitData();
		//SetUpDataProjectile();
		countdown = 2;
		StartCoroutine(AttackBonk());
	}

	private void SetUpDataProjectile()
	{
		int j;
		for (j = 0; j < _projectileBaseBallBats.Count; j++)
		{
			_projectileBaseBallBats[j].SetUpData(this);
			_projectileBaseBallBats[j].SetPlayer(player);
		}
	}

	private IEnumerator AttackBonk()    // vụt mỗi x giây
	{
		int j;
		while (true)
		{
			for (j = 0; j < _projectileBaseBallBats.Count; j++)
			{
				_projectileBaseBallBats[j].gameObject.SetActive(true);
				_projectileBaseBallBats[j].StartAttack();
			}
			yield return new WaitForSeconds(countdown);
		}
	}

	public override void LevelUp(int attributef, float amountf)
	{
		base.LevelUp(attributef, amountf);
		SetUpDataProjectile();
	}

	public override void ResetWeapon()
	{
		base.ResetWeapon();
	}

	public override void SetUpStartGame()
	{
		base.SetUpStartGame();
		StartCoroutine(AttackBonk());
	}
}
