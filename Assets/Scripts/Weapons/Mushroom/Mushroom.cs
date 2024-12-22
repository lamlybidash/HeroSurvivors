using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Weapons
{
	[SerializeField] private List<ProjectileMushroom> projectileMushrooms;
	private void Start()
	{
		_level = 0;
		_level = 1;
		InitData();
		SetUpDataProjectile();
		StartCoroutine(PutMushroom());
	}
	private void AddProjectile(int count)
	{
		int j;
		for (j = 1; j <= count; j++)
		{
			//Thêm tia
			GameObject newPro = Instantiate(projectilePf);
			newPro.transform.parent = transform;
			projectileMushrooms.Add(newPro.GetComponent<ProjectileMushroom>());
		}
	}
	private void SetUpDataProjectile()
	{
		int j;
		Debug.Log(player);
		for (j = 0; j < projectileMushrooms.Count; j++)
		{
			projectileMushrooms[j].SetUpData(this);
			projectileMushrooms[j].SetPlayer(player);
		}
	}
	private IEnumerator PutMushroom()
	{
		while(true)
		{
			PutOne();
			yield return new WaitForSeconds(countdown);
		}	
	}	
	private int FindMushRoomIA()
	{
		int i;

		for (i = 0; i < projectileMushrooms.Count; i++)
		{
			if (projectileMushrooms[i].isActiveAndEnabled == false)
			{
				return i;
			}	
		}	
		return -1;
	}	
	private void PutOne()
	{
		int i;
		i = FindMushRoomIA();
		if (i != -1)
		{
			projectileMushrooms[i].ActiveProjectileMushroom();
		}
		else
		{
			GameObject projectileMushroom = Instantiate(projectilePf);
			projectileMushroom.transform.SetParent(transform);
			projectileMushrooms.Add(projectileMushroom.GetComponent<ProjectileMushroom>());
			projectileMushrooms[projectileMushrooms.Count - 1].SetUpData(this);
			projectileMushrooms[projectileMushrooms.Count - 1].SetPlayer(player);
			projectileMushrooms[projectileMushrooms.Count - 1].ActiveProjectileMushroom();
		}	
	}
	public override void LevelUp(int attributef, float amountf)
	{
		base.LevelUp(attributef, amountf);
		if (attributef == 2)
		{
			projectile += (int)amountf;
			AddProjectile((int)amountf);
		}
		SetUpDataProjectile();
	}
}
