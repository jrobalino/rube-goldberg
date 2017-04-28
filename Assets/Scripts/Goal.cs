using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

	public SteamVR_LoadLevel loadLevel;
	GameObject [] collectibles;
	public BallReset ballReset;
	
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
			int size = collectibles.Length;
			int count = 0;
			foreach (GameObject collectible in collectibles)
			{
				if (collectible.activeSelf)
				{
					ballReset.ResetBall();
					count++;
					break;
				}
				else
				{
					count++;
					if (count == size)
					{
						collision.gameObject.SetActive(false);
						loadLevel.Trigger();
					}
				}
			}
			

		}
	}
}
