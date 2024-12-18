using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionWeapon : MonoBehaviour, IPointerClickHandler
{
	[SerializeField] private GameController _gc;
	[SerializeField] private WeaponsController _wc;
	[SerializeField] private Image _img;
	[SerializeField] private Text _title;
	[SerializeField] private Text _describle;
	private Weapons _weapon;

	private string[] _des = {" sát thương", " tia đạn", " giảm hồi chiêu", " phạm vi hiệu lực", "tốc độ thi triển", "thời gian duy trì" };


	public void SetUpData(Weapons weapon)
	{
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
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			Debug.Log("da chon :" + _title.text);
			_wc.LevelUpW(_weapon);
			transform.parent.gameObject.SetActive(false);
			_gc.PauseGame(false);
		}
	}
}
