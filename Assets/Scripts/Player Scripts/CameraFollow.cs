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
    private bool lookAt = false;

    private void LateUpdate()
    {
        if (offsetPositionSpace == Space.Self) 
            transform.position = tail.TransformPoint(offsetPosition);
        else 
            transform.position = tail.position + offsetPosition;

        // compute rotation
        transform.LookAt(head);
    }

}