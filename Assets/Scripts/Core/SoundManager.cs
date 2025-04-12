using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static SoundManager instance { get; private set; }
	[SerializeField] private AudioSource _sourceEffect;
	[SerializeField] private AudioSource _sourceBackground;
	private bool _isPauseGame;
	//Private function
	private void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;
	}
	private IEnumerator CountLoopSound(AudioClip ac, float timex)
	{
		float timeCount;
		timeCount = 0;
		while (timeCount < timex && _isPauseGame == false)
		{
			_sourceEffect.PlayOneShot(ac);
			yield return new WaitForSeconds(ac.length);
			timeCount += ac.length;
		}
	}
	//Public function
	public void PlayOneSound(AudioClip ac)
	{
		if(_isPauseGame == false)
		{
			_sourceEffect.PlayOneShot(ac);
		}
	}
	public void PlaySoundLoop(AudioClip ac, float timex)
	{
		StartCoroutine(CountLoopSound(ac, timex));
	}
	public void PlayMusic(AudioClip ac) //call PlayMusic(null) = Stop Music
	{
		_sourceBackground.Stop();
		if(ac == null)
		{
			return;
		}	
		if(_sourceBackground.clip == ac)
		{
			return;
		}
		_sourceBackground.clip = ac;
		_sourceBackground.loop = true;
		_sourceBackground.Play();
	}
	public void PauseSound(bool statusx)
	{
		_isPauseGame = statusx;
		//if (statusx == true)
		//{
		//	_sourceEffect.Pause();
		//}
		//else
		//{
		//	_sourceEffect.Play();
		//}
	}
	public void SetVolumeMusic(float valuex)
	{
		_sourceBackground.volume = valuex;
	}
	public void SetVolumeSFX(float valuex)
	{
		_sourceEffect.volume = valuex;
	}
}