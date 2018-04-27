using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance;

	public AudioSource[] bgmSource;	
	public AudioSource[] audioSource;
	public AudioClip[] audioClip;

	void Awake()
	{
		if(AudioManager.Instance == null)
			AudioManager.Instance = this;
		else
			Destroy(this.gameObject);
	}

	void Start ()
	{
		// bgmSource[0].Play();
	}
	
	void Update ()
	{
		
	}
}
