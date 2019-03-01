using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
	public float health = 100.0f; // should be 100
	public float timeToFire = 10.0f; // differs on items, time in seconds the candle needs to set the item on fire
	public float timeToExtinguish = 20.0f; // differs on items, time in seconds the human needs to pour water on to extinguish the fire
	public float durability = 1.0f; // the multiplier on how fast the item should burn
	public bool onFire = false; // boolean value to check if the item is on fire
	public bool extinguished = false; // boolean value to check if the item was extinguished, if so, the timeToFire changes

	private Color blackColor = Color.black;
	private Renderer rend;
	private Color startColor;

	// blocking any action on this object, performed when the object was fully burnt
	void BlockActionOnThisObject()
	{
		this.health = 0.0f;
		this.timeToFire = 0.0f;
		this.timeToExtinguish = 0.0f;
		this.durability = 0.0f;
		this.onFire = false;
		this.extinguished = false;

		// replacing the tag so that the candle's function won't find it as flamable object
		this.transform.gameObject.tag = "Burnt";
	}

	void ItemOnFire()
	{
		this.health -= this.durability * Time.deltaTime;

		// changing the alpha channel
		if (blackColor.a < 1)
			blackColor.a += (this.durability * Time.deltaTime) / 100;
		else
			blackColor.a = 1;

		// Debug purposes
		//Debug.Log("Alpha colour : " + blackColor.a);

		// blending from the start material to black material, blackColor.a value is between 0 and 1 therefore it can be used in Lerp(). Value 0 makes startColor 100% visible, value 1 makes blackColor 100% visible.
		rend.material.color = Color.Lerp(startColor, blackColor, blackColor.a);

		// Debug purposes
		//Debug.Log("Item: " + this.name + " health: " + this.health);
	}

	// Start is called before the first frame update
    void Start()
    {
		// setting the alpha channel to 0 - transparent
		blackColor.a = 0;
		startColor = this.GetComponent<Renderer> ().material.color;
		rend = this.GetComponent<Renderer> ();
    }
		
    // Update is called once per frame
    void Update()
    {
		// when the item is on fire
		if (this.onFire) 
		{
			ItemOnFire();
		}

		// when the item's health reaches 0
		if (this.health <= 0) 
		{
			BlockActionOnThisObject();
		}

		// if the item extinguished the next time it will take double amount of time to set it on fire. Can happen one time throughout the game that's why immediately after checking the extinguished value is changed to false.
		if (this.extinguished) 
		{
			this.timeToFire *= 2;
			extinguished = false;
		}
    }
}
