using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionWeapon : MonoBehaviour, IPointerClickHandler
{
	[SerializeField] private GameController _gc;
	[SerializeField] private WeaponsController _wc;
	[SerializeField] private Image _img;
	[SerializeField] private TextMeshProUGUI _title;
	[SerializeField] private TextMeshProUGUI _describle;
	private Weapons _weapon;
	private int _typeOption = 0; //0 weapon 1 heal 2 money

	private string[] _des = {" sát thương", " tia đạn", " giảm hồi chiêu", " phạm vi hiệu lực", " tốc độ thi triển", " thời gian duy trì" };
	public void SetUpData(Weapons weapon)
	{
		_typeOption = 0;
		_weapon = weapon;
		_title.text = _weapon.data.nameW + " ";
		_img.sprite = _weapon.data.imgSprite;
		if (_weapon._level == 0)
		{
			_title.text += "(New)";
			_describle.text = " Vũ khí mới";
		}
		else
		{
			_title.text += (_weapon._level + 1).ToString();
			_describle.text = " + " + _weapon.data.levelup[_weapon._level - 1].amount.ToString() + _des[_weapon.data.levelup[_weapon._level - 1].attribute - 1];


			//Debug.Log("xxx" + _weapon.data.levelup[_weapon._level - 1].amount.ToString());

			//Sprite spriteTemp = Resources.Load<Sprite>($"Images/Weapon/{_weapon.data.imgUrl}");
			//if (spriteTemp)
			//{
			//	_img.sprite = spriteTemp;
			//}
			//else
			//{
			//	Debug.Log("Khong tim thay anh");
			//}
		}
		gameObject.SetActive(true);
	}

	public void SetUpMoneyAndHeal(int x) // 1 Heal 2 Money
	{
		if (!(x == 1 || x == 2))
		{
			return;
		}

		string titlex = "";
		string describlex = "";
		Sprite imagex = null;
		_typeOption = x;
		if (_typeOption == 1)
        {
			titlex = "Healing";
			describlex = "Hồi phục 20 hp";
			imagex = Resources.Load<Sprite>($"Images/Other/HealthPicOption");
		}
		if (_typeOption == 2)
		{
			titlex = "Get Money";
			describlex = "Nhận 50 vàng";
			imagex = Resources.Load<Sprite>($"Images/Other/CoinOption");
		}

		_title.text = titlex;
		_describle.text = describlex;
		_img.sprite = imagex;
		gameObject.SetActive(true);
	}


	private void InActiveAllOption()
	{
		GameObject parentObj = transform.parent.gameObject;

		foreach(Transform child in parentObj.transform)
		{
			child.gameObject.SetActive(false);
		}	
		parentObj.SetActive(false);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			switch(_typeOption)
			{
				case 0:
					{
						_wc.LevelUpW(_weapon);
						break;
					}
				case 1:
					{
						_gc.CharActive().GetComponent<Health>().TakeDame(-20);
						break;
					}
				case 2:
					{
						_gc.TakeCoinInGame(50);
						break;
					}
			}
			InActiveAllOption();
			_gc.PauseGame(false);
		}
	}
}
