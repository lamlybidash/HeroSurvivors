using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	[SerializeField] private GameObject CharA;
	[SerializeField] private GameObject CharB;
	[SerializeField] private GameObject _menuGame;
	[SerializeField] private EnemyController _ec;


	//[SerializeField] private WeaponsData exampleData;
	private GameObject CharacterActive;
	private bool _isPause;

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
		_isPause = true;
		PauseGame(true);
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

			ToTitleGame();
		}
	}

	private void ResetGame()
	{
		_ec.PlayGameStatus(false);
		_ec.PlayGameStatus(true);
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
		_ec.PlayGameStatus(true);
		_menuGame.gameObject.SetActive(false);
		PauseGame(false);
	}

	public void ToTitleGame()
	{
		_ec.PlayGameStatus(false);
		_menuGame.gameObject.SetActive(true);
		PauseGame(true);
	}

}
