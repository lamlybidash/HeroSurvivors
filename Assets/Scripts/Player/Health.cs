using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
	[SerializeField] private Image _imgHealthBar;
	[SerializeField] private GameController _gc;
	private float _HPTotal = 100;
	private float _HPCurrent = 100;
	private void Start()
	{
		_imgHealthBar.fillAmount = 1;
	}
	public void TakeDame(float dame)
	{
		_HPCurrent = Mathf.Clamp(_HPCurrent - dame, 0, _HPTotal);
		_imgHealthBar.fillAmount = _HPCurrent / _HPTotal;
		if (_HPCurrent <= 0)
		{
			_gc.IsOverGame(true);
		}
	}

	public void Healling(float valueHeal)
	{
		_HPCurrent = Mathf.Clamp(_HPCurrent + valueHeal, 0, _HPTotal);
		_imgHealthBar.fillAmount = _HPCurrent / _HPTotal;
		// sound healing
		
	}

	public void Revive()
	{
		TakeDame(-_HPTotal);
	}

	public void SetUpTotalHealth(float x)
	{
		_HPTotal = x;
		_HPCurrent = _HPTotal;
	}	
}
