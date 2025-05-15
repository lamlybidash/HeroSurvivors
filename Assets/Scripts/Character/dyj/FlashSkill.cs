using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlashSkill : Skill
{
	private Vector3 _target;
	private float _distanceMax = 2.5f;

	private void Start()
	{
		_id = "flash";
		_CDs = 10;
		_typeSkill = 'E';
		_imgArea.transform.localScale = new Vector3(_distanceMax * 2 , _distanceMax * 2 , 1);
		InitData();
	}

	//private void GetPlayer()
	//{
	//	_player = transform.parent;
	//}
	private void SetTarget()
	{
		Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mouseWorldPosition.z = 0;
		if (Vector3.Distance(_player.position, mouseWorldPosition) > _distanceMax)
		{
			Vector3 direction = (mouseWorldPosition - _player.position).normalized;
			_target = _player.position + direction * _distanceMax;
		}
		else
		{
			_target = mouseWorldPosition;
		}
	}
	private void FlashS()
	{
        _imgArea.transform.localScale = new Vector3(_distanceMax * 2 , _distanceMax * 2 , 1);
		//GetPlayer();
		SetTarget();
		_player.position = _target;
		_player.GetComponent<PlayerMovement>().DontMove();
	}

    public override void UseSkill()
    {
        if (_skillCDs.CanUseSkill() == false)
        {
            return;
        }
        _skillCDs.UseSkill();
        FlashS();
    }
}
