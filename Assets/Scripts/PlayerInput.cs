using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // Variables
    private string moveX;
    private string moveY;
    private string horizontalAxis;
    private string verticalAxis;
    private string aButton;
    private string bButton;
    private string xButton;
    private string yButton;
    private string triggerAxis;
    private int controllerNumber;

    // Sets up enumerator for buttons
    public enum Button
    {
        A,
        B,
        X,
        Y,
    }

    // Checks to see if a button is pressed then assigns it to button
    internal bool ButtonIsDown(Button button)
    {
        switch (button)
        {
            case Button.A:
                return Input.GetButton(aButton);
            case Button.B:
                return Input.GetButton(bButton);
            case Button.X:
                return Input.GetButton(xButton);
            case Button.Y:
                return Input.GetButton(yButton);
        }
        return false;
    }

    // Sets the controller number to controll specific controls
    internal void SetControllerNumber(int number)
    {
        controllerNumber = number;
        moveX = "J" + controllerNumber + "moveX";
        moveY = "J" + controllerNumber + "moveY";
        horizontalAxis = "J" + controllerNumber + "Horizontal";
        verticalAxis = "J" + controllerNumber + "Vertical";
        aButton = "J" + controllerNumber + "A";
        bButton = "J" + controllerNumber + "B";
        xButton = "J" + controllerNumber + "X";
        yButton = "J" + controllerNumber + "Y";
        triggerAxis = "J" + controllerNumber + "Trigger";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }
}
