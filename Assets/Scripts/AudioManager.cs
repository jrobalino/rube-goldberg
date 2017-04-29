using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public AudioSource introClip;

	GameObject[] collectibles;

	bool conveyorPlayed, teleporterPlayed, fanPlayed;
	
	// Use this for initialization
	void Start () {
		collectibles = GameObject.FindGameObjectsWithTag("Collectibles");
		Debug.Log(collectibles);
		Debug.Log(collectibles[0]);
		Debug.Log(collectibles[1]);
		Debug.Log(collectibles[2]);
		//introClip.Play();
		Debug.Log("This is the intro audio.");
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!collectibles[2].activeSelf && !conveyorPlayed)
		{
			Debug.Log("Play conveyor audio.");
			conveyorPlayed = true;
		}

		if (!collectibles[0].activeSelf && !teleporterPlayed && conveyorPlayed)
		{
			Debug.Log("Play teleporter audio.");
			teleporterPlayed = true;
		}

		if (!collectibles[1].activeSelf && !fanPlayed && teleporterPlayed)
		{
			Debug.Log("Play fan audio.");
			fanPlayed = true;
		}


	}
}
