using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeCollider : MonoBehaviour
{
    // Start is called before the first frame update
    public SnakeMovement movement;
    private void OnTriggerEnter(Collider other)
    {   
        if(other.CompareTag("Fruit")){
            Destroy(other.gameObject);
            movement.AddBodyPart();
            GameplayController.instance.IncrementScore();
            AudioManager.instance.PlayPickupSound();
        } else if (other.CompareTag("Wall") || other.CompareTag("Bomb") || other.CompareTag("Tail"))
            GameOver.instance.Setup();
        Debug.Log(other.name);
    }
}
