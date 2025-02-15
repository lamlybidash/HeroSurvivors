using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyItem : ItemDrop
{

	protected override void EffectItemDrop()
	{
		gc.TakeCoinInGame((int)valueItem);
	}
}
