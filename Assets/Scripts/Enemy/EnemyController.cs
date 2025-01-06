using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	private bool _isPlay;
	//-----------------------------CORE----------------------
	[SerializeField] private Camera _cam;
	private Vector3 _camBL;
	private Vector3 _camTR;
	//-----------------------------ENEMY-----------------------
	[SerializeField] private GameObject _enemyPf;
	[SerializeField] private List<Enemy> _enemies;
	private int _enemyCountMax;
	//-----------------------------BULLET----------------------
	[SerializeField] private GameObject _ListBulletsObject;
	[SerializeField] private GameObject _bulletPf;
	private List<Bullet> _bullets = new List<Bullet>();
	//-----------------------------PLAYER----------------------
	private Transform _player;
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
			_enemies[indexx].SetupData(_cam, _player);
			_enemies[indexx].ActiveEnemy(new Vector3(x, y, 0));
		}
		else
		{
			// tam thoi co 1 loai quai
			GameObject _enemyx = Instantiate(_enemyPf);
			_enemyx.transform.SetParent(transform);
			_enemyx.GetComponent<Enemy>().SetupData(_cam, _player);
			_enemyx.GetComponent<Enemy>().ActiveEnemy(new Vector3(x, y, 0));
			_enemies.Add(_enemyx.GetComponent<Enemy>());
		}	

	}

	public void CreateBullet(Transform startPoint, Transform target, float dame)
	{
		int indexx;
		indexx = FindBulletInactive();

		if (indexx != -1)
		{
			_bullets[indexx].ShootBullet(startPoint, target, dame);
		}
		else
		{
			GameObject bulletx = Instantiate(_bulletPf);
			bulletx.transform.SetParent(_ListBulletsObject.transform);
			bulletx.GetComponent<Bullet>().ShootBullet(startPoint, target, dame);
			_bullets.Add(bulletx.GetComponent<Bullet>());
		}
	}	

	private void EnemyManager()
	{
		int indexx;
		indexx = FindEnemyInactive();

		if(indexx == -1 && !(_enemies.Count < _enemyCountMax))
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
		for (i = 0; i < _enemies.Count; i++)
		{
			if(!_enemies[i].gameObject.activeInHierarchy)
			{
				return i;
			}	
		}
		return -1;
	}
	private int FindBulletInactive()
	{
		int i;
		for (i = 0; i < _bullets.Count; i++)
		{
			if (!_bullets[i].gameObject.activeInHierarchy)
			{
				return i;
			}
		}
		return -1;
	}
	private void DestroyAllEnemy()
	{
		int i;
		for (i = 0; i < _enemies.Count; i++)
		{
			Destroy(_enemies[i].gameObject);
		}
		_enemies.Clear();
	}

	public void PlayGameStatus(bool status)
	{
		_isPlay	= status;
		if (_isPlay)
		{

		}
		else
		{
			DestroyAllEnemy();
		}
		gameObject.SetActive(status);
	}

	public void SetPlayer(Transform playerx)
	{
		_player = playerx;
	}	
}
