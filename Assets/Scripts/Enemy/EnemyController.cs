using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

	[SerializeField] private Camera _cam;
	private Vector3 _camBL;
	private Vector3 _camTR;
	[SerializeField] private GameObject[] enemies;

	private void Start()
	{
		CamCalc();
	}

	private void Update()
	{
		CamCalc();
		EnemyManager();
	}

	private void CreateEnemy()
	{
		int t = Random.Range(1, 5);
		float x=0, y=0;
		switch (t)
		{
			case 1:
				{
					x = _camBL.x;
					y = Random.Range(_camBL.y, _camTR.y);
					break;
				}
			case 2:
				{
					x = _camTR.x;
					y = Random.Range(_camBL.y, _camTR.y);
					break;
				}
			case 3:
				{
					x = Random.Range(_camBL.x, _camTR.x);
					y = _camBL.y;
					break;
				}
			case 4:
				{
					x = Random.Range(_camBL.x, _camTR.x);
					y = _camTR.y;
					break;
				}
			default:
				break;
		}
		enemies[FindEnemyInactive()].GetComponent<Enemy>().ActiveEnemy(new Vector3(x, y, 0));
		
	}

	private void EnemyManager()
	{
		if (FindEnemyInactive() != -1)
		{
			CreateEnemy();
		}
	}
	private void CamCalc()
	{
		_camBL = _cam.ViewportToWorldPoint(new Vector3(0, 0, _cam.nearClipPlane));
		_camTR = _cam.ViewportToWorldPoint(new Vector3(1, 1, _cam.nearClipPlane));
	}
	private int FindEnemyInactive()
	{
		int i;
		for (i = 0; i < enemies.Length; i++)
		{
			if(!enemies[i].activeInHierarchy)
			{
				return i;
			}	
		}
		return -1;
	}	
}
