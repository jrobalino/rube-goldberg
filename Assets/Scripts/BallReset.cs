using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReset : MonoBehaviour {

	public GameObject startPosition;
	GameObject[] collectibles;

	public void Start()
	{
		gameObject.transform.position = startPosition.gameObject.transform.position;
		collectibles = GameObject.FindGameObjectsWithTag("Collectibles");
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

	private void ResetBall()
	{
		Rigidbody rigidBody = gameObject.GetComponent<Rigidbody>();
		rigidBody.velocity = new Vector3(0, 0, 0);
		rigidBody.angularVelocity = new Vector3(0, 0, 0);
		gameObject.transform.position = startPosition.gameObject.transform.position;
	}
}
