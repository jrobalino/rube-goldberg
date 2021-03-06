﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

	GameObject ball;
	Transform target;
	Vector3 newPosition;
	AudioSource teleporterSound;
	
	// Use this for initialization
	void Start () {
		teleporterSound = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		target = FindObjectOfType<Teleporter>().transform;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.name == "Ball")
		{

			
			ball = collision.gameObject;

			if (target != transform)
			{
				teleporterSound.Play();
				newPosition = target.position + target.forward.normalized * 0.3f;
				
				ball.transform.position = newPosition;
			}
			
		}
	}
}
