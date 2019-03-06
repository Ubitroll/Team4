using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtinguishObject : MonoBehaviour
{
	int currentWeapon = 0; // 0 = no weapon, 1 = "Water-Gun", 2 = "Water-Bomb", 3 = "Water-Mug"
	public GameObject[] waterSupplyObjects; // array of all the objects that human can use to refill weapons with water
	public GameObject waterGun;
	public GameObject waterBomb;
	public GameObject waterMug;
	float distanceToWater = Mathf.Infinity; // distance to water supply
	float distanceToFire = Mathf.Infinity; // distance to flamable object's point he is raycasting
	public Image extinguishBar; // fire bar displaying how much is left to fire up the object
	public Image humanCrosshair;
	public bool raycastedFire = false; // needed to use this to make the extinguish bar properly work

	private float amountFilled = 0.0f; // amount of bar to be filled (how much the human extinguished the object already)
	private Vector3 currentHumanPos;
	private bool nearWaterTrigger = false; // used on trigger enter and exit, if true the player can fill up his weapons with water
	private float waterAmount = 0.0f; // used to know how much water was poured over fired object

	// Finds and returns the closest watter supply object
	GameObject FindClosestWaterSupplyObject()
	{
		waterSupplyObjects = GameObject.FindGameObjectsWithTag ("WaterSupply");
		GameObject closestObject = null;
		float distance = Mathf.Infinity;
		Vector3 humanPosition = this.transform.position;

		foreach (GameObject waterObj in waterSupplyObjects) 
		{
			Vector3 difference = waterObj.transform.position - humanPosition;
			float currentDistance = difference.sqrMagnitude; // faster than Vector3.Distance(a,b)
			if (currentDistance < distance) 
			{
				closestObject = waterObj;
				distance = currentDistance;
			}
		}

		// Debug purposes
		//Debug.Log("Distance from " + closestObject.name + " object: " + distance);

		return closestObject;
	}

	private void FillUpWater()
	{
		Debug.Log ("Filling up current weapon with water");
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "WaterSupply")
			nearWaterTrigger = true;
		Debug.Log ("Human entered trigger " + collider.gameObject.name + " object.");
	}

	void OnTriggerExit(Collider collider)
	{
		if (collider.gameObject.tag == "WaterSupply")
			nearWaterTrigger = false;
		Debug.Log ("Human exited trigger " + collider.gameObject.name + " object.");
	}
		
	// Start is called before the first frame update
	void Start()
	{

	}

    // Update is called once per frame
    void Update()
    {
		// if the player entered the trigger associated with water supply
		if (nearWaterTrigger) 
		{
			FillUpWater ();
		}

		// stores the key player presses
		string pressedKey = Input.inputString;

		// checking which weapon should be displayed
		switch (pressedKey) 
		{
			case "0":
				currentWeapon = 0;
				waterGun.gameObject.SetActive (false);
				waterBomb.gameObject.SetActive (false);
				waterMug.gameObject.SetActive (false);
				Debug.Log ("No weapons selected!");
				break;
			case "1":
				currentWeapon = 1;
				waterGun.gameObject.SetActive (true);
				waterBomb.gameObject.SetActive (false);
				waterMug.gameObject.SetActive (false);
				Debug.Log ("Selected water gun!");
				break;
			case "2":
				currentWeapon = 2;
				waterGun.gameObject.SetActive (false);
				waterBomb.gameObject.SetActive (true);
				waterMug.gameObject.SetActive (false);
				Debug.Log ("Selected water bomb!");
				break;
			case "3":
				currentWeapon = 3;
				waterGun.gameObject.SetActive (false);
				waterBomb.gameObject.SetActive (false);
				waterMug.gameObject.SetActive (true);
				Debug.Log ("Selected water mug!");
				break;
			default:
				break;
		}

		// moving the crosshair to the center of his screen
		humanCrosshair.transform.position = new Vector3(Screen.width * 0.75f, Screen.height * 0.5f);
			
		currentHumanPos = this.transform.position;

		// UI elements showing up
		if (raycastedFire) 
		{
			// Showing text and extinguish bar
			extinguishBar.enabled = true;
			extinguishBar.transform.GetChild (0).gameObject.SetActive (true);
			extinguishBar.transform.GetChild (0).transform.localScale = new Vector3 (amountFilled, 1, 1);
		}
		else
		{
			extinguishBar.enabled = false;
			extinguishBar.transform.GetChild (0).gameObject.SetActive (false);
		}
    }
}
