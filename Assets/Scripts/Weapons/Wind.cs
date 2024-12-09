using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : Weapons
{
	private int _level;
	[SerializeField] private List<ProjectileWind> projectileWinds;
	private void Start()
	{
		_level = 0;
	}



}
