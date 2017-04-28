using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour {

	public float fanSpeed = 700f;
	static bool spinFan;
	float lerpSpeed = 1f;
	Rigidbody ballRigidbody;

	GameObject ball;
	GameObject goal;

	Vector3 startPos;

	// Use this for initialization
	void Start () {

		ball = GameObject.FindGameObjectWithTag("Ball");
		goal = GameObject.FindGameObjectWithTag("Goal");
		ballRigidbody = ball.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

		startPos = ball.transform.position;
		if (spinFan)
		{
			gameObject.transform.Rotate(0, 0, fanSpeed * Time.deltaTime);
			ballRigidbody.velocity = new Vector3(0, 0, 0);
			ballRigidbody.useGravity = false;
			ball.transform.position = Vector3.Lerp(startPos, goal.transform.position, Time.deltaTime * lerpSpeed);
		}
		else ballRigidbody.useGravity = true;
	}

	public void startFan()
	{
		spinFan = true;
	}

	public void stopFan()
	{
		spinFan = false;
	}
}
