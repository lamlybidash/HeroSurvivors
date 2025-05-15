using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

[System.Serializable]
public class LanguageData
{
	public Dictionary<string, Dictionary<string, string>> languages;
}

public class LanguageManager : MonoBehaviour
{
	public static LanguageManager instance;
	//List Controller need update language
	[SerializeField] private SettingManager _SM;
	[SerializeField] private SkillController _SC;
	[SerializeField] private MenuManager _MM;
	[SerializeField] private CharacterController _CC;



	//Var only in class
	private Dictionary<string, Dictionary<string, string>> _currentLang = new Dictionary<string, Dictionary<string, string>>();
	private Dictionary<string, Dictionary<string, string>> _listLangCode;
	private Dictionary<string, TMP_FontAsset> _listFont;
	private TMP_FontAsset _fontCurrent;

	private string _CurrentLangCode;
	
	private void Awake()
	{
		if (instance != null && instance != this)   //instance đã tồn tại và là thằng khác chứ không phải thằng này
		{
			Destroy(gameObject);	// Xóa thằng này
			return;
		}
		//Không phải thì gán = thằng này
		instance = this;
		//Load full data language
		//PlayerPrefs.SetString("Language","en"); //sử dụng để Debug
		_CurrentLangCode = PlayerPrefs.GetString("Language", "en");
		if(_CurrentLangCode == "")
		{
			_CurrentLangCode = "en";
        }
		LoadLangData();
		ReadLangCode();
		ReadAndConvertAllFont();
        SetupFont();
        //Tải ngôn ngữ mặc định
    }

    private void Start()
	{
		//SetLang("vi");
		UpdateLanguage();
	}

	private void LoadLangData()
	{
        _currentLang.Clear();
        string path = Path.Combine(Application.streamingAssetsPath, $"Language/{_CurrentLangCode}.json");
		string jsonFile = File.ReadAllText(path);
		_currentLang = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(jsonFile);
	}


	private void SetupFont()
	{
		_fontCurrent = _listFont[_CurrentLangCode];
    }

	public void SetLang(string lang)
	{
		_CurrentLangCode = lang;
		SetupFont();
		LoadLangData();
	}

	private void ReadLangCode()
	{
		string path = Path.Combine(Application.streamingAssetsPath,"Language/LanguageCode.json");
		string jsonFile = File.ReadAllText(path);
		_listLangCode = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(jsonFile);
	}	

	private void ReadAndConvertAllFont()
	{
		string fontFileName;
		_listFont = new Dictionary<string, TMP_FontAsset>();
		foreach (var item in _listLangCode)
		{
			fontFileName = Path.Combine(Application.streamingAssetsPath, $"Font/{item.Value["font"]}.ttf");
			Font fontTemp = new Font(fontFileName);
			TMP_FontAsset fontTMPTemp =  TMP_FontAsset.CreateFontAsset(fontTemp);
			_listFont[item.Key] = fontTMPTemp;
        }
    }

	public string GetText(string category, string key)
	{
		if(_currentLang.ContainsKey(category))
		{
			if (_currentLang[category].ContainsKey(key))
			{
				return _currentLang[category][key];
			}	
		}
		return key;
	}

	public TMP_FontAsset GetFontAssetCurrent()
	{

        return _fontCurrent;
	}

	public Dictionary<string, Dictionary<string, string>> GetListLangCode()
	{
		return _listLangCode;
	}

	public void UpdateLanguage()
	{
		_CurrentLangCode = PlayerPrefs.GetString("Language", "en");
        SetupFont();
		UpdateFont();
        // Update text mọi nơi
        _SM.UpdateLanguage();
		_MM.UpdateLanguage();
        _SC.UpdateLanguage();
        _CC.UpdateLanguage();
    }

	private void UpdateFont()
	{
		InforWindow.Instance.SetupFont(_fontCurrent);
    }	

	public Dictionary<string, TMP_FontAsset> GetListFont()
	{
		return _listFont;
	}	
}
