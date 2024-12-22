using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

	[SerializeField] private Camera _cam;
	[SerializeField] private GameObject _enemyPf;
	private Vector3 _camBL;
	private Vector3 _camTR;
	[SerializeField] private List<Enemy> enemies;
	private bool _isPlay;
	private int _enemyCountMax;

	private void Start()
	{
		_enemyCountMax = 10;
		_isPlay = false;
		DestroyAllEnemy();
		CamCalc();
	}

	private void Update()
	{
		CamCalc();
		EnemyManager();
	}

	private void CreateEnemy(int indexx)
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

		if(indexx != -1)
		{
			enemies[indexx].ActiveEnemy(new Vector3(x, y, 0));
		}
		else
		{
			// tam thoi co 1 loai quai
			GameObject _enemyx = Instantiate(_enemyPf);
			_enemyx.transform.SetParent(transform);
			_enemyx.GetComponent<Enemy>().ActiveEnemy(new Vector3(x, y, 0));
			enemies.Add(_enemyx.GetComponent<Enemy>());
		}	

	}

	private void EnemyManager()
	{
		int indexx;
		indexx = FindEnemyInactive();

		if(indexx == -1 && !(enemies.Count < _enemyCountMax))
		{
			return;
		}

		CreateEnemy(indexx);

		//if (indexx != -1)
		//{
		//	CreateEnemy(indexx);
		//}
  //      else
  //      {
		//	if (enemies.Count < _enemyCountMax)
		//	{
		//		// them quai vao list
		//		CreateEnemy(indexx);
		//	}
		//}

    }
	private void CamCalc()
	{
		_camBL = _cam.ViewportToWorldPoint(new Vector3(0, 0, _cam.nearClipPlane));
		_camTR = _cam.ViewportToWorldPoint(new Vector3(1, 1, _cam.nearClipPlane));
	}
	private int FindEnemyInactive()
	{
		int i;
		for (i = 0; i < enemies.Count; i++)
		{
			if(!enemies[i].gameObject.activeInHierarchy)
			{
				return i;
			}	
		}
		return -1;
	}	

	public void PlayGameStatus(bool status)
	{
		_isPlay	= status;
		if (_isPlay)
		{
			// Chua nghi ra :))
		}
		else
		{
			DestroyAllEnemy();
		}
		gameObject.SetActive(status);
	}

	private void DestroyAllEnemy()
	{
		int i;
		for (i = 0; i < enemies.Count; i++)
		{
			Destroy(enemies[i].gameObject);
		}
		enemies.Clear();
	}
}
