using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

	public string idEnemy;
	protected string nameEnemy;
	protected float HP;
	protected int level;
	protected float dame;
	protected float areaActtack;
	protected float speedMove;
	protected float speedAttack;
	protected float expDrop;
	public EnemyData data;
	protected Transform player;
	protected EnemyController ec;
	protected Camera cam;
	[SerializeField] protected HealthEnemy healthComponent;
	[SerializeField] protected EnemyMovement movementComponent;
	[SerializeField] protected EnemyAttack attackComponent;

	private void Awake()
	{
		//_health = GetComponent<HealthEnemy>();
	}

	public void ActiveEnemy(Vector3 pos)
	{
		transform.position = pos;
		gameObject.SetActive(true);
	}

	protected void InitData()
	{
        idEnemy = data.idEnemy;
        nameEnemy = data.nameEnemy;
		HP = data.HP;
		level = data.level;
		dame = data.dame;
		areaActtack = data.areaActtack;
		speedMove = data.speedMove;
		speedAttack = data.speedAttack;
		expDrop = data.expDrop;
		healthComponent.SetUpData(HP, expDrop);
		movementComponent.SetUpData(player, cam, speedMove,areaActtack);
		attackComponent.SetUpData(dame, speedAttack);
		ec = transform.parent.GetComponent<EnemyController>();
	}

	private void BalanceParameter(int levelCharacter)		// Cân bằng thông số quái
	{
		HP = data.HP * (1 + (levelCharacter - 1) * 0.1f);
		expDrop = data.expDrop * (1 + (levelCharacter - 1) * 0.25f);
        healthComponent.SetUpData(HP, expDrop);
        healthComponent.setupEnemy();
    }

    public void SetupData(Camera camx, Transform playerx, int levelCharacter)
	{
		cam = camx;
		player = playerx;
		attackComponent.ResetCanGiveDame(); // Sử lý bug khi quái chết CanGiveDame chưa kịp trở lại thành true
		BalanceParameter(levelCharacter);
    }

	public void IncreaseScore(int x)
	{
		ec.IncreaseScore(x);
	}	
}