using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAudioManager : MonoBehaviour {

	public AudioSource introClip;
	public SteamVR_LoadLevel loadLevel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!introClip.isPlaying)
		{
			loadLevel.Trigger();
		}
		
		



	}
}
