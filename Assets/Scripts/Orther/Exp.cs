using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
	[SerializeField] private double _maxExp;
	[SerializeField] private double _expAmount;


	public void SetUpExp(int expAmount)
	{
		_expAmount = expAmount;
	}

	public double GetExp()
	{
		return _expAmount;
	}	
}
