using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupController : MonoBehaviour
{
	public static PopupController instance { get; private set; }

	[SerializeField] private GameObject _fPW;	//Prefab Popup World
	[SerializeField] private GameObject _fPC;   //Prefab Popup Canvas
    [SerializeField] private Transform _parentW;
    [SerializeField] private RectTransform _parentC;
	[SerializeField] private List<PopupWorld> _listPopupWorld;
    [SerializeField] private List<PopupCanvas> _listPopupCanvas;
    private void Awake()
	{
		if (instance && instance != this)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;
	}

	public void PopupWorld(string textx, Vector3? locationx = null, Color? colorx = null)
	{
        if (FindPIA("World") !=-1)
		{
            _listPopupWorld[FindPIA("World")].PopupF(textx, locationx, colorx);
        }
        else
		{
            //Add vào list để quản lý
			GameObject PWnew = Instantiate(_fPW);
            PWnew.transform.SetParent(_parentW);
            _listPopupWorld.Add(PWnew.GetComponent<PopupWorld>());
            _listPopupWorld[_listPopupWorld.Count - 1].PopupF(textx, locationx, colorx);
        }
    }
    public void PopupCanvas(string textx, Vector2? locationx = null, Color? colorx = null)
    {
        if (FindPIA("Canvas") != -1)
        {
            _listPopupCanvas[FindPIA("Canvas")].PopupF(textx, locationx, colorx);
        }
        else
        {
            //Add vào list để quản lý
            GameObject PCnew = Instantiate(_fPC);
            PCnew.GetComponent<RectTransform>().SetParent(_parentC, false);
            _listPopupCanvas.Add(PCnew.GetComponent<PopupCanvas>());
            _listPopupCanvas[_listPopupCanvas.Count - 1].PopupF(textx, locationx, colorx);
        }
    }

    private int FindPIA(string typeList) // Find Popup is active. typeList: World , Canvas
	{
		int i = 0;
		if(typeList == "Canvas")
		{
            for (i = 0; i < _listPopupCanvas.Count; i++)
            {
                if (_listPopupCanvas[i].isActiveAndEnabled == false)
                {
                    return i;
                }
            }
        }

		if(typeList == "World")
		{
            for (i = 0; i < _listPopupWorld.Count; i++)
            {
                if (_listPopupWorld[i].isActiveAndEnabled == false)
                {
                    return i;
                }
            }
        }
        return -1;
	}	
}

//TODO Cập nhật font + lang cho popup