using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeGenEnemy
{
	NewTurnEnemy,	// Tạo turn quái mới mỗi phút 
    EnsureEnemy,	// Đảm bảo quái >= Min
    SuicideEnemy,	// Tạo quái cảm tử (Chụm lại 1 chỗ)
}
public class EnemyController : MonoBehaviour
{
	private bool _isPlay;
	//-----------------------------CORE----------------------
	[SerializeField] private Camera _cam;
	[SerializeField] private GameController _gc;
	private Vector3 _camBL;
	private Vector3 _camTR;
    //For gen enemy
    float x = 0, y = 0;

    //-----------------------------ENEMY-----------------------
    [SerializeField] private List<GameObject> _enemyPf;
	[SerializeField] private List<Enemy> _enemies;
	private int _enemyCountMax;
	private int _enemyCountMin;
	private int _enemyCountCurrent;
	private int _eGenCount = 10;

	//-----------------------------BULLET----------------------
	[SerializeField] private GameObject _ListBulletsObject;
	[SerializeField] private GameObject _bulletPf;
	private List<Bullet> _bullets = new List<Bullet>();
	//-----------------------------PLAYER----------------------
	private Transform _player;
	private PlayerExp _playerExp;
	private void Start()
	{
		_enemyCountMax = 300;
		_enemyCountMin = 10;
		_isPlay = false;
		DestroyAllEnemy();
		CamCalc();
	}

  //  private void Update()
  //  {
		//if (Input.GetKeyDown(KeyCode.Space))
		//{
		//	EnemyManager(TypeGenEnemy.NewTurnEnemy);
		//}	
  //  }

    private void RandomPos()
	{
        int t = Random.Range(1, 5); // 1 2 3 4 là 4 hướng xung quanh màn hình
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

    }
    private void CreateEnemy(int indexx, int typeEnemyx)
	{
		RandomPos();
        if (indexx != -1)
		{
			_enemies[indexx].SetupData(_cam, _player, _playerExp.GetLevelChar());
			_enemies[indexx].ActiveEnemy(new Vector3(x, y, 0));
		}
		else
		{
			GameObject _enemyx = Instantiate(_enemyPf[typeEnemyx]);
			_enemyx.transform.SetParent(transform);
			_enemyx.GetComponent<Enemy>().SetupData(_cam, _player, _playerExp.GetLevelChar());
			_enemyx.GetComponent<Enemy>().ActiveEnemy(new Vector3(x, y, 0));
            _enemies.Add(_enemyx.GetComponent<Enemy>());
        }
        _enemyCountCurrent++;
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

	private void EnemyManager(TypeGenEnemy typeGen)
	{
        CamCalc();
		int typeEnemy, i, indexx;
		string idE;

        if (typeGen == TypeGenEnemy.NewTurnEnemy)
		{
			for (i = 0; i < _eGenCount; i++)
			{
				if(_enemyCountCurrent > _enemyCountMax)
				{
					break;
				}
                typeEnemy = Random.Range(0, _enemyPf.Count);    // Random loại quái
                idE = _enemyPf[typeEnemy].GetComponent<Enemy>().idEnemy;
				indexx = FindEnemyInactive(idE);
                CreateEnemy(indexx, typeEnemy);
            }
			_eGenCount += 10;
        }

		if(typeGen == TypeGenEnemy.EnsureEnemy)
		{
			if(_enemyCountCurrent < _enemyCountMin)
			{
				int counGent = _enemyCountMin - _enemyCountCurrent;

                for (i = 0; i < counGent; i++)
				{
                    if (_enemyCountCurrent > _enemyCountMax)
                    {
                        break;
                    }
                    typeEnemy = Random.Range(0, _enemyPf.Count);    // Random loại quái
                    idE = _enemyPf[typeEnemy].GetComponent<Enemy>().idEnemy;
                    indexx = FindEnemyInactive(idE);
                    CreateEnemy(indexx, typeEnemy);
                }
			}	
		}

		if(typeGen == TypeGenEnemy.SuicideEnemy)
		{
			//TODO: Code tiếp
			PopupController.instance.PopupCanvas("Quái cảm tử is coming");
		}	

    }
	private void CamCalc()
	{
		_camBL = _cam.ViewportToWorldPoint(new Vector3(0, 0, _cam.nearClipPlane));
		_camTR = _cam.ViewportToWorldPoint(new Vector3(1, 1, _cam.nearClipPlane));
	}
	private int FindEnemyInactive(string idEx)
	{
		int i;
		for (i = 0; i < _enemies.Count; i++)
		{
			if(!_enemies[i].gameObject.activeInHierarchy)
			{
				if (_enemies[i].idEnemy == idEx)
				{
                    return i;
                }
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
	private void DestroyAllBullet()
	{
		int i;
		for (i = 0; i < _bullets.Count; i++)
		{
			Destroy(_bullets[i].gameObject);
		}
		_bullets.Clear();
	}	
	public void PlayGameStatus(bool status)
	{
		_isPlay	= status;
		if (_isPlay)
		{
            _enemyCountMax = 300;
            _enemyCountMin = 10;
			_eGenCount = 10;
			_enemyCountCurrent = 0;
            EnemyManager(TypeGenEnemy.EnsureEnemy);
        }
        else
		{
			DestroyAllEnemy();
			DestroyAllBullet();
		}
	}
	public void SetPlayer(Transform playerx)
	{
        if (playerx != _player)
        {
            _player = playerx;
            _playerExp = _player.GetComponent<PlayerExp>();
        }
    }	
	public List<GameObject> GetListEnemyActive()
	{
		List<GameObject> listE = new List<GameObject>();
        int i;
        for (i = 0; i < _enemies.Count; i++)
        {
            if (_enemies[i].gameObject.activeInHierarchy)
            {
                listE.Add( _enemies[i].gameObject);
            }
        }
		return listE;
    }	
	public void IncreaseScore(int x)
	{
		_gc.IncreaseScore(x);
		DailyQuestEvent.EnemyKilled();
		_enemyCountCurrent--;
		EnemyManager(TypeGenEnemy.EnsureEnemy);
    }

    public void GenarateMoreEnemy()
	{
		EnemyManager(TypeGenEnemy.NewTurnEnemy);
		_enemyCountMin += 5;
    }

	public void OutCamera(GameObject enemyOutCamx)
	{
        CamCalc();
        RandomPos();
        enemyOutCamx.GetComponent<Enemy>().ActiveEnemy(new Vector3(x, y, 0));
    }
}
