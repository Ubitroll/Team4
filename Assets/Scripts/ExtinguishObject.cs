using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtinguishObject : MonoBehaviour
{
	int currentWeapon = 0; // 0 = no weapon, 1 = "Water-Gun", 2 = "Water-Bomb", 3 = "Water-Mug"
	public GameObject[] waterSupplyObjects; // array of all the objects that human can use to refill weapons with water
	public List<GameObject> waterWeapons;
	public GameObject waterGun;
	public GameObject waterBomb;
	public GameObject waterMug;
	float distanceToWater = Mathf.Infinity; // distance to water supply
	float distanceToFire = Mathf.Infinity; // distance to flamable object's point he is raycasting
	public Image extinguishBar; // fire bar displaying how much is left to fire up the object
	public Image humanCrosshair;
	public float throwForce = 30.0f;
	public bool raycastedFire = false; // needed to use this to make the extinguish bar properly work

	private float amountFilled = 0.0f; // amount of bar to be filled (how much the human extinguished the object already)
	private Vector3 currentHumanPos;
	private bool nearWaterTrigger = false; // used on trigger enter and exit, if true the player can fill up his weapons with water
	private float waterAmount = 0.0f; // used to know how much water was poured over fired object
	private bool isAbleToThrow = true; // associated with throwing the water bomb

	private void FillUpWater(GameObject weapon)
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
		
	IEnumerator DelayThrowing()
	{
		isAbleToThrow = false;
		yield return new WaitForSeconds (0.2f);
		isAbleToThrow = true;
	}

	void ThrowWaterBomb()
	{
		GameObject bomb = Instantiate (Resources.Load("Prefabs/Water-Bomb"), waterBomb.transform.position, Quaternion.identity) as GameObject;
		bomb.GetComponent<Rigidbody> ().AddForce (transform.forward * throwForce, ForceMode.Impulse);
	}

	// Start is called before the first frame update
	void Start()
	{
		waterWeapons = new List<GameObject> ();
		// assigning gameobjects to waterWeapons array
		waterWeapons.Add(waterGun);
		waterWeapons.Add(waterBomb);
		waterWeapons.Add(waterMug);
	}

    // Update is called once per frame
    void Update()
    {
		// if the player entered the trigger associated with water supply
		if (nearWaterTrigger) 
		{
			// if current weapon isn't no weapon
			if(currentWeapon != 0)
				FillUpWater (waterWeapons[currentWeapon - 1]);
		}

		// stores the key player presses
		string pressedKey = Input.inputString;

		// checking which weapon should be displayed
		switch (pressedKey) 
		{
			case "0":
				currentWeapon = 0;
				// disactivating all weapons
				for (int i = 0; i < waterWeapons.Count; i++) 
				{
					waterWeapons [i].SetActive (false);
				}
				//Debug.Log ("No weapons selected!");
				break;
			case "1":
				currentWeapon = 1;
				// making current weapon gameobject active and disactivating others
				for (int i = 0; i < waterWeapons.Count; i++) 
				{
					if (i == currentWeapon - 1)
						waterWeapons [i].SetActive (true);
					else
						waterWeapons [i].SetActive (false);
				}
				//Debug.Log ("Selected water gun!");
				break;
			case "2":
				currentWeapon = 2;
				// making current weapon gameobject active and disactivating others
				for (int i = 0; i < waterWeapons.Count; i++) 
				{
					if (i == currentWeapon - 1)
						waterWeapons [i].SetActive (true);
					else
						waterWeapons [i].SetActive (false);
				}
				//Debug.Log ("Selected water bomb!");
				break;
			case "3":
				currentWeapon = 3;
				// making current weapon gameobject active and disactivating others
				for (int i = 0; i < waterWeapons.Count; i++) 
				{
					if (i == currentWeapon - 1)
						waterWeapons [i].SetActive (true);
					else
						waterWeapons [i].SetActive (false);
				}
				//Debug.Log ("Selected water mug!");
				break;
			default:
				break;
		}

		// throwing needs to be in this script rather than in the WaterBombScript
		if (waterWeapons[1].gameObject.activeSelf) 
		{

			if (Input.GetButton ("Fire1") && isAbleToThrow) 
			{
				StartCoroutine (DelayThrowing ());
				ThrowWaterBomb ();
			}

		} // end of activeSelf

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
