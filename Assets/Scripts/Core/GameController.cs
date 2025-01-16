using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
	[SerializeField] private GameObject CharA;
	[SerializeField] private GameObject CharB;
	[SerializeField] private GameObject _menuGame;
	[SerializeField] private GameObject _OverGamePanel;
	[SerializeField] private EnemyController _enc;
	[SerializeField] private WeaponsController _wec;
	[SerializeField] private TextMeshProUGUI _textCoin;
	[SerializeField] private AudioClip _acBackgound;
	[SerializeField] private AudioClip _acInGame;
	[SerializeField] private AudioClip _acMoney;

	private int _coinTotal = 0;
	private int _coinInGame = 0;
	//[SerializeField] private WeaponsData exampleData;
	private GameObject CharacterActive;
	private bool _isPause;
	private bool _isOverGame;

	//A Nam
	//public static int PlayerBestScore {
	//	get =>PlayerPrefs.GetInt("PlayerBestScore", 0);
	//	set=> PlayerPrefs.SetInt("PlayerBestScore", value);
	//}

	private void Awake()
	{
		CharacterActive = CharA;
		//Debug.Log(exampleData.countdown);
	}

	private void Start()
	{
		_coinInGame = 0;
		TakeCoinInGame(-_coinInGame);
		_isPause = true;
		PauseGame(true);
		SoundManager.instance.PlayMusic(_acBackgound);
		_enc.SetPlayer(CharacterActive.transform);
	}

	public GameObject CharActive()
	{
		return CharacterActive;
	}

	void Update()
    {
		if (Input.GetKey(KeyCode.A))
		{
			Debug.Log("Nhan A");
			CharB.gameObject.SetActive(false);
			CharA.gameObject.SetActive(true);
			CharA.GetComponent<PlayerMovement>().resetCam();
			CharacterActive = CharA;
		}
		if (Input.GetKey(KeyCode.B))
		{
			Debug.Log("Nhan B");
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
		SoundManager.instance.PlayMusic(_acInGame);
		_isOverGame = false;

		_menuGame.gameObject.SetActive(false);
		CharacterActive.GetComponent<Health>().Revive();
		IsOverGame(false);
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
		_enc.PlayGameStatus(true);
		//Clear Exp
		ExpController.instance.DestroyAllExp();
		//Reset Weapon


		PauseGame(x);
		_OverGamePanel.SetActive(x);
	}	

}
