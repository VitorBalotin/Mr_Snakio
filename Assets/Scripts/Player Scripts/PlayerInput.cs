using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour{
    // Gets the player controller(keyboard)
    private PlayerController player_controller;
    // Possible directions that the player could move the snake
    private int horizontal = 0, vertical = 0;
    public enum Axis{
        Horizontal,
        Vertical
    }

    // Used awake to get the player controller
    void Awake(){
        player_controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update(){
        // Resets the value every update
        horizontal = 0;
        vertical = 0;
        // Grabs the input
        GetKeyboardInput();
        SetMovement();
    }

    void GetKeyboardInput(){
        horizontal = GetAxisRaw(Axis.Horizontal);
        vertical = GetAxisRaw(Axis.Vertical);
        if(horizontal != 0){
            vertical = 0;
        }
    }

    void SetMovement(){
        if(vertical != 0){
            player_controller.SetInputDirection((vertical == 1) ? PlayerDirection.UP : PlayerDirection.DOWN);
        }else if(horizontal != 0){
            player_controller.SetInputDirection(horizontal == 1 ? PlayerDirection.RIGHT : PlayerDirection.LEFT);
        }

    }

    int GetAxisRaw(Axis axis){
        // Validates which key was pressed, returns 0 if nothing changed or 1 or -1 to change the direction
        if(axis == Axis.Horizontal){
            bool left = Input.GetKeyDown(KeyCode.LeftArrow);
            bool right = Input.GetKeyDown(KeyCode.RightArrow);
            if(left){
                return -1;
            }
            if(right){
                return 1;
            }
            return 0;
        }else if(axis == Axis.Vertical){
            bool up = Input.GetKeyDown(KeyCode.UpArrow);
            bool down = Input.GetKeyDown(KeyCode.DownArrow);
            if(up){
                return 1;
            }
            if(down){
                return -1;
            }
            return 0;
        }
        return 0;
    }
}
