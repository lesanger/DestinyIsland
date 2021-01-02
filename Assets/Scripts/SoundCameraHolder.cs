using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCameraHolder : MonoBehaviour
{
	private AudioSource audioSource;
	public AudioClip audioClip;

	private void Awake()
	{
		audioSource = gameObject.GetComponent<AudioSource>();
	}

	public void WalkStep()
    {
	    //audioSource.PlayOneShot(audioClip);
	    Debug.Log("Step");
    }
}
