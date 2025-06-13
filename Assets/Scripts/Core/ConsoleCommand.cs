using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleCommand : MonoBehaviour
{
    [SerializeField] private GameController _gc;
    [SerializeField] private CharacterController _cc;

    [SerializeField] private GameObject _panel;
    [SerializeField] private TMP_InputField _inputField;
    private string _command;
    private bool _isActive = false;
    private bool _isThisPause = false;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.BackQuote))
        {
            Debug.Log($"TimeScale trước = {Time.timeScale}");
            if (_isThisPause == true)
            {
                _isThisPause = false;
                Time.timeScale = 1;
            }

            if (Time.timeScale != 0 && _isActive == false)
            {
                _isThisPause = true;
                Time.timeScale = 0;
            }  
            _isActive = !_isActive;
            _panel.SetActive(_isActive);
            Debug.Log($"TimeScale sau = {Time.timeScale}");
        }
    }
    void ProcessCommand(string input)
    {
        CancelOnClick();
        input = input.Trim().ToLower();
        string[] parts = input.Split(' ');
        if (parts.Length == 2) // Kiểm tra định dạng "command value"
        {
            string command = parts[0];
            if (float.TryParse(parts[1], out float value) && value > 0)
            {
                switch (command)
                {
                    case "coin":
                        _gc.TakeTotalCoin((int) value);
                        break;
                    case "hp":
                        if (_cc.CharActive())
                        {
                            _cc.CharActive().GetComponent<Health>().Healling(value);
                        }
                        break;
                    case "exp":
                        if (_cc.CharActive())
                        {
                            _cc.CharActive().GetComponent<PlayerExp>().GainExp(value);
                        }
                        break;
                    default:
                        Debug.Log("Invalid command");
                        break;
                }
            }
            else
            {
                Debug.Log("Invalid value. Use a positive number.");
            }
        }
        else
        {
            Debug.Log("Invalid format. Use: command number (e.g., coin 100)");
        }
    }
    public void OKOnClick()
    {
        _command = _inputField.text;
        ProcessCommand(_command);
    }

    public void CancelOnClick()
    {
        _inputField.text = "";
        _command = "";
        _isActive = false;
        _panel.SetActive(false);
        if(_isThisPause)
        {
            Time.timeScale = 1;
        }
    }
}
