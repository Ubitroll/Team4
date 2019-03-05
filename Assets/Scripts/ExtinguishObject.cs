using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtinguishObject : MonoBehaviour
{
	int currentWeapon = 0; // 0 = no weapon, 1 = "Water-Gun", 2 = "Water-Bomb", 3 = "Water-Mug"
	public GameObject[] waterSupplyObjects; // array of all the objects that candle can burn
	float distanceToWater = Mathf.Infinity; // distance to water supply
	float distanceToFire = Mathf.Infinity; // distance to flamable object's point he is raycasting
	public Image extinguishBar; // fire bar displaying how much is left to fire up the object
	public Image humanCrosshair;
	private float amountFilled = 0.0f; // amount of bar to be filled (how much the human extinguished the object already)

	private Vector3 currentHumanPos;
	private bool raycastedFire = false; // needed to use this to make the extinguish bar properly work
	private float waterAmount = 0.0f;
	private float gunAmmo = 0.0f;
	private float bombAmmo = 0.0f;
	private float mugAmmo = 0.0f;
   
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

	private void FillUpWater(bool boolean)
	{
		if (boolean) 
		{
			Debug.Log ("Filling up current weapon with water");
		}
		else 
		{
			Debug.Log("Stopped filling up current weapon with water");
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "WaterSupply")
			FillUpWater (true);

		Debug.Log (collision.gameObject.name + " object!");
	}

	void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.tag == "WaterSupply")
			FillUpWater (false);

		Debug.Log (collision.gameObject.name + " object! exit");
	}


	// Start is called before the first frame update
	void Start()
	{

	}

    // Update is called once per frame
    void Update()
    {
		// moving the crosshair to the center of his screen
		humanCrosshair.transform.position = new Vector3(Screen.width * 0.75f, Screen.height * 0.5f);
			
		currentHumanPos = this.transform.position;

		RaycastHit hit;
			
		// raycast
		if (Physics.Raycast (this.transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity)) 
		{	
			if (hit.collider.gameObject.tag == "Flamable") 
			{
				ItemScript itemScript = hit.collider.gameObject.GetComponent<ItemScript> ();	

				Debug.Log ("HIT OBJECT " + hit.collider.name);

				amountFilled = waterAmount / itemScript.waterAmountNeeded;

				// UI elements showing up
				if (itemScript.onFire) 
				{
					raycastedFire = true;
				}
				else
				{
					raycastedFire = false;
				}

			}
			else
				raycastedFire = false;
		}


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
