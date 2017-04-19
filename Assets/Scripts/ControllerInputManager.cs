using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputManager : MonoBehaviour {

	public bool leftController;
	
	//public SteamVR_TrackedObject trackedObj;
	public SteamVR_Controller.Device leftDevice;
	//public SteamVR_Controller.Device rightDevice;
	int leftIndex;


	// Teleporter
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


	// Use this for initialization
	void Start () {
		//trackedObj = GetComponent<SteamVR_TrackedObject>();
		//int rightIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);
		leftIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
	}
	
	// Update is called once per frame
	void Update () {

		leftDevice = SteamVR_Controller.Input(leftIndex);
		//rightDevice = SteamVR_Controller.Input(rightIndex);

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
				else player.transform.position = new Vector3(teleportLocation.x, teleportLocation.y + playerHeight, teleportLocation.z);
				
			}
		}


	}
}
