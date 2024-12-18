using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
	[SerializeField] private GameController _gc;
	private WeaponsReadData _readerW;
	[SerializeField] private List<OptionWeapon> optionWeapons;
	[SerializeField] private GameObject UIChooseW;
	[SerializeField] private List<Weapons> _listWeaponFull; // lay data
	private List<Weapons> _listWeaponTemp; // list de chon nang cap
	private List<Weapons> _weapons; // list wp hien tai
	private int WeaponCountMax;
	private void Awake()
	{
		_readerW = GetComponent<WeaponsReadData>();
		_weapons = new List<Weapons>();
	}

	private void Start()
	{
		WeaponCountMax = 6;
		_listWeaponTemp = new List<Weapons>(_listWeaponFull); // sao chep != (list a = list b)
		SetPlayer();

	}

	public void ChosseWeapons()
	{
		_gc.PauseGame(true);
		//List<int> rdl; // randomlist
		//rdl = new List<int>(ChooseRandomNumber(0, _listWeaponTemp.Count, 3));


		//if(_weapons.Count < WeaponCountMax)
		//{
		//	rdl = new List<int>(ChooseRandomNumber(0, _listWeaponTemp.Count, 3));
		//}
		//else
		//{
		//	rdl = new List<int>(ChooseRandomNumber(0, WeaponCountMax, 3));
		//}


		int i = 0;
		foreach (Weapons item in _listWeaponTemp)
		{
			if (i < optionWeapons.Count)
			{
				optionWeapons[i].SetUpData(item);
				i++;
			}
		}
		UIChooseW.SetActive(true);
	}

	public void LevelUpW(Weapons w)
	{
		w.LevelUp(w.data.levelup[w._level - 1].attribute, w.data.levelup[w._level - 1].amount);
	}

	public void SetPlayer()
	{
		foreach (Weapons item in _listWeaponFull)
		{
			item.SetPlayer(_gc.CharActive().transform);
		}
	}

	private List<int> ChooseRandomNumber(int min, int max, int count)
	{
		List<int> listInt = new List<int>();
		int i = 0, x;
		bool check;
		while (i < count)
		{
			check = true;
			do
			{
				x = Random.Range(min, max);
				foreach (int k in listInt)
				{
					if (k == x)
					{
						check = false;
						break;
					}
				}
				if (check)
				{
					listInt.Add(x);
				}
			} while (check == false);

			i++;
		}
		return listInt;
	}
}
