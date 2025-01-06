using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static SoundManager instance { get; private set; }
	private AudioSource _source;

	private void Awake()
	{
		if (instance != null && instance == this)
		{
			Destroy(gameObject);
		}
		instance = this;
		_source = GetComponent<AudioSource>();
	}

	public void PlayOneSound(AudioClip ac)
	{
		_source.PlayOneShot(ac);
	}

	public void PlayMusic(AudioClip ac) //call PlayMusic(null) = Stop Music
	{
		_source.Stop();
		if(ac == null)
		{
			return;
		}	
		if(_source.clip == ac)
		{
			return;
		}
		_source.clip = ac;
		_source.loop = true;
		_source.Play();
	}

}
