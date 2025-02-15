using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : ItemDrop
{
	private float _timeDuration = 5;
	protected override void EffectItemDrop()
	{
		MagnetEffect magnetEf = new MagnetEffect(5,5);
		player.GetComponent<EffectManager>().ExcuteEffect(magnetEf);
	}
}
