using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlashSkill : MonoBehaviour
{
	[SerializeField] private SkillCDs _skillCDs;
	[SerializeField] private SpriteRenderer _imgArea;
	private Transform _player;
	private Vector3 _target;
	private float _distanceMax = 2.5f;

	private void Start()
	{
		_skillCDs.SetupDataSkill(10);
		_imgArea.transform.localScale = new Vector3(_distanceMax * 2 , _distanceMax * 2 , 1);
	}
	private void Update()
	{
		if(Input.GetKey(KeyCode.E))
		{
			if (_skillCDs.CanUseSkill() == true)
			{
				FlashS();
			}
		}	
	}

	private void GetPlayer()
	{
		_player = transform.parent;
		Debug.Log(_player);
		Debug.Log(_player.gameObject.name);
	}

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
		GetPlayer();
		SetTarget();

		_player.position = _target;
		_skillCDs.UseSkill();
		_player.GetComponent<PlayerMovement>().DontMove();
	}	
}
