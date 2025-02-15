using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
	[SerializeField] private List<GameObject> _listChar;
	[SerializeField] private List<Button> _listButtonSelectChar;
	[SerializeField] private CameraController _camC;
	private GameObject CharacterActive;



	public void ChooseCharacter(int i)
	{
		CharacterActive = _listChar[i];
	}

	public GameObject CharActive()
	{
		return CharacterActive;
	}
	public void SetUpSelectCharPanel()
	{
		int i = 0;
		for (i = 0; i < _listChar.Count; i++)
		{
			int k = i;
			_listButtonSelectChar[k].onClick.AddListener(() => SelectCharX(_listChar[k]));
			_listButtonSelectChar[k].gameObject.SetActive(true);
		}
	}
	public void SelectCharX(GameObject x)
	{
		CharacterActive = x;
	}
	public void SetFollowerForCamera()
	{
		_camC.SetFollower(CharacterActive.transform);
	}

	public void ResetAllCharacter()
	{
		foreach(GameObject x in _listChar)
		{
			x.GetComponent<Character>().ResetData();
		}	
	}	
}
