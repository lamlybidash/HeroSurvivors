using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
	protected float damage;
	protected int projectile;
	protected float countdown;
	protected float area;
	protected float speed;
	protected Transform target;

	protected virtual void GiveDame(float dame, GameObject target)
	{
		//target.gameObject.GetComponent<HealthEnemy>().TakeDame(damage);
	}	
}
