using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : Weapons
{
	private List<ProjectileWind> projectileWinds = new List<ProjectileWind>();
	private bool _soundPlaying;
	private void Start()
	{
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
			//Sound cũ loop
			//SoundManager.instance.PlaySoundLoop(ac, duration);

			//Sound mới one shot
			SoundManager.instance.PlayOneSound(ac);
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
	}
	public override void SetUpStartGame(Character charx)
	{
        base.SetUpStartGame(charx);
        AddProjectile(projectile);
        SetUpDataProjectile();
	}
    protected override void UpdateStat()
    {
        base.UpdateStat();
        SetUpDataProjectile();
    }
}