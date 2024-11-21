using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{


	public void ActiveEnemy(Vector3 pos)
	{
		transform.position = pos;
		gameObject.SetActive(true);
	}

}
