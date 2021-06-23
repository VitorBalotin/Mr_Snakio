using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{

    public List<Transform> bodyParts = new List<Transform>();
    public GameObject bodyPrefab;
    public float minDistance = 0.25f;
    public int beginSize;

    public float speed = 1;
    public float speedModifier = 2;
    public float rotationSpeed = 50;
    public float rotationSpeedModifier = 2;
    
    private float distance;
    private Transform curBodyPart;
    private Transform prevBodyPart;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < beginSize-1; i++)
        {
            addBodyPart();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        float curSpeed = speed;
        float curRotationSpeed = rotationSpeed;
        
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.W))
        {
            curSpeed *= speedModifier;
            curRotationSpeed *= rotationSpeedModifier;
        }
        
        Transform head = bodyParts[0];
        head.Translate(head.forward * (curSpeed * Time.smoothDeltaTime),Space.World);

        if(Input.GetAxis("Horizontal") != 0)
            head.Rotate(Vector3.up * (curRotationSpeed * Time.deltaTime * Input.GetAxis("Horizontal")));

        if (Input.GetKey(KeyCode.LeftShift))
            curSpeed *= 2;
        
        for (int i = 1; i < bodyParts.Count; i++)
        {
            curBodyPart = bodyParts[i];
            prevBodyPart = bodyParts[i - 1];

            distance = Vector3.Distance(prevBodyPart.position, curBodyPart.position);

            Vector3 newPos = prevBodyPart.position;

            newPos.y = head.position.y;

            float T = Time.deltaTime * distance / minDistance*curSpeed;

            if (T > 0.5f)
                T = 0.5f;
            curBodyPart.position = Vector3.Slerp(curBodyPart.position, newPos, T);
            curBodyPart.rotation = Quaternion.Slerp(curBodyPart.rotation, prevBodyPart.rotation, T);
            
        }
    }

    public void addBodyPart()
    {
        Transform tail = bodyParts[bodyParts.Count-1];
        
        Transform newPart = (Instantiate(bodyPrefab, tail.position, tail.rotation) as GameObject).transform;
        
        newPart.SetParent(transform);
        
        bodyParts.Add(newPart);
    }
}
