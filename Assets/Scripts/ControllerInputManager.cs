using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputManager : MonoBehaviour {

	public SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device device;
	
	public SteamVR_Controller.Device leftDevice;
	public SteamVR_Controller.Device rightDevice;
	int leftIndex;
	int rightIndex;

	// Throwing
	public float throwForce = 1.5f;


	// Teleporter

	public bool leftController;

	public LineRenderer laser;
	public GameObject teleportAimerObject;
	public GameObject disabledAimerObject;
	private Vector3 teleportLocation;
	public GameObject player;
	public LayerMask laserMask;
	public float yNudgeAmount = 2f; //specific to teleportAimerObject right
	public float teleporterMaxHorizontal = 15.0f;
	public float teleporterMaxVertical = 17.0f;
	public float playerHeight = 2.0f;
	public float xOffset = 0.25f;
	public float zOffset = -.75f;
	private bool canTeleport = false;

	// Dash
	public bool useDash;
	public float dashSpeed = 0.1f;
	private bool isDashing;
	private float lerpTime;
	private Vector3 dashStartPosition;

	// Walking
	public bool allowWalking;
	public Transform playerCam;
	public float moveSpeed = 4f;
	private Vector3 movementDirection;

	// Object Menu Swiping
	public bool rightController;

	bool menuActive = false;
	float swipeSum;
	float touchLast;
	float touchCurrent;
	float distance;
	bool hasSwipedLeft;
	bool hasSwipedRight;
	public ObjectMenuManager objectMenuManager;

	// Trigger fan
	public Fan fan;
	public AudioSource fanSound;

	// Keep track of ball on platform
	public BallReset ballReset;


	// Use this for initialization
	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		leftIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
		rightIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);
	}

	// Update is called once per frame
	void Update () {

		device = SteamVR_Controller.Input((int)trackedObj.index);
		leftDevice = SteamVR_Controller.Input(leftIndex);
		rightDevice = SteamVR_Controller.Input(rightIndex);


		/**** Teleportation ****/

		if (allowWalking && leftDevice.GetPress(SteamVR_Controller.ButtonMask.Grip))
		{
			movementDirection = playerCam.transform.forward;
			movementDirection = new Vector3(movementDirection.x, 0, movementDirection.z); // this assumes floor is always at y = 0
			movementDirection = movementDirection * moveSpeed * Time.deltaTime;
			player.transform.position += movementDirection;
		}
		
		if (isDashing && useDash)
		{
			lerpTime += 1 * dashSpeed;
			player.transform.position = Vector3.Lerp(dashStartPosition, teleportLocation + new Vector3(0, playerHeight, 0), lerpTime);

			if (lerpTime >= 1)
			{
				isDashing = false;
				lerpTime = 0;
			}
		}
		
		else
		{
			
			if (leftDevice.GetPress(SteamVR_Controller.ButtonMask.Touchpad) && laser != null)
			{
				canTeleport = false;
				laser.gameObject.SetActive(true);
				teleportAimerObject.SetActive(true);

				laser.SetPosition(0, gameObject.transform.position);
				RaycastHit hit;
				if (Physics.Raycast(transform.position, transform.forward, out hit, teleporterMaxHorizontal, laserMask))
				{
					disabledAimerObject.SetActive(false);
					teleportAimerObject.GetComponent<Renderer>().material.color = new Color(.42f, .82f, .56f, .39f);
					canTeleport = true;
					teleportLocation = hit.point;
					laser.SetPosition(1, teleportLocation);
					//aimer position
					teleportAimerObject.transform.position = new Vector3(teleportLocation.x, teleportLocation.y + yNudgeAmount, teleportLocation.z);
				}
				else
				{
					disabledAimerObject.SetActive(true);
					laser.gameObject.SetActive(false);
					teleportAimerObject.SetActive(false);
					canTeleport = false;
					teleportLocation = new Vector3(transform.forward.x * teleporterMaxHorizontal + transform.position.x, transform.forward.y * teleporterMaxHorizontal + transform.position.y, transform.forward.z * teleporterMaxHorizontal + transform.position.z);
					RaycastHit groundRay;
					if (Physics.Raycast(teleportLocation, -Vector3.up, out groundRay, teleporterMaxVertical, laserMask))
					{
						disabledAimerObject.SetActive(false);
						laser.gameObject.SetActive(true);
						teleportAimerObject.SetActive(true);
						canTeleport = true;
						teleportLocation = new Vector3(transform.forward.x * teleporterMaxHorizontal + transform.position.x, groundRay.point.y, transform.forward.z * teleporterMaxHorizontal + transform.position.z);
					}
					laser.SetPosition(1, transform.forward * teleporterMaxHorizontal + transform.position);
					//aimer
					teleportAimerObject.transform.position = teleportLocation + new Vector3(0, yNudgeAmount, 0);

				}
			}
		}
		if (leftDevice.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && laser != null)
		{
			disabledAimerObject.SetActive(false);
			laser.gameObject.SetActive(false);
			teleportAimerObject.SetActive(false);

			if (canTeleport)
			{
				if(useDash)
				{
					dashStartPosition = player.transform.position;
					isDashing = true;
				}
				else player.transform.position = new Vector3(teleportLocation.x + xOffset, teleportLocation.y + playerHeight, teleportLocation.z + zOffset);
			}
		}
		/**** End Teleportation ****/

		// Detect touchpad swipes
		if (rightDevice.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && objectMenuManager != null)
		{
			menuActive = !menuActive;
			if (menuActive)
			{
				ShowMenu();
			}
			else HideMenu();
		}

		if (rightDevice.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad) && objectMenuManager != null && menuActive == true)
		{
			touchLast = rightDevice.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;
		}

		if (rightDevice.GetTouch(SteamVR_Controller.ButtonMask.Touchpad) && objectMenuManager != null && menuActive == true)
		{
			touchCurrent = rightDevice.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;
			distance = touchCurrent - touchLast;
			touchLast = touchCurrent;
			swipeSum += distance;

			if (!hasSwipedRight && swipeSum > 0.5f && objectMenuManager != null)
			{
				swipeSum = 0;
				SwipeRight();
				hasSwipedRight = true;
				hasSwipedLeft = false;
			}
			if (!hasSwipedLeft && swipeSum < -0.5f && objectMenuManager != null)
			{
				swipeSum = 0;
				SwipeLeft();
				hasSwipedRight = false;
				hasSwipedLeft = true;
			}
		}

		if (rightDevice.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad) && objectMenuManager != null)
		{
			swipeSum = 0;
			touchCurrent = 0;
			touchLast = 0;
			hasSwipedLeft = false;
			hasSwipedRight = false;
		}

		if (rightDevice.GetPressDown(SteamVR_Controller.ButtonMask.Trigger) && objectMenuManager != null && menuActive == true)
		{
			// Spawn object currently selected by menu. You could skip the SpawnObject function, but this function gives flexibility to support other types of controllers
			SpawnObject();
			menuActive = false;
			HideMenu();
		}

		/**** Trigger Fan ****/
		if (rightDevice.GetPressDown(SteamVR_Controller.ButtonMask.Grip) && fan != null)
		{
			fan.startFan();
		}

		if (rightDevice.GetPressUp(SteamVR_Controller.ButtonMask.Grip) && fan != null)
		{
			fan.stopFan();
		}


	} /**** End Update() ****/


	/**** Continue Object Menu Swip ****/

	void ShowMenu()
	{
		objectMenuManager.ShowMenu();
	}

	void HideMenu()
	{
		objectMenuManager.HideMenu();
	}

	void SpawnObject()
	{
		objectMenuManager.SpawnCurrentObject();
	}

	void SwipeLeft()
	{
		objectMenuManager.MenuLeft();
	}

	void SwipeRight()
	{
		objectMenuManager.MenuRight();
	}
	/**** End Object Menu Swipe ****/

	/*** Grabbing and Throwing ****/
	// To use this script, add colliders (say, sphere) to controller, enable Is Trigger, and decrease Radius to 0.2
	// Then add rigidbody to controllers, setting Is Kinematic to true and Collison Detection to Continuous Dynamic.
	// Objects that you pick up should have colliders with rigidbody with collision detection set to Continuous and should have the Throwable tag on them

	private void OnTriggerStay(Collider col)
	{
		if (col.gameObject.CompareTag("Throwable") || col.gameObject.CompareTag("Ball"))
		{
			if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
			{
				ThrowObject(col);
				if (col.gameObject.CompareTag("Ball"))
				{
					DropBall();
				}
			}
			else if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
			{
				GrabObject(col);
				if (col.gameObject.CompareTag("Ball"))
				{
					GrabBall();
				}
			}
		}

		if (col.gameObject.CompareTag("Structure"))
		{
			if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
			{
				PlaceObject(col);
			}
			else if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
			{
				GrabObject(col);
			}
		}
	}

	void GrabBall()
	{
		ballReset.GrabBall();
	}

	void DropBall()
	{
		ballReset.DropBall();
	}

	void GrabObject(Collider coli)
	{
		coli.transform.SetParent(gameObject.transform);
		coli.GetComponent<Rigidbody>().isKinematic = true;
		device.TriggerHapticPulse(2000);
	}

	void ThrowObject(Collider coli)
	{
		coli.transform.SetParent(null);
		Rigidbody rigidBody = coli.GetComponent<Rigidbody>();
		rigidBody.isKinematic = false;
		rigidBody.velocity = device.velocity * throwForce;
		rigidBody.angularVelocity = device.angularVelocity;
	}

	void PlaceObject(Collider coli)
	{
		coli.transform.SetParent(null);
	}

	/**** End Grabbing and Throwing ****/
}
