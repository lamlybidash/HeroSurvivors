using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : Weapons
{
	[SerializeField] private List<ProjectileWind> projectileWinds;


	private bool _soundPlaying;



	private void Start()
	{
		InitData();
		AddProjectile(projectile);
		SetUpDataProjectile();
		_soundPlaying = false;
	}

	private void AddProjectile(int count)
	{
		int j;
		for (j = 1; j <= count; j++)
		{
			//Thêm tia
			GameObject newPro = Instantiate(projectilePf);
			newPro.transform.parent = transform;
			projectileWinds.Add(newPro.GetComponent<ProjectileWind>());
		}
	}
	private void SetUpDataProjectile()
	{
		int j;
		//Debug.Log("Char Active hiện tại: " + player.name);
		for (j = 0; j < projectileWinds.Count; j++)
		{
			//Set goc cho 1 projectile
			projectileWinds[j].SetUpAngle(j * ((2 * Mathf.PI) / projectileWinds.Count));
			projectileWinds[j].SetUpData(this);
			projectileWinds[j].SetPlayer(player);
			projectileWinds[j].gameObject.SetActive(true);
		}
	}
	public override void LevelUp(int attributef, float amountf)
	{
		base.LevelUp(attributef, amountf); //level++
		foreach (Weapons x in projectileWinds)					
		{
			Destroy(x.gameObject);
		}
		projectileWinds.Clear();
		AddProjectile(projectile);
		SetUpDataProjectile();
	}

	public void PlaySound()
	{
		if(_soundPlaying == false)
		{
			_soundPlaying = true;
			SoundManager.instance.PlaySoundLoop(ac, duration);
		}
	}

	public void StopSound()
	{
		if(_soundPlaying)
		{
			_soundPlaying = false;
		}	
	}

	public override void ResetWeapon()
	{
		base.ResetWeapon();
        foreach(Weapons x in projectileWinds)
		{
			Destroy(x.gameObject);
		}
        projectileWinds.Clear();
		AddProjectile(projectile);
		SetUpDataProjectile();
	}

	public override void SetUpStartGame()
	{
		base.SetUpStartGame();
		SetUpDataProjectile();
	}
}
