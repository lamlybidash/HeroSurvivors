using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
	[SerializeField] private Enemy enemyComponent;
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
		PopupController.instance.PopupWorld(dame.ToString(), transform.position);
		if (_healthCurrent == 0 )
		{
			_indexForRandom = Random.Range(0,100);
			if(_indexForRandom < 75) //75% rơi ra viên exp
			{
				ExpController.instance.DropExp(transform, _expDrop);
			}
			if(_indexForRandom < 10) //10% rơi ra vật phẩm ngẫu nhiên
			{
				ItemDropManager.instance.DropItem(transform);
			}	
			_isDie = true;
			enemyComponent.IncreaseScore(1);
			gameObject.SetActive(false);
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
}
