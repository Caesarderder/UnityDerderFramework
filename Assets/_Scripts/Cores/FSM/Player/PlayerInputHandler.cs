using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour 
{
    public float Move;
    public float MoveNormalized ;
    public bool Jump;
    public bool Crouch;
    public bool Dash;
    public bool Interact;
    public bool LeftButtonDown;

    private void Update()
    {
        Move = Input.GetAxis("Horizontal");
        CheckKeyInput(ref Jump, KeyCode.Space);
        CheckKeyInput(ref Dash, KeyCode.LeftShift);
        CheckKeyInput(ref Interact, KeyCode.E);
        CheckKeyInput(ref Crouch, KeyCode.S);
        CheckButtonInput(ref LeftButtonDown, 0);
        CheckMoveInput();
    }

    public void CheckMoveInput()
    {
        if(Input.GetKey(KeyCode.A))
        {
            MoveNormalized= -1;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            MoveNormalized= 1;
        }
        else { MoveNormalized = 0; }
    }
    public void CheckKeyInput(ref bool value,KeyCode keyCode)
    {
        if (Input.GetKeyDown(keyCode))
        {
            value = true;
        }
        if(Input.GetKeyUp(keyCode))
            value = false;
    }

    public void CheckButtonInput(ref bool value, int buttonTpye)
    {
        if (Input.GetMouseButtonDown(buttonTpye))
        {
            value = true;
        }
        if(Input.GetMouseButtonUp(buttonTpye))
            value = false;
    }
}
