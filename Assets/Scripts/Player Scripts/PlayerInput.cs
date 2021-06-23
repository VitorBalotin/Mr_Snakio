using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour{
    // Gets the player controller(keyboard)
    private PlayerController player_controller;
    // Possible directions that the player could move the snake
    private int movement = 0;
    
    // Used awake to get the player controller
    void Awake(){
        player_controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update(){
        // Resets the value every update
        // Grabs the input
        movement = GetAxisRaw();
        SetMovement();
    }
    void SetMovement(){
        if (movement == 1) {
            if(player_controller.direction== PlayerDirection.WEST) player_controller.SetInputDirection(PlayerDirection.NORTH);
            
            else if(player_controller.direction== PlayerDirection.NORTH) player_controller.SetInputDirection(PlayerDirection.EAST);
            else if(player_controller.direction== PlayerDirection.EAST) player_controller.SetInputDirection(PlayerDirection.SOUTH);
            else if(player_controller.direction== PlayerDirection.SOUTH) player_controller.SetInputDirection(PlayerDirection.WEST);
        } else if (movement == -1) {
            if(player_controller.direction== PlayerDirection.WEST) player_controller.SetInputDirection(PlayerDirection.SOUTH);
            else if(player_controller.direction== PlayerDirection.SOUTH) player_controller.SetInputDirection(PlayerDirection.EAST);
            else if(player_controller.direction== PlayerDirection.EAST) player_controller.SetInputDirection(PlayerDirection.NORTH);
            else if(player_controller.direction== PlayerDirection.NORTH) player_controller.SetInputDirection(PlayerDirection.WEST);
        }
    }

    int GetAxisRaw(){
        // Validates which key was pressed, returns 0 if nothing changed or 1 or -1 to change the direction
        bool left = Input.GetKeyDown(KeyCode.LeftArrow);
        bool right = Input.GetKeyDown(KeyCode.RightArrow);
        
        if(left)
            return -1;
        else if(right)
            return 1;

        return 0;
    }
}
