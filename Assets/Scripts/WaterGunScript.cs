using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterGunScript : MonoBehaviour
{
	public float waterAmmoClip = 10.0f; // how much water the weapon stores (how much can be used or how much needs to be refilled)
	public float waterAmmoAll = 20.0f;
	public float waterAmount = 5.0f; // how much water there is in one 'shot' that can be applied on ONE fired objects
	public float waterRange = 10.0f; // how far the water can reach 
	public float amountFilled = 0.0f;
	public bool outOfAmmo = false;

	public Text ammoText;
	private bool isAbleToShoot = true;
	private bool isReloading = false;

	IEnumerator DelayShooting()
	{
		isAbleToShoot = false;
		yield return new WaitForSeconds (0.1f);
		isAbleToShoot = true;
	}

	void Reload()
	{
		// ammoToFill = clip size - current clip size
		float ammoToFill = 10 - waterAmmoClip;

		// If there is enough ammo in reserve to reload
		if (ammoToFill - waterAmmoAll < 0) 
		{
			waterAmmoClip += ammoToFill;
			waterAmmoAll = 0;
		} else 
		{
			waterAmmoAll -= waterAmmoClip;
			waterAmmoClip = 10;
		}
	}

	void ShootWater()
	{
		Debug.Log ("Shooting water!");
		if (waterAmmoClip > 0) 
		{
			waterAmmoClip--;

			RaycastHit hit = new RaycastHit ();

			// raycast
			if (Physics.Raycast (this.transform.position, transform.TransformDirection (Vector3.forward), out hit, waterRange)) {	
				// used to change variable that shows up UI
				ExtinguishObject extinguishObject = GameObject.Find ("Human").GetComponent<ExtinguishObject> ();

				// if the player shot flamable object that is on fire
				if (hit.collider.gameObject.tag == "Flamable") 
				{
					ItemScript itemScript = hit.collider.gameObject.GetComponent<ItemScript> ();	
				
					Debug.Log ("Raycasted flamable object: " + hit.collider.name);

					// used to show up UI elements, when the player points at fired object
					if (itemScript.onFire && extinguishObject != null) 
					{
						extinguishObject.raycastedFire = true;
						amountFilled += waterAmount;
						Debug.Log ("Amount Filled " + amountFilled);

						if (amountFilled >= itemScript.waterAmountNeeded) 
						{
							itemScript.extinguished = true;
						}
					} 
					else 
					{
						extinguishObject.raycastedFire = false;
					}

				} else if (extinguishObject != null) // checking first if the object was found
					extinguishObject.raycastedFire = false;
			}
		} else
			outOfAmmo = true;
	}

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
	{
		// checking if the gun is activated by player
		if (this.gameObject.activeSelf) 
		{
			if (waterAmmoClip <= 0) 
			{
				waterAmmoClip = 0;
				outOfAmmo = true;
			}

			if (Input.GetButton ("Fire1") && isAbleToShoot && !(isReloading)) 
			{
				StartCoroutine (DelayShooting ());
				ShootWater ();
			}

			if (Input.GetKey (KeyCode.R))
				Reload ();

			ammoText.text = "Ammo " + waterAmmoClip + " / " + maxWaterAmmoAll;
		} 
		else 
		{
			ammoText.enabled = false;	
		}
	}
}
