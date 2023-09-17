using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputChecker : MonoBehaviour
{
    //Character Input
    public Vector2 move;
    public Vector2 look;
    public bool roll;
    public bool jump;
    public bool fire;
    public bool sprint;
    public bool aim;
    public bool hurt;
    
    //MouseCursor
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;

    //Getting Input Values
    public void OnMove(InputValue value)
    {   
        MoveInput(value.Get<Vector2>());
    }

    public void OnLook(InputValue value)
    {
        if(cursorInputForLook)
        {
            LookInput(value.Get<Vector2>());
        }
    }
    public void OnRoll(InputValue value)
    {   
        RollInput(value.isPressed);
    }
    public void OnJump(InputValue value)
    {   
        JumpInput(value.isPressed);
    }
    public void OnSprint(InputValue value)
    {
        SprintInput(value.isPressed);
    }
    public void OnAim(InputValue value)
    {   
        aim = !aim;
    }
    public void OnFire(InputValue value)
    {   
        FireInput(value.isPressed);
    }
    public void OnHurt(InputValue value)
    {   
        HurtInput();
    }
    public void HurtInput()
    {
        hurt = !hurt;
    }
    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLocked);
    }

    //Assigning InputValues
    public void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
    } 

    public void LookInput(Vector2 newLookDirection)
    {
        look = newLookDirection;
    }

    public void RollInput(bool newRollState)
    {
        roll = newRollState;
    }

    public void SprintInput(bool newSprintState)
    {
        sprint = newSprintState;
    }
    public void JumpInput(bool newJumpState)
    {
        jump = newJumpState;
    }
    public void FireInput(bool newFireState)
    {
        fire = newFireState;
    }
    
    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}

