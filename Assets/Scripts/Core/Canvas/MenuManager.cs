using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
	[SerializeField] private GameController _GC;
	[SerializeField] private CharacterController _cc;
	[SerializeField] private SettingManager _SM;
	[SerializeField] private GameObject _playButton;
	[SerializeField] private GameObject _settingsButton;
	[SerializeField] private GameObject _quitButton;
    [SerializeField] private GameObject _buyButton;
    [SerializeField] private GameObject _selectCharPanel;
	[SerializeField] private GameObject _dailyQuestPanel;
	[SerializeField] private GameObject _settingsPanel;
    [SerializeField] private AudioClip _acSelectChar;

	//Text
	[SerializeField] private TextMeshProUGUI _titleGame;
	[SerializeField] private TextMeshProUGUI _textCoin;
	private TextMeshProUGUI _textPlayButton;
	private TextMeshProUGUI _textSettingsButton;
	private TextMeshProUGUI _textQuitButton;
	private TMP_FontAsset _font;

	private void Awake()
	{
		SetupComponent();
	}

	//Private ------------------------------------------------------------------------
	private void SetupComponent()
	{
		_textPlayButton = _playButton.transform.Find("Title").GetComponent<TextMeshProUGUI>();
		_textSettingsButton = _settingsButton.transform.Find("Title").GetComponent<TextMeshProUGUI>();
		_textQuitButton = _quitButton.transform.Find("Title").GetComponent<TextMeshProUGUI>();
	}	

	//Public --------------------------------------------------------------------------
	public void PlayButtonOnClick()
	{
		_selectCharPanel.SetActive(true);
		_cc.SetUpSelectCharPanel();
        _textCoin.text = _GC.GetCoin().ToString();
        _dailyQuestPanel.SetActive(true);
        SoundManager.instance.PlayMusic(_acSelectChar);
	}
	public void SettingsButtonOnClick()
	{
		_settingsPanel.SetActive(true);
	}
	public void QuitButtonOnClick()
	{
		Application.Quit();
	}
    public void BuyButtonOnClick()
    {
		_cc.BuyChar();
		UpdateTextCoin();
    }
    public void UpdateLanguage()
	{
		UpdateFont();
        _textPlayButton.text = LanguageManager.instance.GetText("menu", "start");
		_textSettingsButton.text = LanguageManager.instance.GetText("menu", "settings");
		_textQuitButton.text = LanguageManager.instance.GetText("menu", "exit_game");
	}

	private void UpdateFont()
	{
		_font = LanguageManager.instance.GetFontAssetCurrent();
        _textPlayButton.font = _font;
        _textSettingsButton.font = _font;
        _textQuitButton.font = _font;
    }

	public void CanBuy(bool x)
	{
		_buyButton.GetComponent<Button>().interactable = x;
	}
	public void UpdateTextCoin()
	{
        _textCoin.text = _GC.GetCoin().ToString();
    }
}
