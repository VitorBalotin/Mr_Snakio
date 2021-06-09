using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    private Transform head;
    
    [SerializeField]
    private Transform tail;

    [SerializeField]
    private Vector3 offsetPosition;

    [SerializeField]
    private Space offsetPositionSpace = Space.Self;

    [SerializeField]
    private bool lookAt = true;

    private void LateUpdate()
    {
        // compute position
        if (offsetPositionSpace == Space.Self)
        {
            transform.position = tail.TransformPoint(offsetPosition);
        }
        else
        {
            transform.position = tail.position + offsetPosition;
        }

        // compute rotation
        if (lookAt)
        {
            transform.LookAt(head);
        }
        else
        {
            transform.rotation = tail.rotation;
        }
    }
}