using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
	[SerializeField] private Image _imgHealthBar;
	private float HPTotal = 100;
	private float HPCurrent = 100;
	private void Start()
	{
		_imgHealthBar.fillAmount = 1;
	}


	public void TakeDame(float dame)
	{
		HPCurrent = Mathf.Clamp(HPCurrent - dame,0,HPTotal);
		_imgHealthBar.fillAmount = HPCurrent / HPTotal;
	}	
}
