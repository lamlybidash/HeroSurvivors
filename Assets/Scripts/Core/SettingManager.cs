using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;

public class SettingManager : MonoBehaviour
{
	[SerializeField] private GameObject _music;
	[SerializeField] private GameObject _sfx;
	[SerializeField] private GameObject _language;
	[SerializeField] private GameObject _backButton;
	[SerializeField] private GameObject _applyButton;
	[SerializeField] private Slider _sliderMusic;
	[SerializeField] private Slider _sliderSFX;
	[SerializeField] private TMP_Dropdown _dropdownLang;
	[SerializeField] private DropdownManager _dropdownLangM;
	private TextMeshProUGUI _textMusic;
	private TextMeshProUGUI _textSfx;
	private TextMeshProUGUI _textLang;
	private TextMeshProUGUI _textBackButton;
	private TextMeshProUGUI _textApplyButton;
	private TMP_FontAsset _font;
	private Dictionary<string, Dictionary<string, string>> _listLanguageCode;
	private Dictionary <string, string> _reverseLanguageCode = new Dictionary<string, string>();
    private Dictionary<string, TMP_FontAsset> _listFont;

    private float _musicValue;
	private float _musicValueTemp;
	private float _SFXValue;
	private float _SFXValueTemp;
	private string _languageCodeCurrent;
	private string _languageCodeCurrentTemp;

	private void Awake()
	{
		SetupComponent();
		SetupDataOld();
	}
	private void Start()
	{
		SetupDropdownLang();
		SetupDataStart();
		gameObject.SetActive(false);
	}
	private void SetupComponent()
	{
		_textMusic = _music.transform.Find("Text").GetComponent<TextMeshProUGUI>();
		_textSfx = _sfx.transform.Find("Text").GetComponent<TextMeshProUGUI>();
		_textLang = _language.transform.Find("Text").GetComponent<TextMeshProUGUI>();
		_textBackButton = _backButton.transform.Find("Title").GetComponent<TextMeshProUGUI>();
		_textApplyButton = _applyButton.transform.Find("Title").GetComponent<TextMeshProUGUI>();
	}

	private void SetupDropdownLang()
	{
		if(_listLanguageCode != null)
		{
			_listLanguageCode.Clear();
		}
		_listLanguageCode = LanguageManager.instance.GetListLangCode();
		_dropdownLang.ClearOptions();
		List<TMP_Dropdown.OptionData> optionDataList = new List<TMP_Dropdown.OptionData>();
		foreach(var item in _listLanguageCode)
		{
			TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData(item.Value["name"], null);
			_reverseLanguageCode[item.Value["name"]] = item.Key;
			optionDataList.Add(optionData);
		}
		_dropdownLang.AddOptions(optionDataList);
	}

