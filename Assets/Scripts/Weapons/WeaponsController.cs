using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
	[SerializeField] private GameController _gc;
	private WeaponsReadData _readerW;
	[SerializeField] private List<OptionWeapon> optionWeapons;
	[SerializeField] private GameObject UIChooseW;
	private testWeaponList _listWeapon;
	private void Awake()
	{
		_readerW = GetComponent<WeaponsReadData>();
	}
	private void Start()
	{
		_listWeapon = _readerW.getListWeapon();
	}

	public void ChosseWeapons()
	{
		_gc.PauseGame(true);
		int i = 0;
		foreach (testWeapon item in _listWeapon.weapons)
		{
			if (i < optionWeapons.Count)
			{
				optionWeapons[i].SetUpData(item);
				i++;
			}
		}
		UIChooseW.SetActive(true);
	}

}
