using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour {

	GameObject ball;
	public float speed = 2.5f;
	Transform startPos;
	Transform[] positions;
	Transform endPos;

	bool moveBall = false;
	
	// Use this for initialization
	void Start () {
		positions = gameObject.GetComponentsInChildren<Transform>();
		endPos = positions[1];
	}
	
	// Update is called once per frame
	void Update () {
		
		if (moveBall)
		{
			ball.transform.position = Vector3.Lerp(startPos.position, endPos.position, Time.deltaTime * speed);
		}

	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.name == "Ball")
		{
			moveBall = true;
			ball = collision.gameObject;
			startPos = ball.transform;
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.name == "Ball")
		{
			moveBall = false;
		}
	}
}
