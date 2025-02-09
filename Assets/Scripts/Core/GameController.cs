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
	[SerializeField] private EnemyController _enc;
	[SerializeField] private WeaponsController _wec;
	[SerializeField] private TextMeshProUGUI _textCoin;
	[SerializeField] private TextMeshProUGUI _textTime;
	[SerializeField] private AudioClip _acBackgound;
	[SerializeField] private AudioClip _acInGame;
	[SerializeField] private AudioClip _acMoney;
	[SerializeField] private List<GameObject> _listChar;
	[SerializeField] private List<Button> _listButtonSelectChar;


	private int _coinTotal = 0;
	private int _coinInGame = 0;
	//[SerializeField] private WeaponsData exampleData;
	private GameObject CharacterActive;
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
		SoundManager.instance.PlayMusic(_acBackgound);
		//_enc.SetPlayer(CharacterActive.transform);
		_textTime.text = "0:0";
	}

	public GameObject CharActive()
	{
		return CharacterActive;
	}

	void Update()
    {
		if (Input.GetKey(KeyCode.A))
		{
			//Debug.Log("Nhan A");
			CharB.gameObject.SetActive(false);
			CharA.gameObject.SetActive(true);
			CharA.GetComponent<PlayerMovement>().resetCam();
			CharacterActive = CharA;
		}
		if (Input.GetKey(KeyCode.B))
		{
			//Debug.Log("Nhan B");
			CharA.gameObject.SetActive(false);
			CharB.gameObject.SetActive(true);
			CharB.GetComponent<PlayerMovement>().resetCam();
			CharacterActive = CharB;
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			PauseGame(!_isPause);
		}
	}

	private void ResetGame() // reset data
	{
		_enc.PlayGameStatus(false);
		//_ec.PlayGameStatus(true);
		_coinTotal += _coinInGame;
		TakeCoinInGame(-_coinInGame);
		//_coinInGame = 0;
	}

	public void PauseGame(bool status)
	{
		if (status == true)
		{
			_isPause = true;
			Time.timeScale = 0;
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
		SetUpSelectCharPanel();
	}

	public void GoGame()
	{
		SoundManager.instance.PlayMusic(_acInGame);
		_isOverGame = false;
		_SelectCharPanel.SetActive(false);
		_menuGame.SetActive(false);
		IsOverGame(false);
		SetUpCharacter();
		_CountTimeC = StartCoroutine(CountTime());
	}
	public void ToTitleGame()
	{
		ResetGame();
		_enc.PlayGameStatus(false);
		_menuGame.gameObject.SetActive(true);
		_OverGamePanel.SetActive(false);
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
		CharacterActive.GetComponent<Health>().Revive();
		CharacterActive.gameObject.SetActive(!x);
		//Stop Count Time
		if (_CountTimeC != null && x == true)
		{
			StopCoroutine(_CountTimeC);
			_CountTimeC = null;
		}

		PauseGame(x);
		_OverGamePanel.SetActive(x);
	}	

	private void SetUpSelectCharPanel()
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

	public void ChooseCharacter(int i)
	{
		CharacterActive = _listChar[i];
	}

	private void SetUpCharacter()
	{
		if(CharacterActive.activeInHierarchy == false)
		{
			CharacterActive.SetActive(true);
		}
		_enc.SetPlayer(CharacterActive.transform);
		_wec.SetPlayer(CharacterActive.transform);
		_wec.ResetWeapon();
		//_wec.SetUpStartGame(); // đã gọi ở nâng cấp vũ khí từ 0 -> 1
		_wec.SetUpStartingWeapon(CharacterActive.GetComponent<Character>().GetStartingWeapon());
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

	public void QuitGame()
	{
		Application.Quit();
	}	
}
