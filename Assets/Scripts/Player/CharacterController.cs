using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
	[SerializeField] private List<GameObject> _listChar;
	[SerializeField] private List<GameObject> _listObButtonChar;
	[SerializeField] private List<string> _listCharacterUnlocked;
	[SerializeField] private CameraController _camC;
	[SerializeField] private GameController _GC;
	[SerializeField] private MenuManager _MM;
	private GameObject CharacterActive;
	private GameObject CharacterButton;
	private SaveData _saveData;


	//Infor Character Panel
	[SerializeField] private GameObject _inforPanel;
	[SerializeField] private TextMeshProUGUI _textName;
	[SerializeField] private TextMeshProUGUI _textHP;
	[SerializeField] private TextMeshProUGUI _textDef;
	[SerializeField] private TextMeshProUGUI _textSpeed;
	[SerializeField] private TextMeshProUGUI _textWeapon;
	[SerializeField] private TextMeshProUGUI _textSpecialAbility;
	[SerializeField] private Image _imgSkillE;
	[SerializeField] private Image _imgSkillQ;
    private TMP_FontAsset _font;
    private Color colorSelect;
    private bool _isTarget;
    private bool _canPlay;
    private int _indexObButtonChar;
	//

    private void Start()
    {
        ColorUtility.TryParseHtmlString("#8080FF", out colorSelect);
        _isTarget = false;
		_canPlay = false;
        StartCoroutine(WaitToLoadData());

    }
	private IEnumerator WaitToLoadData()
	{
        yield return new WaitForSecondsRealtime(1);
        foreach (GameObject charI in _listChar)
		{
            charI.gameObject.SetActive(false);
        }
        _saveData = SaveDataManager.instance.GetSave();
    }
    private void SetupInfor(GameObject x) // Character x
	{
		Character xx = x.GetComponent<Character>();
        string special_ability_text1, special_ability_text2, special_ability_text3;
        special_ability_text1 = LanguageManager.instance.GetText("character", "special_ability");
        special_ability_text2 = LanguageManager.instance.GetText("weapon", xx.data.SA);
        if (x.GetComponent<Character>().data.SAType == "Bonus")
        {
            special_ability_text3 = $" + {xx.data.SAAmount}";
        }
        else  // Multi
        {
            special_ability_text3 = $" x {xx.data.SAAmount}";
        }
		_textSpecialAbility.text = special_ability_text1 + ":" + special_ability_text2 + special_ability_text3;
        _textName.text = LanguageManager.instance.GetText("character", "name") + ":" + xx.data.name;
        _textHP.text = LanguageManager.instance.GetText("character", "hp") + ":" + xx.data.HP;
        _textDef.text = LanguageManager.instance.GetText("character", "def") + ":" + xx.data.def;
        _textSpeed.text = LanguageManager.instance.GetText("character", "speed") + ":" + xx.data.speedMove;
        _textWeapon.text = LanguageManager.instance.GetText("character", "weapon") + ":" + LanguageManager.instance.GetText("weapon", xx.data.startingWeapon);
		_imgSkillE.sprite = xx.GetSkill('E').GetImageSkill();
		_imgSkillQ.sprite = xx.GetSkill('Q').GetImageSkill();

    }
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
		string nameChar = "";
		bool isUnlocked;
        _listCharacterUnlocked = _saveData.list_character;
        int i = 0;
		for (i = 0; i < _listChar.Count; i++)
		{
			int k = i;
			isUnlocked = false;
            EventTrigger.Entry entryEnter = new EventTrigger.Entry
			{
				eventID = EventTriggerType.PointerEnter,
				callback = new EventTrigger.TriggerEvent()
			};

			EventTrigger.Entry entryExit = new EventTrigger.Entry
			{
				eventID = EventTriggerType.PointerExit,
				callback = new EventTrigger.TriggerEvent()
			};

			//Click
			EventTrigger.Entry entryDown = new EventTrigger.Entry
			{
				eventID = EventTriggerType.PointerDown,
				callback = new EventTrigger.TriggerEvent()
			};

			entryDown.callback.AddListener((eventData) => SelectCharX(_listChar[k], _listObButtonChar[k], k));
			entryEnter.callback.AddListener((eventData) =>ShowInfor(_listChar[k]));
            entryExit.callback.AddListener((eventData) =>HideInfor(_listChar[k]));

			if(_listObButtonChar[k].GetComponent<EventTrigger>().triggers != null)
			{
                _listObButtonChar[k].GetComponent<EventTrigger>().triggers.Clear();
            }
            _listObButtonChar[k].GetComponent<EventTrigger>().triggers.Add(entryDown);
			_listObButtonChar[k].GetComponent<EventTrigger>().triggers.Add(entryEnter);
			_listObButtonChar[k].GetComponent<EventTrigger>().triggers.Add(entryExit);

			nameChar = _listChar[k].GetComponent<Character>().data.nameChar;
            foreach (string item in _listCharacterUnlocked)
			{
				if(nameChar == item)
				{
					isUnlocked = true;
					break;
				}
			}
			GameObject lockChar;
			lockChar = _listObButtonChar[k].transform.Find("Lock").gameObject;

            if (isUnlocked)
			{
                lockChar.SetActive(false);
			}
			else
			{
                lockChar.SetActive(true);
				lockChar.transform.Find("Cost").GetComponent<TextMeshProUGUI>().text = _listChar[k].GetComponent<Character>().data.cost.ToString();
            }
            _listObButtonChar[k].SetActive(true);
		}
	}
	public void SelectCharX(GameObject x, GameObject buttonx, int indexC)
	{
		_isTarget = true;
		_indexObButtonChar = indexC;
        if (CharacterButton)
		{
            CharacterButton.GetComponent<Image>().color = Color.white;
        }
		CharacterButton = buttonx;
        CharacterActive = x;
        CharacterButton.GetComponent<Image>().color = colorSelect;
		_canPlay = false;
		foreach(string item in _listCharacterUnlocked)
		{
			if(item == x.GetComponent<Character>().data.nameChar)
			{
                _canPlay = true;
				break;
            }
        }
		_MM.CanBuy(!_canPlay);
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
	public void ShowInfor(GameObject x)
	{
		if (!x)
		{
			return;
		}
        _inforPanel.SetActive(true);
        SetupInfor(x);
    }
    public void HideInfor(GameObject x)
    {
        _inforPanel.SetActive(false);
		if(_isTarget)
		{
            ShowInfor(CharacterActive);
        }
    }
    public void UpdateLanguage()
	{
        _font = LanguageManager.instance.GetFontAssetCurrent();
        _textName.font = _font;
        _textHP.font = _font;
        _textDef.font = _font;
        _textSpecialAbility.font = _font;
        _textSpeed.font = _font;
        _textWeapon.font = _font;
    }
	public bool CanPlay()
	{
		return _canPlay;
	}
	public void BuyChar()
	{
		if(CharacterActive)
		{
            if (_canPlay == false && _GC.SpendCoin(CharacterActive.GetComponent<Character>().data.cost) == true)
			{
				_listCharacterUnlocked.Add(CharacterActive.GetComponent<Character>().data.nameChar);
				_listObButtonChar[_indexObButtonChar].transform.Find("Lock").gameObject.SetActive(false);
				SaveDataManager.instance.SaveGame();
            }
		}
	}	
}