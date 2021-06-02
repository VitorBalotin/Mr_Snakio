using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour{
    public static GameplayController instance;
    // pickups
    public GameObject fruit_pickup, bomb_pickup;
    // position x and y from the map
    private float min_x = -4.25f, max_x = 4.25f, min_y= -2.26f, max_y = 2.26f;
    // z position of the middle of the map
    private float z_pos = 5.8f;
    // Text and counter for the score
    private Text score_text;
    private int score_count;

    void Awake(){
        MakeInstance();
    }
    
    void Start(){
        score_text = GameObject.Find("Score").GetComponent<Text>();
        Invoke("StartSpawning", 0.5f);
    }

    void MakeInstance(){
        if(instance == null){
            instance = this;
        }
    }

    // Routines to spawn pickup or bomb
    void StartSpawning(){
        StartCoroutine(SpawnPickUps());
    }

    // Cancels the spawn routine
    public void CancelSpawning(){
        CancelInvoke("StartSpawning");
    }

    IEnumerator SpawnPickUps(){
        yield return new WaitForSeconds(Random.Range(1f, 1.5f));
        if(Random.Range(0, 10) >= 2){
            Instantiate(fruit_pickup, new Vector3(Random.Range(min_x, max_x), Random.Range(min_y, max_y), z_pos), Quaternion.identity);
        }else{
            Instantiate(bomb_pickup, new Vector3(Random.Range(min_x, max_x), Random.Range(min_y, max_y), z_pos), Quaternion.identity);
        }

        Invoke("StartSpawning", 0f);
    }

    // Increments the score
    public void IncrementScore(){
        score_count++;
        score_text.text = "Score: " + score_count;
    }
}
