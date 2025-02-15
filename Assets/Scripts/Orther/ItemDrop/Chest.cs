using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
	private ItemDrop _item;

	public void SetupItem(ItemDrop itemx)
	{
		_item = itemx;
	}

	public void SetLocationDrop(Transform localtion)
	{
		transform.position = new Vector3(localtion.position.x, localtion.position.y, localtion.position.z);
	}
	public void SetLocationDrop(Vector2 localtion)
	{
		transform.position = new Vector3(localtion.x, localtion.y, 0);
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "WeaponDame")
		{
			_item.SetLocationDrop(transform);
			gameObject.SetActive(false);
			_item.gameObject.SetActive(true);
		}
	}

}
