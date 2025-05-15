using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBallBat : Weapons
{
	private List<ProjectileBaseBallBat> _projectileBaseBallBats = new List<ProjectileBaseBallBat>();

	void Start()
    {
		//StartCoroutine(AttackBonk());
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
			SoundManager.instance.PlayOneSound(ac);
			for (j = 0; j < _projectileBaseBallBats.Count; j++)
			{
				_projectileBaseBallBats[j].gameObject.SetActive(true);
				_projectileBaseBallBats[j].StartAttack();
			}
			yield return new WaitForSeconds(countdown);
		}
	}

	private void AddProjectile(int x)
	{
		int i;
		for (i = 0; i < x; i++)
		{
			GameObject projectilex = Instantiate(projectilePf);
			projectilex.transform.parent = transform;
			_projectileBaseBallBats.Add(projectilex.GetComponent<ProjectileBaseBallBat>());
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

    protected override void UpdateStat()
    {
        base.UpdateStat();
		SetUpDataProjectile();
    }
	public override void SetUpStartGame(Character charx)
	{
		base.SetUpStartGame(charx);
		AddProjectile(projectile);
		SetUpDataProjectile();
		StartCoroutine(AttackBonk());
	}
}
