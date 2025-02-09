using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	
	private int i = 0;
    private Transform _player;

	
	private void FixedUpdate()
	{
		if (_player != null)
		{
			transform.position = new Vector3(_player.position.x, _player.position.y, transform.position.z);
		}
	}

	public void SetFollower(Transform trans)
	{
		i++;
		//Debug.Log(trans.name + i);
		_player = trans;
	}

}
