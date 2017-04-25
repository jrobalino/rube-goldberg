﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

	public GameObject [] collectibles;
	
	// Use this for initialization
	void Start () {
		collectibles = GameObject.FindGameObjectsWithTag("Collectibles");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.name == "Ball")
		{
			foreach (GameObject collectible in collectibles)
			{
				if (collectible.activeSelf)
				{
					break;
				}
				else collision.gameObject.SetActive(false);
			}
			
		}
	}
}