	public void UpdateLanguage()
	{
		UpdateFont();
		_textMusic.text = LanguageManager.instance.GetText("menu", "music");
		_textSfx.text = LanguageManager.instance.GetText("menu", "sfx");
		_textLang.text = LanguageManager.instance.GetText("menu", "language");
		_textBackButton.text = LanguageManager.instance.GetText("menu", "back");
		_textApplyButton.text = LanguageManager.instance.GetText("menu", "apply");
	}
	private void UpdateFont()
	{
		_font = LanguageManager.instance.GetFontAssetCurrent();
		_textMusic.font = _font;
		_textSfx.font = _font;
		_textLang.font = _font;
		_textBackButton.font = _font;
		_textApplyButton.font = _font;
		_dropdownLangM.SetFont(_font);
	}
	private void SetupDataOld()
	{
		_musicValue = PlayerPrefs.GetFloat("MusicValue", 1);
		_SFXValue = PlayerPrefs.GetFloat("SFXValue", 1);
		_languageCodeCurrent = PlayerPrefs.GetString("Language", "en");
		_languageCodeCurrentTemp = _languageCodeCurrent;
		_musicValueTemp = _musicValue;
		_SFXValueTemp = _SFXValue;
	}
	public void BackButtonOnClick()
	{
		SetupDataOld();

		//Thực thi thay đổi
		LanguageManager.instance.SetLang(_languageCodeCurrent);
		LanguageManager.instance.UpdateLanguage();
		SoundManager.instance.SetVolumeMusic(_musicValue);
		SoundManager.instance.SetVolumeSFX(_SFXValue);

		gameObject.SetActive(false);
	}
	public void ApplyButtonOnClick()
	{
		PlayerPrefs.SetFloat("MusicValue", _musicValueTemp);
		PlayerPrefs.SetFloat("SFXValue", _SFXValueTemp);
		PlayerPrefs.SetString("Language", _languageCodeCurrentTemp);
		_musicValue = _musicValueTemp;
		_SFXValue = _SFXValueTemp;
		_languageCodeCurrent = _languageCodeCurrentTemp;
		//Thực thi thay đổi
		LanguageManager.instance.UpdateLanguage();
		SoundManager.instance.SetVolumeMusic( _musicValueTemp );
		SoundManager.instance.SetVolumeSFX( _SFXValueTemp );

		CheckEnableApplyButton();
	}
	// Call often
	public void CheckEnableApplyButton()
	{
		GetValueSlider();
		if ((_musicValue == _musicValueTemp) && (_SFXValue == _SFXValueTemp) && (_languageCodeCurrent == _languageCodeCurrentTemp))
		{
			_applyButton.GetComponent<Button>().interactable = false;
		}
		else
		{
			_applyButton.GetComponent<Button>().interactable = true;
		}
	}
	public void GetValueSlider()
	{
		_musicValueTemp = _sliderMusic.value;
		_SFXValueTemp = _sliderSFX.value;
		SoundManager.instance.SetVolumeMusic(_musicValueTemp);
		SoundManager.instance.SetVolumeSFX(_SFXValueTemp);
	}	
	public void LanguageDropDownChanged()
	{
		int _indexLangTemp = _dropdownLang.value;
		string nameSelect = _dropdownLang.options[_indexLangTemp].text;
		_languageCodeCurrentTemp = _reverseLanguageCode[nameSelect];
		LanguageManager.instance.SetLang(_languageCodeCurrentTemp);
		UpdateLanguage();
		CheckEnableApplyButton();
	}
	public void SetupDataStart()
	{
		_sliderMusic.value = _musicValue;
		_sliderSFX.value = _SFXValue;
		int i;
		i = 0;
		foreach(TMP_Dropdown.OptionData op in _dropdownLang.options)
		{
			if (op.text == _listLanguageCode[_languageCodeCurrent]["name"])
			{
				_dropdownLang.value = i;
				break;
			}
			i++;
		}
		_listFont = LanguageManager.instance.GetListFont();
    }
	public void TestTriggerDown()
	{
		StartCoroutine(SetupFontLang());
	}	
    private IEnumerator SetupFontLang()
	{
		//Đợi List Item được TMP_Dropdown tạo ra
		GameObject DropdownList = null;
        while (DropdownList == null)
        {
			var varTemp = _dropdownLang.transform.Find("Dropdown List");
            if (varTemp == null)
            {
                yield return null;
            }
			else
			{
                DropdownList = varTemp.gameObject;
            }
        }
        GameObject Content = DropdownList.transform.Find("Viewport/Content").gameObject;
		foreach (Transform item in Content.transform)
		{
			TextMeshProUGUI textItem = item.transform.Find("Item Label").GetComponent<TextMeshProUGUI>();
			// cài đặt font
			if(_reverseLanguageCode.ContainsKey(textItem.text))
			{
                if (_listFont.ContainsKey(_reverseLanguageCode[textItem.text]))
                {
					textItem.enableAutoSizing = true;
                    textItem.font = _listFont[_reverseLanguageCode[textItem.text]];
                }
            }	
        }
	}	
}