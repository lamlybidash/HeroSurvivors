using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionWeapon : MonoBehaviour, IPointerClickHandler
{

	[SerializeField] private GameController _gc;
	[SerializeField] private Image _img;
	[SerializeField] private Text _title;
	[SerializeField] private Text _describle;

	private string[] _des = {"Vũ khí mới", " sát thương", " tia đạn", " giảm hồi chiêu", " phạm vi hiệu lực", "tốc độ thi triển" };


	public void SetUpData(testWeapon dataW)
	{
		_title.text = dataW.title;
		_describle.text = dataW.describle;
		Sprite spriteTemp = Resources.Load<Sprite>($"Images/Weapon/{dataW.image}");
		if (spriteTemp)
		{
			_img.sprite = spriteTemp;
		}
		else
		{
			Debug.Log("Khong tim thay anh");
		}
	}	

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			Debug.Log("da chon :" + _title.text);
			transform.parent.gameObject.SetActive(false);
			_gc.PauseGame(false);
		}
		
	}
}
