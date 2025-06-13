using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
	[SerializeField] private Image _imgHealthBar;
	[SerializeField] private GameController _gc;
	[SerializeField] private AudioClip _acHealing;
	private float _HPTotal = 100;
	private float _HPCurrent = 100;
	private bool _canDie;
	private bool _canTakeDame = true;
	private int _reviveCount = 0;		//Số lượt hồi sinh
	private void Start()
	{
		_imgHealthBar.fillAmount = 1;
	}
	public void TakeDame(float dame)
	{
		if(_canTakeDame)
		{
            _HPCurrent = Mathf.Clamp(_HPCurrent - dame, 0, _HPTotal);
            _imgHealthBar.fillAmount = _HPCurrent / _HPTotal;
        }
		if (_HPCurrent <= 0)
		{
			if(_canDie)
			{
				if (_reviveCount > 0)
				{
					_reviveCount--;
					Revive();
				}
				else
				{
                    _gc.IsOverGame(true);
                }
            }
			else // nếu đang bất tử thì + 0.01Hp
			{
                _HPCurrent = 0.01f;
                _imgHealthBar.fillAmount = _HPCurrent / _HPTotal;
            }
        }
	}

	public void Healling(float valueHeal)
	{
		_HPCurrent = Mathf.Clamp(_HPCurrent + valueHeal, 0, _HPTotal);
		_imgHealthBar.fillAmount = _HPCurrent / _HPTotal;
		PopupController.instance.PopupWorld(((int)valueHeal).ToString(), transform.position, Color.green);
		Debug.Log("Healing");
		// sound healing
		SoundManager.instance.PlayOneSound(_acHealing);
	}

	private void Revive()
	{
		TakeDame(-_HPTotal);
	}

	public void SetupStartGame()
	{
		_canDie = true;
		_canTakeDame = true;
		_reviveCount = 0;
		Revive();
    }
	public void SetUpTotalHealth(float x)
	{
		_HPTotal = x;
		_HPCurrent = _HPTotal;
	}

	public float GetHealthCurrent()
	{
		return _HPCurrent;
	}

    public float GetHealthTotal()
    {
        return _HPTotal;
    }

	public void SetCanDie(bool x)
	{
		_canDie = x;
	}
    public void SetCanTakeDame(bool x)
    {
        _canTakeDame = x;
    }
    public void SetReviveCount(int x)
	{
		_reviveCount = x;
	}
}
