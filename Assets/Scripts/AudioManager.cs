using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public AudioSource introClip, conveyorClip, teleporterClip, fanClip;

	GameObject[] collectibles;
	Color starColor;

	bool conveyorPlayed, teleporterPlayed, fanPlayed;
	
	// Use this for initialization
	void Start () {
		collectibles = GameObject.FindGameObjectsWithTag("Collectibles");
		starColor = collectibles[0].GetComponent<Renderer>().material.color;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!collectibles[2].activeSelf && !conveyorPlayed)
		{
			conveyorPlayed = true;
			conveyorClip.Play();
		}

		if (!collectibles[0].activeSelf && !teleporterPlayed && conveyorPlayed)
		{
			teleporterPlayed = true;
			teleporterClip.Play();
		}

		if (!collectibles[1].activeSelf && !fanPlayed && teleporterPlayed)
		{
			fanPlayed = true;
			fanClip.Play();
		}

		if (introClip.isPlaying || conveyorClip.isPlaying || teleporterClip.isPlaying || fanClip.isPlaying)
		{
			foreach (GameObject collectible in collectibles)
			{
				collectible.GetComponent<Collider>().enabled = false;
				collectible.GetComponent<Renderer>().material.color = new Color(.88f, .25f, .25f, .25f);
			}
		}
		else
		{
			foreach (GameObject collectible in collectibles)
			{
				collectible.GetComponent<Collider>().enabled = true;
				collectible.GetComponent<Renderer>().material.color = starColor;
			}
		}
		



	}
}
