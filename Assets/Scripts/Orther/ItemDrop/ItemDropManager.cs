using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropManager : ItemDrop
{
	//Topleft (-39, 27.5)
	//BotRight (40, -31.5)
	public static ItemDropManager instance;

	[SerializeField] private List<GameObject> _listItemPf;
	[SerializeField] private GameObject _chestPf;
	private List<ItemDrop> _listItemDropInMap = new List<ItemDrop>();
	private List<Chest> _listChestInMap = new List<Chest>();

	private Vector2 Topleft = new Vector2(-39f, 27.5f);
	private Vector2 BotRight = new Vector2(40f, -31.5f);
	private Vector2 LocationDrop;

	private bool _isDropping = false;
	private float _timeStepDrop; // Thời gian nghỉ mỗi lần drop

	//For random
	private TypeItem _typeItemR;
	private Rarity _qualityItemR;
	private int _indexForRandom;

	private void Awake()
	{
		if (instance && instance != this)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;
	}
	private IEnumerator DropItemLoop()
	{
		float x = 0,y = 0;
		while (_isDropping)
		{
			//Random tọa độ
			x = Random.Range(Topleft.x, BotRight.x);
			y = Random.Range(BotRight.y, Topleft.y);
			LocationDrop = new Vector2 (x,y);

			//Random thời điểm drop
			_timeStepDrop = Random.Range(3, 5); // drop mỗi 10s ~ 20s

			DropChest(LocationDrop);
			yield return new WaitForSeconds(_timeStepDrop);
		}
	}
	public void DropChest(Transform localtionx)
	{
		int indexitem, indexchest;
		LocationDrop = new Vector2(localtionx.position.x, localtionx.position.y);
		//Random vật phẩm drop
		#region
		_indexForRandom = Random.Range(0, 100);
		if (0 <= _indexForRandom && _indexForRandom < 45)
		{
			_typeItemR = TypeItem.HealItem;
		}
		if (45 <= _indexForRandom && _indexForRandom < 90)
		{
			_typeItemR = TypeItem.MoneyItem;
		}
		if (90 <= _indexForRandom && _indexForRandom < 100)
		{
			_typeItemR = TypeItem.OrtherItem;
		}

		_indexForRandom = Random.Range(0, 100);
		if (0 <= _indexForRandom && _indexForRandom < 60)
		{
			_qualityItemR = Rarity.Common;
		}
		if (60 <= _indexForRandom && _indexForRandom < 90)
		{
			_qualityItemR = Rarity.Rare;
		}
		if (90 <= _indexForRandom && _indexForRandom < 99)
		{
			_qualityItemR = Rarity.Epic;
		}
		if (99 <= _indexForRandom && _indexForRandom < 100)
		{
			_qualityItemR = Rarity.Legend;
		}
		#endregion
		// Sao chép Item
		#region
		indexitem = FindItem(_qualityItemR, _typeItemR);
		indexchest = FindChest();
		if (indexitem == -1)
		{
			foreach (GameObject item in _listItemPf)
			{
				if (item.GetComponent<ItemDrop>().quality == _qualityItemR && item.GetComponent<ItemDrop>().typeItem == _typeItemR)
				{
					GameObject itemTemp = Instantiate(item);
					itemTemp.transform.SetParent(transform);
					_listItemDropInMap.Add(itemTemp.GetComponent<ItemDrop>());
					indexitem = _listItemDropInMap.Count - 1;
					break;
				}
			}
		}
		if (indexitem != -1)
		{
			_listItemDropInMap[indexitem].SetLocationDrop(LocationDrop);
		}
		#endregion
		//Sao chép rương
		#region
		if (indexchest == -1)
		{
			GameObject chestTemp = Instantiate(_chestPf);
			chestTemp.transform.SetParent(transform);
			_listChestInMap.Add(chestTemp.GetComponent<Chest>());
			indexchest = _listChestInMap.Count - 1;
		}
		if (indexchest != -1 && indexitem != -1)
		{
			_listChestInMap[indexchest].SetupItem(_listItemDropInMap[indexitem]);
			_listChestInMap[indexchest].SetLocationDrop(LocationDrop);
			_listChestInMap[indexchest].gameObject.SetActive(true);
			_listItemDropInMap[indexitem].inChest = true;
		}
		#endregion
	}
	public void DropChest(Vector2 localtionx)
	{
		int indexitem, indexchest;
		LocationDrop = new Vector2(localtionx.x, localtionx.y);
		//Random vật phẩm drop
		#region
		_indexForRandom = Random.Range(0, 100);
		if (0 <= _indexForRandom && _indexForRandom < 45)
		{
			_typeItemR = TypeItem.HealItem;
		}
		if (45 <= _indexForRandom && _indexForRandom < 90)
		{
			_typeItemR = TypeItem.MoneyItem;
		}
		if (90 <= _indexForRandom && _indexForRandom < 100)
		{
			_typeItemR = TypeItem.OrtherItem;
		}
		_indexForRandom = Random.Range(0, 100);
		if (0 <= _indexForRandom && _indexForRandom < 60)
		{
			_qualityItemR = Rarity.Common;
		}
		if (60 <= _indexForRandom && _indexForRandom < 90)
		{
			_qualityItemR = Rarity.Rare;
		}
		if (90 <= _indexForRandom && _indexForRandom < 99)
		{
			_qualityItemR = Rarity.Epic;
		}
		if (99 <= _indexForRandom && _indexForRandom < 100)
		{
			_qualityItemR = Rarity.Legend;
		}
		#endregion
		// Sao chép Item
		#region
		indexitem = FindItem(_qualityItemR, _typeItemR);
		indexchest = FindChest();
		if (indexitem == -1)
		{
			foreach (GameObject item in _listItemPf)
			{
				if (item.GetComponent<ItemDrop>().quality == _qualityItemR && item.GetComponent<ItemDrop>().typeItem == _typeItemR)
				{
					GameObject itemTemp = Instantiate(item);
					itemTemp.transform.SetParent(transform);
					_listItemDropInMap.Add(itemTemp.GetComponent<ItemDrop>());
					indexitem = _listItemDropInMap.Count - 1;
					break;
				}
			}
		}
		if(indexitem != -1)
		{
			_listItemDropInMap[indexitem].SetLocationDrop(LocationDrop);
		}	
		#endregion
		//Sao chép rương
		#region
		if (indexchest == -1)
		{
			GameObject chestTemp = Instantiate(_chestPf);
			chestTemp.transform.SetParent(transform);
			_listChestInMap.Add(chestTemp.GetComponent<Chest>());
			indexchest = _listChestInMap.Count - 1;
		}
		if (indexchest != -1 && indexitem != -1)
		{
			_listChestInMap[indexchest].SetupItem(_listItemDropInMap[indexitem]);
			_listChestInMap[indexchest].SetLocationDrop(LocationDrop);
			_listChestInMap[indexchest].gameObject.SetActive(true);
			_listItemDropInMap[indexitem].inChest = true;
		}
		#endregion
	}
	public void DropItem(Transform localtionx, int qualityx, int typeitemx)
	{
		int indexitem;
		LocationDrop = new Vector2(localtionx.position.x, localtionx.position.y);
		_qualityItemR = (Rarity)qualityx;
		_typeItemR = (TypeItem)typeitemx;
		// Sao chép Item
		#region
		indexitem = FindItem(_qualityItemR, _typeItemR);
		if (indexitem == -1)
		{
			foreach (GameObject item in _listItemPf)
			{
				if (item.GetComponent<ItemDrop>().quality == _qualityItemR && item.GetComponent<ItemDrop>().typeItem == _typeItemR)
				{
					GameObject itemTemp = Instantiate(item);
					itemTemp.transform.SetParent(transform);
					_listItemDropInMap.Add(itemTemp.GetComponent<ItemDrop>());
					indexitem = _listItemDropInMap.Count - 1;
					break;
				}
			}
		}
		if (indexitem != -1)
		{
			_listItemDropInMap[indexitem].SetLocationDrop(LocationDrop);
			_listItemDropInMap[indexitem].gameObject.SetActive(true);
		}
		#endregion
	}
	public void DropItem(Vector2 localtionx, int qualityx , int typeitemx)
	{
		int indexitem;
		LocationDrop = new Vector2(localtionx.x, localtionx.y);
		_qualityItemR = (Rarity)qualityx;
		_typeItemR = (TypeItem)typeitemx;
		// Sao chép Item
		#region
		indexitem = FindItem(_qualityItemR, _typeItemR);
		if (indexitem == -1)
		{
			foreach (GameObject item in _listItemPf)
			{
				if (item.GetComponent<ItemDrop>().quality == _qualityItemR && item.GetComponent<ItemDrop>().typeItem == _typeItemR)
				{
					GameObject itemTemp = Instantiate(item);
					itemTemp.transform.SetParent(transform);
					_listItemDropInMap.Add(itemTemp.GetComponent<ItemDrop>());
					indexitem = _listItemDropInMap.Count - 1;
					break;
				}
			}
		}
		if (indexitem != -1)
		{
			_listItemDropInMap[indexitem].SetLocationDrop(LocationDrop);
			_listItemDropInMap[indexitem].gameObject.SetActive(true);
		}
		#endregion
	}
	public void DropItem(Transform localtionx)
	{
		int indexitem;
		LocationDrop = new Vector2(localtionx.position.x, localtionx.position.y);
		//Random vật phẩm drop
		#region
		_indexForRandom = Random.Range(0, 100);
		if (0 <= _indexForRandom && _indexForRandom < 45)
		{
			_typeItemR = TypeItem.HealItem;
		}
		if (45 <= _indexForRandom && _indexForRandom < 90)
		{
			_typeItemR = TypeItem.MoneyItem;
		}
		if (90 <= _indexForRandom && _indexForRandom < 100)
		{
			_typeItemR = TypeItem.OrtherItem;
		}

		_indexForRandom = Random.Range(0, 100);
		if (0 <= _indexForRandom && _indexForRandom < 60)
		{
			_qualityItemR = Rarity.Common;
		}
		if (60 <= _indexForRandom && _indexForRandom < 90)
		{
			_qualityItemR = Rarity.Rare;
		}
		if (90 <= _indexForRandom && _indexForRandom < 99)
		{
			_qualityItemR = Rarity.Epic;
		}
		if (99 <= _indexForRandom && _indexForRandom < 100)
		{
			_qualityItemR = Rarity.Legend;
		}
		#endregion
		// Sao chép Item
		#region
		indexitem = FindItem(_qualityItemR, _typeItemR);
		if (indexitem == -1)
		{
			foreach (GameObject item in _listItemPf)
			{
				if (item.GetComponent<ItemDrop>().quality == _qualityItemR && item.GetComponent<ItemDrop>().typeItem == _typeItemR)
				{
					GameObject itemTemp = Instantiate(item);
					itemTemp.transform.SetParent(transform);
					_listItemDropInMap.Add(itemTemp.GetComponent<ItemDrop>());
					indexitem = _listItemDropInMap.Count - 1;
					break;
				}
			}
		}
		if (indexitem != -1)
		{
			_listItemDropInMap[indexitem].SetLocationDrop(LocationDrop);
			_listItemDropInMap[indexitem].gameObject.SetActive(true);
		}	
		#endregion
	}
	public void DropItem(Vector2 localtionx)
	{
		int indexitem;
		LocationDrop = new Vector2(localtionx.x, localtionx.y);
		//Random vật phẩm drop
		#region
		_indexForRandom = Random.Range(0, 100);
		if (0 <= _indexForRandom && _indexForRandom < 45)
		{
			_typeItemR = TypeItem.HealItem;
		}
		if (45 <= _indexForRandom && _indexForRandom < 90)
		{
			_typeItemR = TypeItem.MoneyItem;
		}
		if (90 <= _indexForRandom && _indexForRandom < 100)
		{
			_typeItemR = TypeItem.OrtherItem;
		}

		_indexForRandom = Random.Range(0, 100);
		if (0 <= _indexForRandom && _indexForRandom < 60)
		{
			_qualityItemR = Rarity.Common;
		}
		if (60 <= _indexForRandom && _indexForRandom < 90)
		{
			_qualityItemR = Rarity.Rare;
		}
		if (90 <= _indexForRandom && _indexForRandom < 99)
		{
			_qualityItemR = Rarity.Epic;
		}
		if (99 <= _indexForRandom && _indexForRandom < 100)
		{
			_qualityItemR = Rarity.Legend;
		}
		#endregion
		// Sao chép Item
		#region
		indexitem = FindItem(_qualityItemR, _typeItemR);
		if (indexitem == -1)
		{
			foreach (GameObject item in _listItemPf)
			{
				if (item.GetComponent<ItemDrop>().quality == _qualityItemR && item.GetComponent<ItemDrop>().typeItem == _typeItemR)
				{
					GameObject itemTemp = Instantiate(item);
					itemTemp.transform.SetParent(transform);
					_listItemDropInMap.Add(itemTemp.GetComponent<ItemDrop>());
					indexitem = _listItemDropInMap.Count - 1;
					break;
				}
			}
		}
		if (indexitem != -1)
		{
			_listItemDropInMap[indexitem].SetLocationDrop(LocationDrop);
			_listItemDropInMap[indexitem].gameObject.SetActive(true);
		}
		#endregion
	}
	public void StartCoroutineDropF()
	{
		_isDropping = true;
		StartCoroutine(DropItemLoop());
	}
	public void StopCoroutieDropF()
	{
		_isDropping = false;
		StopAllCoroutines();
	}
	public void ClearAllItemInMap()
	{
		foreach(ItemDrop item in _listItemDropInMap)
		{
			if(item != null)
			{
				Destroy(item.gameObject);
			}
		}
		foreach(Chest chest in _listChestInMap)
		{
			if (chest != null)
			{
				Destroy(chest.gameObject);
			}
		}
		_listItemDropInMap.Clear();
		_listChestInMap.Clear();
	}
	private int FindItem(Rarity qualityx, TypeItem typeItemx)
	{
		int i;
		int x = -1;
		for (i = 0; i < _listItemDropInMap.Count; i++)
		{
			if (_listItemDropInMap[i].gameObject.activeInHierarchy == false)
			{
				if(qualityx == _listItemDropInMap[i].quality && typeItemx == _listItemDropInMap[i].typeItem && _listItemDropInMap[i].inChest == false)
				{
					x = i;
					return x;
				}
			}
		}
		return x;
	}
	private int FindChest()
	{
		int i = -1;
		int x = -1;
		for (i = 0; i < _listChestInMap.Count; i++)
		{
			if (_listChestInMap[i].gameObject.activeInHierarchy == false)
			{
				x = i;
				return x;
			}
		}
		return x;
	}
}
