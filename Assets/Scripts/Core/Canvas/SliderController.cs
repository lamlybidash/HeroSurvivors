using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SliderController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	private bool _isHold = false;
	private float _lastValue = 0;
	[SerializeField] RectTransform _handle;
	[SerializeField] SettingManager _SM;
	public void OnValueChanged(float valuex)
	{
		float direction = valuex - _lastValue;
		if (Mathf.Abs(direction) > 0.01f)
		{
			if (direction > 0)
			{
				_handle.localEulerAngles = new Vector3(0, 0, 0);
			}
			if (direction < 0)
			{
				_handle.localEulerAngles = new Vector3(0, 180, 0);
			}
			_lastValue = valuex;
		}
		_SM.CheckEnableApplyButton();
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		throw new System.NotImplementedException();
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		throw new System.NotImplementedException();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		_isHold = true;
		_handle.GetComponent<Animator>().SetBool("isHold", true);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		_isHold = false;
		_handle.GetComponent<Animator>().SetBool("isHold", false);
	}
}
