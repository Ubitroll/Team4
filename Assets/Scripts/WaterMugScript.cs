using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMugScript : MonoBehaviour
{
	public float waterAmmo = 0.0f; // how much water the weapon stores (how much can be used or how much needs to be refilled)
	public float waterAmount = 0.0f; // how much water there is in one 'shot' that can be applied on fired objects
	public float wateringSpeed = 0.0f; // how much water the weapons pours
	public float waterRange = 0.0f; // how far the water can reach 
	public bool outOfAmmo = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
