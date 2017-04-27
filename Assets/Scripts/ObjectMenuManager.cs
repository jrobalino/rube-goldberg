using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectMenuManager : MonoBehaviour {
	public List<GameObject> objectList; // handled automatically at start
	public List<GameObject> objectPrefabList; // set manually in Inspector and must match order of scene menu objects
	public int inventory1, inventory2, inventory3, inventory4, inventory5;
	bool enabled1 = true;
	bool enabled2 = true;
	bool enabled3 = true;
	bool enabled4 = true;
	bool enabled5 = true;

	public SteamVR_LoadLevel loadLevel;

	public int currentObject = 0;

	// Use this for initialization
	void Start () {
		foreach (Transform child in transform)
		{
			objectList.Add(child.gameObject);
		}
	}

	public void ShowMenu()
	{
		objectList[currentObject].SetActive(true);
		
	}

	public void HideMenu()
	{
		objectList[currentObject].SetActive(false);
	}

	public void MenuLeft()
	{
		HideMenu();
		objectList[currentObject].SetActive(false);
		currentObject--;
		if (currentObject < 0)
		{
			currentObject = objectList.Count - 1;
		}
		objectList[currentObject].SetActive(true);
	}

	public void MenuRight()
	{
		HideMenu();
		objectList[currentObject].SetActive(false);
		currentObject++;
		if (currentObject >= objectList.Count)
		{
			currentObject = 0;
		}
		objectList[currentObject].SetActive(true);
	}

	public void SpawnCurrentObject()
	{
		Transform [] children = objectList[currentObject].GetComponentsInChildren<Transform>();
		Instantiate(objectPrefabList[currentObject], children[1].position, children[1].rotation);
		DecreaseInventory(currentObject);
	}

	public void DecreaseInventory(int currentObject)
	{
		switch (currentObject)
		{
			case 0:
				inventory1--;
				if (inventory1 == 0)
				{
					disableObject(currentObject);
					enabled1 = false;
				}
				break;
			case 1:
				inventory2--;
				if (inventory2 == 0)
				{
					disableObject(currentObject);
					enabled2 = false;
				}
				break;
			case 2:
				inventory3--;
				if (inventory3 == 0)
				{
					disableObject(currentObject);
					enabled3 = false;
				}
				break;
			case 3:
				inventory4--;
				if (inventory4 == 0)
				{
					disableObject(currentObject);
					enabled4 = false;
				}
				break;
			case 4:
				inventory5--;
				if (inventory5 == 0)
				{
					disableObject(currentObject);
					enabled5 = false;
				}
				break;
		}
	}

	public void disableObject(int currentObject)
	{
		Text text = objectList[currentObject].GetComponentInChildren<Text>();
		text.color = new Color(.87f, .18f, .18f);
		text.text = "You have run out of this item.";

		Renderer renderer = objectList[currentObject].GetComponentInChildren<Renderer>();
		renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0.25f);

	}

	// Update is called once per frame
	void Update () {
		
	}
}
