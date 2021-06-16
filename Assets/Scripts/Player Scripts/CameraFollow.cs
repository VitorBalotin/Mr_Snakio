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

    private void LateUpdate() {
        if (offsetPositionSpace == Space.Self) {
            transform.position = tail.TransformPoint(offsetPosition);
        }
        else {
            transform.position = tail.position + offsetPosition;
        }
        transform.LookAt(head);
    }
}