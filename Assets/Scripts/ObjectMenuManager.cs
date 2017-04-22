using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMenuManager : MonoBehaviour {
	public List<GameObject> objectList; // handled automatically at start
	public List<GameObject> objectPrefabList; // set manually in Inspector and must match order of scene menu objects

	public SteamVR_LoadLevel loadLevel;

	public int currentObject = 0;
	public GameObject menu;

	// Use this for initialization
	void Start () {
		foreach(Transform child in transform)
		{
			objectList.Add(child.gameObject);
		}
	}

	public void ShowMenu()
	{
		menu.SetActive(true);
	}

	public void HideMenu()
	{
		menu.SetActive(false);
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
		if (!menu.gameObject.activeSelf)
		{
			Instantiate(objectPrefabList[currentObject], objectList[currentObject].transform.position, objectList[currentObject].transform.rotation);
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
