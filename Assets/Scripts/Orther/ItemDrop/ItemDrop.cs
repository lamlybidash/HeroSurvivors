using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemDrop : MonoBehaviour
{
	public enum TypeItem // loại vật phẩm
	{
		HealItem,
		MoneyItem,
		OrtherItem
	}

	public enum Rarity // độ hiếm
	{
		Common,
		Rare,
		Epic,
		Legend
	}

	public TypeItem typeItem;    // 1 hồi phục 2 Tiền 3 Khác
	public Rarity quality;      // phẩm chất ảnh hưởng tới độ hiếm
	public bool inChest = false;
	[SerializeField] protected float valueItem;
	[SerializeField] protected GameController gc;
	protected GameObject player;

	protected virtual void EffectItemDrop()
	{
		// do something
	}

	public void ExcuteEffectItemDrop()
	{
		EffectItemDrop();
		inChest = false;
	}

	public void SetUpPlayer(GameObject playerx)
	{
		player = playerx;
	}

	public void SetLocationDrop(Vector2 localtion)
	{
		transform.position = new Vector3(localtion.x, localtion.y, 0);
	}
	public void SetLocationDrop(Transform localtion)
	{
		transform.position = new Vector3(localtion.position.x, localtion.position.y, localtion.position.z);
	}
}
