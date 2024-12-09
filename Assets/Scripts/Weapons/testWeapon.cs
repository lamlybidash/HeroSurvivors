using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class testWeapon
{
	public string title;
	public string describle;
	public string image;
}


[System.Serializable]
public class testWeaponList
{
	public testWeapon[] weapons;
}