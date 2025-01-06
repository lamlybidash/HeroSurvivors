using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : Weapons
{
	[SerializeField] private List<ProjectileWind> projectileWinds;


	private AudioSource _audioSource;
	private bool _soundPlaying;

	private void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	private void Start()
	{
		_level = 1;
		InitData();
		AddProjectile(projectile);
		SetUpDataProjectile();
		_soundPlaying = false;
		_audioSource.clip = ac;
		_audioSource.loop = true;
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
		Debug.Log(player);
		for (j = 0; j < projectileWinds.Count; j++)
		{
			//Set goc cho 1 projectile
			projectileWinds[j].SetUpAngle(j * ((2 * Mathf.PI) / projectileWinds.Count));
			projectileWinds[j].SetUpData(this);
			projectileWinds[j].SetPlayer(player);
		}
	}
	public override void LevelUp(int attributef, float amountf)
	{
		base.LevelUp(attributef, amountf); //level++

		if(attributef == 2)
		{
			projectile += (int)amountf;
			AddProjectile((int)amountf);
		}
		SetUpDataProjectile();
	}

	public void PlaySound()
	{
		if(_soundPlaying == false)
		{
			_soundPlaying = true;
			_audioSource.Play();
		}
	}

	public void StopSound()
	{
		if(_soundPlaying)
		{
			_soundPlaying = false;
			_audioSource.Stop();
		}	
	}	
}
