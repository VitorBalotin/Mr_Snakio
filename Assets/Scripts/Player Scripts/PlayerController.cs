using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    [HideInInspector]
    public PlayerDirection direction;

    [HideInInspector]
    public float step_length = 0.2f;

    [HideInInspector]
    public float movement_frequency = 0.1f;
    private float counter;
    private bool move;

    [SerializeField]
    private GameObject tailPrefab;

    private List<Vector3> delta_position;

    private List<Rigidbody> nodes;

    private Rigidbody main_body;
    private Rigidbody head_body;
    private Transform tr;

    private bool create_node;

    void Awake(){
        tr = transform;
        main_body = GetComponent<Rigidbody>();

        InitSnakeNodes();
        InitPlayer();

        delta_position = new List<Vector3>(){
            new Vector3(-step_length, 0f), // -dx LEFT
            new Vector3(0f, step_length),  // -dy UP
            new Vector3(step_length, 0f),  // dx RIGHT
            new Vector3(0f, -step_length) // -dy DOWN
        };
    }

    // Update is called once per frame
    void Update(){
        
    }

    void InitSnakeNodes(){
        nodes = new List<Rigidbody>();
        nodes.Add(tr.GetChild(0).GetComponent<Rigidbody>());
        nodes.Add(tr.GetChild(1).GetComponent<Rigidbody>());
        nodes.Add(tr.GetChild(2).GetComponent<Rigidbody>());
        head_body = nodes[0];
    }

    void SetDirectionRandom(){
        int dirRandom = Random.Range(0, (int)PlayerDirection.COUNT);
        direction = (PlayerDirection)dirRandom;
    }

    void InitPlayer(){
        SetDirectionRandom();
        switch (direction){
            case PlayerDirection.RIGHT:
                nodes[1].position = nodes[0].position - new Vector3(Metrics.NODE, 0f, 0f);
                nodes[2].position = nodes[0].position - new Vector3(Metrics.NODE * 2f, 0f, 0f);
                break;
            case PlayerDirection.LEFT:
                nodes[1].position = nodes[0].position + new Vector3(Metrics.NODE, 0f, 0f);
                nodes[2].position = nodes[0].position + new Vector3(Metrics.NODE * 2f, 0f, 0f);
                break;
            case PlayerDirection.UP:
                nodes[1].position = nodes[0].position - new Vector3(0f, Metrics.NODE, 0f);
                nodes[2].position = nodes[0].position - new Vector3(0f, Metrics.NODE * 2f, 0f);
                break;
            case PlayerDirection.DOWN:
                nodes[1].position = nodes[0].position + new Vector3(0f, Metrics.NODE, 0f);
                nodes[2].position = nodes[0].position + new Vector3(0f, Metrics.NODE * 2f, 0f);
                break;
            default:
                break;
        }
    }

    void Move(){
        Vector3 dPosition = delta_position[(int)direction];
        Vector3 parentPosition = head_body.position;
        Vector3 previousPosition;
        main_body.position = main_body.position + dPosition;
        head_body.position = head_body.position + dPosition;

        for(int i = 1; i < nodes.Count; i++){
            previousPosition = nodes[i].position;
            nodes[i].position = parentPosition;
            parentPosition = previousPosition;
        }
        // Validate if it's necessary to create a new node
        // in case a fruit was eaten
        if(create_node){

        }
    }

    void CheckMovementFrequncy(){
        counter += Time.deltaTime;
        if(counter >= movement_frequency){
            counter = 0f;
            move = true;
        }
    }
}
