using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : ItemDrop
{
	protected override void EffectItemDrop()
	{
		player.GetComponent<Health>().Healling(valueItem);
	}
}
