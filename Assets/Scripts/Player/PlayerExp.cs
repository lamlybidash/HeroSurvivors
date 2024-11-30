using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExp : MonoBehaviour
{
	private int _level;
	private double _ExpNext;
	private double _ExpCurrent;
	private void Start()
	{
		_level = 1;
		_ExpCurrent = 0;
		_ExpNext = 5;
	}


}
