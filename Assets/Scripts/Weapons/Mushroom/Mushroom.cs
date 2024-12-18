using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Weapons
{
	[SerializeField] private List<ProjectileMushroom> projectileMushroom;
	private void Start()
	{
		_level = 0;
		_level = 1;
		InitData();
		AddProjectile(projectile);
		SetUpDataProjectile();
	}

	private void AddProjectile(int count)
	{
		int j;
		for (j = 1; j <= count; j++)
		{
			//Thêm tia
			GameObject newPro = Instantiate(projectilePf);
			newPro.transform.parent = transform;
			projectileMushroom.Add(newPro.GetComponent<ProjectileMushroom>());
		}
	}
	private void SetUpDataProjectile()
	{
		int j;
		Debug.Log(player);
		for (j = 0; j < projectileMushroom.Count; j++)
		{
			projectileMushroom[j].SetUpData(this);
			projectileMushroom[j].SetPlayer(player);
		}
	}

}
