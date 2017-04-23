using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour {

	public float force = 7f;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.name == "Ball")
		{
			Rigidbody rigidbody = collision.gameObject.GetComponent<Rigidbody>();
			rigidbody.AddForce(0, force, 0, ForceMode.Impulse);
		}
	}
}
