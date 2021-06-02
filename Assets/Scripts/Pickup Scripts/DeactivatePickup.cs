using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivatePickup : MonoBehaviour{
    // Start is called before the first frame update
    // Deactivates the pickup or bomb after 3 or 6 seconds
    void Start(){
        Invoke("Deactivate", Random.Range(3f, 6f));
    }

    void Deactivate(){
        gameObject.SetActive(false);
    }
}
