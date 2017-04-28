using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReset : MonoBehaviour {

	public GameObject startPosition;
	GameObject[] collectibles;
	public GameObject platform;
	Renderer platformRenderer, ballRenderer;
	float xMax, xMin, zMax, zMin;
	static bool ballInHand;

	public void Start()
	{
		gameObject.transform.position = startPosition.gameObject.transform.position;
		collectibles = GameObject.FindGameObjectsWithTag("Collectibles");
		platformRenderer = platform.GetComponent<Renderer>();
		ballRenderer = gameObject.GetComponentInChildren<Renderer>();
		xMax = platformRenderer.bounds.center.x + platformRenderer.bounds.extents.x;
		xMin = platformRenderer.bounds.center.x - platformRenderer.bounds.extents.x;
		zMax = platformRenderer.bounds.center.z + platformRenderer.bounds.extents.z;
		zMin = platformRenderer.bounds.center.z - platformRenderer.bounds.extents.z;
	}

	public void Update()
	{
		if (ballInHand && (gameObject.transform.position.x > xMax || gameObject.transform.position.x < xMin || gameObject.transform.position.z > zMax || gameObject.transform.position.z < zMin))
		{
			ballRenderer.material.color = new Color(ballRenderer.material.color.r, ballRenderer.material.color.g, ballRenderer.material.color.b, 0.25f);
			ResetCollectibles();
		}
		else ballRenderer.material.color = new Color(ballRenderer.material.color.r, ballRenderer.material.color.g, ballRenderer.material.color.b, 1f);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.tag == "Ground")
		{
			foreach (GameObject collectible in collectibles)
			{
				collectible.SetActive(true);
			}
			Invoke("ResetBall", 1.5f);
		}
	}

	public void ResetCollectibles()
	{
		foreach (GameObject collectible in collectibles)
		{
			collectible.SetActive(true);
		}
	}

	public void ResetBall()
	{
		Rigidbody rigidBody = gameObject.GetComponent<Rigidbody>();
		rigidBody.velocity = new Vector3(0, 0, 0);
		rigidBody.angularVelocity = new Vector3(0, 0, 0);
		gameObject.transform.position = startPosition.gameObject.transform.position;
	}

	public void GrabBall()
	{
		ballInHand = true;
	}

	public void DropBall()
	{
		ballInHand = false;
		if (gameObject.transform.position.x > xMax || gameObject.transform.position.x < xMin || gameObject.transform.position.z > zMax || gameObject.transform.position.z < zMin)
		{
			ballRenderer.material.color = new Color(ballRenderer.material.color.r, ballRenderer.material.color.g, ballRenderer.material.color.b, 0.25f);
			ResetBall();
		}
	}
}
