using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExp : MonoBehaviour
{
	[SerializeField] private WeaponsController _wc;
	[SerializeField] private Image expBar;
	[SerializeField] private TextMeshProUGUI _levelText;
	private int _level;
	private double _ExpNext;
	private double _ExpCurrent;
	private void Start()
	{
		ResetLevelChar();
	}

	public void GainExp(double expAmount)
	{
		_ExpCurrent += expAmount;
		expBar.fillAmount = (float)(_ExpCurrent / _ExpNext);
		if(_ExpCurrent >= _ExpNext)
		{
			double remainder = _ExpCurrent - _ExpNext;
			_level++;
			_levelText.text = "Level : " + _level.ToString();
			_ExpCurrent = 0;
			_ExpNext += CheckExpNext();

			//choose weapon
			_wc.ChosseWeapons();

			//Debug.Log($"Level up : {_level} / Exp next: {_ExpNext} ");
			GainExp(remainder);
		}
	}

	private double CheckExpNext()
	{
		if(_level == 1)
		{
			return 5;
		}
		if(_level <= 20)
		{
			return 10;
		}
		if (_level <= 40)
		{
			return 12;
		}
		return 14;
	}

	public void ResetLevelChar()
	{
		expBar.fillAmount = 0;
		_level = 1;
		_ExpCurrent = 0;
		_ExpNext = 5;
		_levelText.text = "Level : " + _level.ToString();
	}
}
