﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
	[SerializeField] private Enemy enemyComponent;
	[SerializeField] private EffectManager _EF;
	private float _healthTotal;
	private float _healthCurrent;
	private double _expDrop;
	private bool _isDie;
	private int _indexForRandom;

	private void Start()
	{
		_healthCurrent = _healthTotal;
	}
	public void setupEnemy()
	{
		if(_healthCurrent <= 0)
		{
			_healthCurrent = _healthTotal;
			_isDie = false;
		}
	}	
	public void TakeDame(float dame)
	{
        _healthCurrent = Mathf.Clamp(_healthCurrent - dame, 0, _healthTotal);
		PopupController.instance.PopupWorld(((int)dame).ToString(), transform.position);
		if (_healthCurrent == 0 )
		{

            gameObject.SetActive(false);
            _indexForRandom = Random.Range(0,100);
			if(_indexForRandom < 50) //50% rơi ra viên exp
			{
				ExpController.instance.DropExp(transform, _expDrop);
			}
			if(_indexForRandom < 2) //2% rơi ra vật phẩm ngẫu nhiên
			{
				ItemDropManager.instance.DropItem(transform);
			}	
			_isDie = true;
            _EF.ClearAllEffect();
            enemyComponent.IncreaseScore(1);
		}
	}

	public void SetUpData(float health, double expAmount)
	{
		_healthTotal = health;
		_expDrop = expAmount;
	}	

	public bool isDieF()
	{
		return _isDie;
	}
	public float GetHealthTotal()
	{
		return _healthTotal;
	}
}
