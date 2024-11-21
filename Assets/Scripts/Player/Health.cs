using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
	[SerializeField] private Image _imgHealthBar;
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
	}
}
