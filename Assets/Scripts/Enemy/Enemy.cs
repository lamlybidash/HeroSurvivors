using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
	protected string nameEnemy;
	protected float HP;
	protected int level;
	protected float dame;
	protected float areaActtack;
	protected float speedMove;
	protected float speedAttack;
	protected double expDrop;
	public EnemyData data;
	protected Transform player;
	protected Camera cam;
	[SerializeField] protected HealthEnemy healthComponent;
	[SerializeField] protected EnemyMovement movementComponent;
	[SerializeField] protected EnemyAttack attackComponent;


	private void Awake()
	{
		//_health = GetComponent<HealthEnemy>();
	}
	private void Start()
	{

	}


	public void ActiveEnemy(Vector3 pos)
	{
		transform.position = pos;
		gameObject.SetActive(true);
		healthComponent.setupEnemy();
	}

	protected void InitData()
	{
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
	}

	public virtual void BalanceParameter()		// Cân bằng thông số quái
	{

	}

	public void SetupData(Camera camx, Transform playerx)
	{
		cam = camx;
		player = playerx;
		attackComponent.ResetCanGiveDame(); // Sử lý bug khi quái chết CanGiveDame chưa kịp trở lại thành true
	}
}
