using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSounds : MonoBehaviour {

	GameObject [] ballDeaths;
	int size;

	// Use this for initialization
	void Start () {
		ballDeaths = GameObject.FindGameObjectsWithTag("Ball Death");
		size = ballDeaths.Length - 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void playRandomSound()
	{
		
		int randomNumber = Random.Range(0, size);
		ballDeaths[randomNumber].GetComponent<AudioSource>().Play();
	}
}
