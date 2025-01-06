using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/Create Enemy Data", fileName = "EnemyData")]
public class EnemyData : ScriptableObject
{
	public string nameEnemy;
	public int level;		// pham chat quai
	public float HP;
	public float dame;
	public float areaActtack;
	public float speedMove;
	public float speedAttack;
	public double expDrop;
}
