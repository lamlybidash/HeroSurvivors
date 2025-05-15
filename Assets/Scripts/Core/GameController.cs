using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject CharA;
	[SerializeField] private GameObject CharB;
	[SerializeField] private GameObject _menuGame;
	[SerializeField] private GameObject _OverGamePanel;
	[SerializeField] private GameObject _SelectCharPanel;
    [SerializeField] private GameObject _dailyQuestPanel;
	[SerializeField] private GameObject _SettingPanel;
    [SerializeField] private GameObject _PausePanel;
    [SerializeField] private TextMeshProUGUI _textCoin;
	[SerializeField] private TextMeshProUGUI _textTime;
	[SerializeField] private TextMeshProUGUI _textScore;
	[SerializeField] private TextMeshProUGUI _textHighScore;
	[SerializeField] private AudioClip _acBackgound;
	[SerializeField] private AudioClip _acSelectChar;
	[SerializeField] private AudioClip _acInGame;
	[SerializeField] private AudioClip _acMoney;
	[SerializeField] private AudioClip _acSpendMoney;
	[SerializeField] private Button _resumeBt;
	[SerializeField] private Button _titleBt;

	[SerializeField] private EnemyController _enc;
	[SerializeField] private CharacterController _cc;
	[SerializeField] private WeaponsController _wec;
	[SerializeField] private SkillController _sc;

    private int _coinTotal = 0;	//Tương tác với 2n + 3 (Tránh cheat engine)
	private int _coinInGame = 0;
	private int _highScore = 0;
	private int _score = 0;
    private List<string> _listCharacterOwned;

    private bool _isPause;
	private bool _isOverGame;
	private Coroutine _CountTimeC;
	private SaveData _saveData;
	private void Awake()
	{

	}

	private void Start()
	{
        LoadGame();
        _isPause = true;
		PauseGame(true);
		SoundManager.instance.PlayMusic(_acBackgound);
		SetUpButton();
		_textTime.text = "00:00";
		_score = 0;
		_textScore.text = $"Score: {_score}";
		_textHighScore.text = $"High Score:  {_highScore}";
        //DailyQuestController.Instance.SetupGC(this);
    }

    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape) && _menuGame.activeInHierarchy == false && _OverGamePanel.activeInHierarchy == false)
		{
			PauseGame(!_isPause);
			_PausePanel.SetActive(_isPause);
		}

        if (Input.GetKeyDown(KeyCode.S))
        {
			SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
			LoadGame();
        }
    }


	public void PauseGame(bool status)
	{
		if (status == true)
		{
			Time.timeScale = 0;
			if (_score >= _highScore)
			{
                _highScore = _score;
            }
			SaveGame();
		}
		else
		{
			Time.timeScale = 1;
		}
		_isPause = status;
		SoundManager.instance.PauseSound(status);
	}

	public void GoGame()
	{
		if(_cc.CanPlay() == false)
		{
			PopupController.instance.PopupCanvas("Đéo sở hữu tướng, không thể chơi");
			return;
		}	

		SoundManager.instance.PlayMusic(_acInGame);
		_isOverGame = false;
		_SelectCharPanel.SetActive(false);
		_menuGame.SetActive(false);
		_dailyQuestPanel.SetActive(false);
		IsOverGame(false);
		SetUpCharacter();
		ItemDropManager.instance.StartCoroutineDropF();
		DailyQuestController.Instance.StartQuest();
		_CountTimeC = StartCoroutine(CountTime());
	}
	public void ToTitleGame()
	{
		IsOverGame(true);
		_menuGame.gameObject.SetActive(true);
		_OverGamePanel.SetActive(false);
		_PausePanel.SetActive(false);
		SoundManager.instance.PlayMusic(_acBackgound);
	}
	public void TakeCoinInGame(int x)
	{
		_coinInGame += x;
		_textCoin.text = _coinInGame.ToString();
		SoundManager.instance.PlayOneSound(_acMoney);
	}
    public void TakeTotalCoin(int x)
    {
        _coinTotal = EncryptCoin(DecryptCoin(_coinTotal) + x);
		Debug.Log($"Gain{x} total: {DecryptCoin(_coinTotal)}");
    }
    public void IsOverGame(bool x)
	{
		_isOverGame = x;
		//Clear Enemy
		_enc.PlayGameStatus(!x);
		//Clear Exp
		ExpController.instance.DestroyAllExp(x);
		//Reset Weapon
		_wec.ResetWeapon();
		// InActive Character Current
		//CharacterActive.GetComponent<Health>().Revive();
		//CharacterActive.gameObject.SetActive(!x);
		_cc.CharActive().GetComponent<Health>().Revive();
		_cc.CharActive().gameObject.SetActive(!x);
		//Stop Count Time
		if (_CountTimeC != null && x == true)
		{
			StopCoroutine(_CountTimeC);
			_CountTimeC = null;
		}
		if(x == true)
		{
			ItemDropManager.instance.StopCoroutieDropF();
			ItemDropManager.instance.ClearAllItemInMap();


        }
        //Lưu data
        if (x == true)
		{
            // Sửa hight score
            if (_score >= _highScore)
            {
                _highScore = _score;
            }
            IncreaseScore(-_score);
			// Cộng tiền total
			_coinTotal = EncryptCoin(DecryptCoin(_coinTotal) + _coinInGame);
            TakeCoinInGame(-_coinInGame);

			//Lưu
            SaveGame();
        }	

        PauseGame(x);
		_OverGamePanel.SetActive(x);
	}
	private void SetUpCharacter()
	{
		if(_cc.CharActive().activeInHierarchy == false)
		{
			_cc.CharActive().SetActive(true);
		}
		_cc.ResetAllCharacter();
		_cc.SetFollowerForCamera();
		_enc.SetPlayer(_cc.CharActive().transform);
		_wec.SetPlayer(_cc.CharActive().transform);
		_wec.ResetWeapon();
		//_wec.SetUpStartGame(); // đã gọi ở nâng cấp vũ khí từ 0 -> 1
		_wec.SetUpStartingWeapon(_cc.CharActive().GetComponent<Character>().GetStartingWeapon());
		//Setup Skill
		_sc.SetupSkill();
	}

	// đếm thời gian
	private IEnumerator CountTime()
	{
		string timeString = "";
		int seconds;
		int minutes;
		seconds = 0;
		minutes = 0;
		while(_isOverGame == false)
		{
			seconds++;
			if(seconds == 60)
			{
				minutes++;
				seconds = 0;
			}
			timeString = "";
			if (minutes < 10)
			{
				timeString += "0";
			}
			timeString += minutes.ToString() + ":";

			if (seconds < 10)
			{
				timeString += "0";
			}
			timeString += seconds.ToString();
			_textTime.text = timeString;
			DailyQuestEvent.TimePassed(1);
			yield return new WaitForSeconds(1);
		}
		_textTime.text = "0:0";
	}

	// Check game mất focus
	private void OnApplicationFocus(bool focus)
	{
		if (focus == false && _isPause == false)
		{
			PauseGame(!focus);
			_PausePanel.SetActive(true);
		}
	}


	public void IncreaseScore(int x)
	{
		_score += x;
		_textScore.text = $"Score: {_score}";
		if (_score > _highScore)
		{
			_highScore = _score;
			_textHighScore.text = $"High Score:  {_highScore}";
		}
	}
	public void SetUpButton()
	{
		_resumeBt.onClick.AddListener(() => 
			{
				PauseGame(false);
				_PausePanel.SetActive(false); 
			});
		_titleBt.onClick.AddListener(() => ToTitleGame());
	}
	private void SaveGame()
	{
		_saveData.coin = _coinTotal;
		_saveData.best_score = _highScore;
		_saveData.list_character = _listCharacterOwned;
        SaveDataManager.instance.SaveGame();
    }
    private void LoadGame()
    {
        SaveDataManager.instance.LoadGame();
		_saveData = SaveDataManager.instance.GetSave();
        _coinTotal = _saveData.coin;
        _highScore = _saveData.best_score;
        _listCharacterOwned = _saveData.list_character;
    }
	private int DecryptCoin(int x)
	{
		return (int)((x - 3) / 2);
	}
    private int EncryptCoin(int x)
    {
		return x * 2 + 3;
    }

    #region Get Set Data
    public int GetCoin()
	{
		return DecryptCoin(_coinTotal);
	}
	public bool SpendCoin(int x)
	{
		if (x > DecryptCoin(_coinTotal))
		{
			PopupController.instance.PopupCanvas("Đéo đủ tiền để dùng");
			return false;
		}
		else
		{
			_coinTotal = EncryptCoin(DecryptCoin(_coinTotal) - x);
			SaveGame();
			SoundManager.instance.PlayOneSoundOnSystem(_acSpendMoney);
            return true;
		}
	}
    #endregion
}
//TODO chơi lại chưa reset vị trí đứng của player
//TODO Chỉnh slider xong k Apply back ra vào lại slider k được trả về đúng giá trị
//TODO Đồng bộ logic của wind với các weapon khác (Wind -> Control, Projectile -> Dame)
//TODO Chỉnh lại Update All Language trong LanguageManager. Sử dụng design pattern obsever Update(System.Action);
