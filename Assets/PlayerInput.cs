using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    private string movementX;
    private string movementY;
    private string horizontal;
    private string vertical;
    private string aButton;
    private string bButton;
    private string xButton;
    private string yButton;
    private string triggerAxis;
    private int controllerNumber;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    internal void SetControllerNumber(int number)
    {
        controllerNumber = number;
        movementX = "J" + controllerNumber + "movementX";
        movementY = "J" + controllerNumber + "movementY";
        horizontal = "J" + controllerNumber + "Horizontal";
        vertical = "J" + controllerNumber + "Vertical";
        aButton = "J" + controllerNumber + "A";
        bButton = "J" + controllerNumber + "B";
        xButton = "J" + controllerNumber + "X";
        yButton = "J" + controllerNumber + "Y";
        triggerAxis = "J" + controllerNumber + "Trigger";
    }

    public enum Button
    {
        A,
        B,
        X,
        Y,
    }

    internal bool ButtonIsDown(Button button)
    {
        switch (button)
        {
            case Button.A:
                return Input.GetButton(aButton);
                break;
            case Button.B:
                return Input.GetButton(bButton);
                break;
            case Button.X:
                return Input.GetButton(xButton);
                break;
            case Button.Y:
                return Input.GetButton(yButton);
                break;
        }
        return false;
    }
}
