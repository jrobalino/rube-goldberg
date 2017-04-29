using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public AudioSource introClip;
	
	// Use this for initialization
	void Start () {
		introClip.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
