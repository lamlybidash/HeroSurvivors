using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WLevelUp
{
	public string level;
	public string attribute;
	public string amount;
}

[System.Serializable]
public class WDataUpdate
{
	public List<WeaponsData> ListWeapon;
}

public abstract class Weapons : MonoBehaviour
{
	protected string nameW=>data.nameW;
	protected float damage => data.damage;
	protected int projectile {
		get {
			return data.projectile;
		}
		set
		{

		data.projectile = value; 
		}
	}
	protected float countdown=>data.countdown;
	private float area;
	private float speed;
	private float duration;
	private List<WLevelUp> levelup;
	private WeaponsData data;
}

public abstract class AbstractWeapons : MonoBehaviour
{
	private WeaponsData _initializedData;
	public void SetupData(WeaponsData data)
	{
		_initializedData = data;
		//var x = new AbstractWeapons(null);
		var y = new TestX();
		y.SetupData(null);
	}
}
public class TestX : AbstractWeapons
{

}


