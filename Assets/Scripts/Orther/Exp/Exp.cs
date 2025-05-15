using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
	[SerializeField] private double _maxExp;
	[SerializeField] private double _expAmount;

	public void SetUpExp(double expAmount)
	{
		_expAmount = expAmount;
	}
	public void SetUpLocation(Transform location)
	{
		transform.position = new Vector3(location.position.x, location.position.y, transform.position.z);
	}
	public double GetExp()
	{
		return _expAmount;
	}
	public double GetExpMax()
	{
		return _maxExp;
	}
}
