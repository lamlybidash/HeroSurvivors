using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropdownManager : MonoBehaviour
{
	[SerializeField] private TMP_Dropdown _dropdown;
	[SerializeField] private TextMeshProUGUI _textLabel;
	[SerializeField] private TextMeshProUGUI _textItem;



	public void OnDropdownSelect()
	{
		int indexSelect = _dropdown.value;
		string nameSelect = _dropdown.options[indexSelect].text;
		Debug.Log($"index select = {indexSelect}");
		Debug.Log($"name select = {nameSelect}");
	}

	public void ClearOption()
	{
		_dropdown.ClearOptions();
	}

	public void AddOptions(List<TMP_Dropdown.OptionData> listOps)
	{
		_dropdown.AddOptions(listOps);
	}

	public void AddOptions(TMP_Dropdown.OptionData Ops)
	{
		List<TMP_Dropdown.OptionData> listOpsTemp = new List<TMP_Dropdown.OptionData>();
		listOpsTemp.Add(Ops);
		_dropdown.AddOptions(listOpsTemp);
		listOpsTemp.Clear();
	}

	public void SetFont(TMP_FontAsset fontx)
	{
		_textLabel.font = fontx;
		_textItem.font = fontx;
	}
}