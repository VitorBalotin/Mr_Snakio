using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    // Direction that the snake is moving
    [HideInInspector]
    public PlayerDirection direction;

    // distance that the snake moves
    [HideInInspector]
    public float step_length = 0.2f;

    // how many times it can move
    [HideInInspector]
    public float movement_frequency = 0.1f;
    // a counter to check if it can move based on the frequency
    private float counter;
    // bool to allow the snake to move
    private bool move;

    // Contains the tail prefab, will be used after the snake eats
    [SerializeField]
    private GameObject tail_prefab;

    // Controls which way the snake moves, left, right, up or down
    private List<Vector3> delta_position;

    // Has the parts of the snake, head, node and tails
    private List<Rigidbody> nodes;

    // Main body asset, snake
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
            new Vector3(-step_length, 0f),   // -x LEFT
            new Vector3(0f, 0f, step_length), // z UP
            new Vector3(step_length, 0f),     // x RIGHT
            new Vector3(0f, 0f, -step_length)    // -z DOWN
        };
    }

    // Update is called once per frame
    void Update(){
        CheckMovementFrequency();
    }

    void FixedUpdate(){
        if(move){
            move = false;
            Move();
        }
    }

    // Sets the nodes var with all the snake nodes avaliable
    void InitSnakeNodes(){
        nodes = new List<Rigidbody>();
        nodes.Add(tr.GetChild(0).GetComponent<Rigidbody>());
        nodes.Add(tr.GetChild(1).GetComponent<Rigidbody>());
        nodes.Add(tr.GetChild(2).GetComponent<Rigidbody>());
        head_body = nodes[0];
    }

    // Initializes the direction that the snake will move
    void SetDirectionRandom(){
        int dir_random = Random.Range(0, (int)PlayerDirection.COUNT);
        direction = (PlayerDirection)dir_random;
    }

    void InitPlayer(){
        SetDirectionRandom();
        // After it got the initial direction, it changes the snake direction
        switch (direction){
            case PlayerDirection.EAST:
                nodes[1].position = nodes[0].position - new Vector3(Metrics.NODE, 0f, 0f);
                nodes[2].position = nodes[0].position - new Vector3(Metrics.NODE * 2f, 0f, 0f);
                break;
            case PlayerDirection.WEST:
                nodes[1].position = nodes[0].position + new Vector3(Metrics.NODE, 0f, 0f);
                nodes[2].position = nodes[0].position + new Vector3(Metrics.NODE * 2f, 0f, 0f);
                break;
            case PlayerDirection.NORTH:
                nodes[1].position = nodes[0].position - new Vector3(0f, Metrics.NODE, 0f);
                nodes[2].position = nodes[0].position - new Vector3(0f, Metrics.NODE * 2f, 0f);
                break;
            case PlayerDirection.SOUTH:
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
        head_body.rotation = Quaternion.Euler(0f, (float)90*(int)direction, 0f);
        
        for(int i = 1; i < nodes.Count; i++){
            previousPosition = nodes[i].position;
            nodes[i].rotation = Quaternion.Euler(0f, (float)90*(int)direction, 0f);
            nodes[i].position = parentPosition;
            parentPosition = previousPosition;
        }
        // Validate if it's necessary to create a new node
        // in case a fruit was eaten
        if(create_node){
            create_node = false;
            // Creates the new node at the end of the snake, sets the parent and add it to the snake.
            GameObject node = Instantiate(tail_prefab, nodes[nodes.Count -1].position, Quaternion.identity);
            node.transform.SetParent(transform, true);
            nodes.Add(node.GetComponent<Rigidbody>());
        }
    }

    void CheckMovementFrequency(){
        counter += Time.deltaTime;
        if(counter >= movement_frequency){
            counter = 0f;
            move = true;
        }
    }

    public void SetInputDirection(PlayerDirection dir){
        // Validates if the direction choosen is allowed
        if(((dir == PlayerDirection.NORTH && direction == PlayerDirection.SOUTH) ||
           (dir == PlayerDirection.SOUTH && direction == PlayerDirection.NORTH) ||
           (dir == PlayerDirection.EAST && direction == PlayerDirection.WEST) ||
           (dir == PlayerDirection.WEST && direction == PlayerDirection.EAST)))
        {
            return;
        }

        direction = dir;
        // Forces the snake to move
        ForceMove();
    }

    void ForceMove(){
        counter = 0;
        move = false;
        Move();
    }

    void OnTriggerEnter(Collider target){
        if(target.tag == Tags.FRUIT){
            target.gameObject.SetActive(false);
            create_node = true;
            GameplayController.instance.IncrementScore();
            AudioManager.instance.PlayPickupSound();
        }

        if(target.tag == Tags.WALL || target.tag == Tags.BOMB || target.tag == Tags.TAIL){
            Time.timeScale = 0f;
            AudioManager.instance.PlayDeathSound();
        }
    }
}
