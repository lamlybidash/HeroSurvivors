using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsController : MonoBehaviour
{
	[SerializeField] private GameController _gc;
	private WeaponsReadData _readerW;
	[SerializeField] private List<OptionWeapon> optionWeapons;
	[SerializeField] private GameObject UIChooseW;
	[SerializeField] private List<Weapons> _listWeaponFull; // lay data
	private List<Weapons> _listWeaponTemp; // list de chon nang cap
	private List<Weapons> _weapons; // list wp hien tai
	private int WeaponCountMax = 3;
	private Transform _player;
	private void Awake()
	{
		_readerW = GetComponent<WeaponsReadData>();
		_weapons = new List<Weapons>();
	}

	private void Start()
	{
		WeaponCountMax = 3;
		_listWeaponTemp = new List<Weapons>(_listWeaponFull); // sao chep != (list a = list b)
		//SetPlayer(_player);
		//test
		//InitDataAllWeapons();
	}

	public void ChosseWeapons()
	{
		_gc.PauseGame(true);
		int OptionWeaponCount = 1;
		UIChooseW.SetActive(true);

		// test money
		//optionWeapons[0].SetUpMoneyAndHeal(1);
		//optionWeapons[2].SetUpMoneyAndHeal(2);
		//return;
		//

		if (_listWeaponTemp.Count == 0)
		{
			optionWeapons[0].SetUpMoneyAndHeal(1);
			optionWeapons[2].SetUpMoneyAndHeal(2);
			return;
		}

		if (_listWeaponTemp.Count >= 3)
		{
			OptionWeaponCount = 3;
		}
		else
		{
			OptionWeaponCount = _listWeaponTemp.Count;
		}

		List<int> rdl; // randomlist
		rdl = new List<int>(ChooseRandomNumber(0, _listWeaponTemp.Count, OptionWeaponCount));


		int i = 0;
		for (i = 0; i < OptionWeaponCount; i++)
		{
			optionWeapons[i].SetUpData(_listWeaponTemp[rdl[i]]);
		}
	}

	public void LevelUpW(Weapons w)
	{
		if(w._level == 0)
		{
            w.gameObject.SetActive(true);
            w._level = 1;
			_weapons.Add(w);
			w.SetUpStartGame(_player.GetComponent<Character>());
			//Loại bỏ các vũ khí khác khi số lượng vũ khí đạt tối đa
			if (_weapons.Count == WeaponCountMax)
            {
                _listWeaponTemp.Clear();
                foreach (Weapons wi in _weapons)
                {
                    if (wi._level <= wi.data.levelup.Count)
                    {
                        _listWeaponTemp.Add(wi);
                    }
                }
            }
        }
		else
		{
            w.LevelUp(w.data.levelup[w._level - 1].attribute, w.data.levelup[w._level - 1].amount);
        }
        if (w._level >= w.data.levelup.Count + 1)
		{
			DailyQuestEvent.WeaponLvMax();
			_listWeaponTemp.Remove(w);
			return;
		}
    }

	public void SetPlayer(Transform playerx)
	{
		_player = playerx;
		foreach (Weapons item in _listWeaponFull)
		{
			item.SetPlayer(_player);
		}
	}

	public void ResetWeapon()
	{
		foreach (Weapons w in _weapons)
		{
			w.ResetWeapon();
			w.ResetBonusAndMulti();
		}
		_weapons.Clear();
		_listWeaponTemp = new List<Weapons>(_listWeaponFull);
	}
	//Test
	//public void SetUpStartGame()
	//{
	//	foreach (Weapons item in _listWeaponFull)
	//	{
	//		item.SetUpStartGame(_player.GetComponent<Character>());
	//	}
	//}	
	public void SetUpStartingWeapon(string x)
	{
		_listWeaponTemp = new List<Weapons>(_listWeaponFull);
		foreach (Weapons w in _listWeaponFull)
		{
			if (w.data.nameW == x)
			{
				LevelUpW(w);
				break;
			}
		}	
	}

	//test
	//private void InitDataAllWeapons()
	//{
	//	foreach (Weapons w in _listWeaponFull)
	//	{
	//		w.InitData();
	//	}
	//}
	private List<int> ChooseRandomNumber(int min, int max, int count) // 0 3 3
	{
		List<int> listInt = new List<int>();
		int i = 0, x;
		bool check;
		// i: 0 1
		// add: 0
		while (i < count) 
		{
			do
			{
				check = true;
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
