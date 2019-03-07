using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBombScript : MonoBehaviour
{
	public float waterAmmo = 0.0f; // how much water the weapon stores (how much can be used or how much needs to be refilled)
	public float waterAmount = 0.0f; // how much water there is in one 'shot' that can be applied on fired objects
	public float wateringSpeed = 0.0f; // how much water the weapons pours
	public float waterRadius = 0.0f; // how far the water can reach 
	public int numberOfBombs = 0; // how many water bombs the player has
	public float timeAlive = 0.0f;

	void OnCollisionEnter(Collision collision)
	{
		Debug.Log (collision.gameObject.name);
		// had issues with object being destroyed at the start because it collided with the human
		if(timeAlive > 0.2f)
			Destroy (this.gameObject);
	}

    // Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
		timeAlive += Time.deltaTime;

		if (this.gameObject.activeSelf) 
		{

		} // end of activeSelf
    }
}
