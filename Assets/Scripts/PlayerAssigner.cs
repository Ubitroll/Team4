using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAssigner : MonoBehaviour
{

    private List<int> assignedControllers = new List<int>();
    private 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        for (int i = 0; i < 1; i++)
        {
            if (Input.GetButton("J" + i + "A" ))
            {
                AddPlayerController(i);
                break;
            }


        }
    }

    public
}
