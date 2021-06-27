using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeactivatePickup : MonoBehaviour{
    [Min(0)]
    public float intervalMin = 0;
    [Min(0)]
    public float intervalMax = 0;
    
    void Start(){
        Invoke(nameof(Deactivate), Random.Range(intervalMin, intervalMax));
    }

    void Deactivate(){
        Destroy(gameObject);
    }
}
