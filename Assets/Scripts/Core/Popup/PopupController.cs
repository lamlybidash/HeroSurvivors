using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupController : MonoBehaviour
{
	public static PopupController instance { get; private set; }

	[SerializeField] private GameObject _fDameP;
	[SerializeField] private Transform _parent;
	[SerializeField] private List<DameP> _DamePs;

	private void Awake()
	{
		if (instance && instance != this)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;
	}

	public void PopupDame(Transform location, float dame)
	{
		if(FindDamePIA()!=-1)
		{
			_DamePs[FindDamePIA()].SetUpPopup(location, (int)dame);
		}
		else
		{
			GameObject damePnew = Instantiate(_fDameP);
			damePnew.transform.SetParent(_parent);
			_DamePs.Add(damePnew.GetComponent<DameP>());
			_DamePs[_DamePs.Count-1].SetUpPopup(location, (int)dame);
		}
	}	

	private int FindDamePIA()
	{
		int i = -1;
		for(i = 0; i < _DamePs.Count; i++)
		{
			if (_DamePs[i].isActiveAndEnabled == false)
			{
				return i;
			}	
		}
		return -1;
	}	
}
