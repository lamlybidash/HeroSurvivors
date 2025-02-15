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
	[SerializeField] private GameObject _PausePanel;
	[SerializeField] private TextMeshProUGUI _textCoin;
	[SerializeField] private TextMeshProUGUI _textTime;
	[SerializeField] private TextMeshProUGUI _textScore;
	[SerializeField] private TextMeshProUGUI _textHighScore;
	[SerializeField] private AudioClip _acBackgound;
	[SerializeField] private AudioClip _acInGame;
	[SerializeField] private AudioClip _acMoney;
	[SerializeField] private Button _resumeBt;
	[SerializeField] private Button _titleBt;

	[SerializeField] private EnemyController _enc;
	[SerializeField] private CharacterController _cc;
	[SerializeField] private WeaponsController _wec;

	private int _coinTotal = 0;
	private int _coinInGame = 0;
	private int _highScore = 0;
	private int _score = 0;
	//[SerializeField] private WeaponsData exampleData;
	private bool _isPause;
	private bool _isOverGame;
	private Coroutine _CountTimeC;

	//A Nam
	//public static int PlayerBestScore {
	//	get =>PlayerPrefs.GetInt("PlayerBestScore", 0);
	//	set=> PlayerPrefs.SetInt("PlayerBestScore", value);
	//}

	private void Awake()
	{
		//CharacterActive = CharA;
		//Debug.Log(exampleData.countdown);
	}

	private void Start()
	{
		_coinInGame = 0;
		TakeCoinInGame(-_coinInGame);
		_isPause = true;
		PauseGame(true);
		SetUpButton();
		SoundManager.instance.PlayMusic(_acBackgound);
		//_enc.SetPlayer(CharacterActive.transform);
		_textTime.text = "00:00";
		_score = 0;
		_highScore = PlayerPrefs.GetInt("HighScore", 0);
		_textScore.text = $"Score: {_score}";
		_textHighScore.text = $"High Score:  {_highScore}";
	}



	void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape) && _menuGame.activeInHierarchy == false && _OverGamePanel.activeInHierarchy == false)
		{
			PauseGame(!_isPause);
			_PausePanel.SetActive(_isPause);
		}
	}

	private void ResetGame() // reset data
	{
		_enc.PlayGameStatus(false);
		//_ec.PlayGameStatus(true);
		_coinTotal += _coinInGame;
		TakeCoinInGame(-_coinInGame);
		//_coinInGame = 0;
		IsOverGame(true);
	}

	public void PauseGame(bool status)
	{
		if (status == true)
		{
			_isPause = true;
			Time.timeScale = 0;
			if (_highScore >= PlayerPrefs.GetInt("HighScore", 0))
			{
				PlayerPrefs.SetInt("HighScore", _highScore);
				PlayerPrefs.Save();
			}
		}
		else
		{
			_isPause = false;
			Time.timeScale = 1;
		}
	}

	public void PlayGame()
	{
		_SelectCharPanel.SetActive(true);
		_cc.SetUpSelectCharPanel();
	}

	public void GoGame()
	{
		SoundManager.instance.PlayMusic(_acInGame);
		_isOverGame = false;
		_SelectCharPanel.SetActive(false);
		_menuGame.SetActive(false);
		IsOverGame(false);
		SetUpCharacter();
		ItemDropManager.instance.StartCoroutineDropF();
		_CountTimeC = StartCoroutine(CountTime());
	}
	public void ToTitleGame()
	{
		ResetGame();
		_enc.PlayGameStatus(false);
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
	public void TakeCoin(int x)
	{
		_coinInGame += x;
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

		// Lưu điểm cao nhất
		if(x)
		{
			if (_highScore >= PlayerPrefs.GetInt("HighScore", 0))
			{
				PlayerPrefs.SetInt("HighScore", _highScore);
				PlayerPrefs.Save();
			}	
			IncreaseScore(-_score);
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
	public void QuitGame()
	{
		Application.Quit();
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
}
