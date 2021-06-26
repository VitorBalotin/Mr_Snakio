using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameplayController : MonoBehaviour{
    public static GameplayController instance;
    // pickups
    public GameObject fruit_pickup, bomb_pickup;
    // position x and y from the map
    public Transform objeto1, objeto2;
    public float posY;
    private float minX,minZ,maxX,maxZ;
    // Text and counter for the score
    private Text score_text;
    private int score_count;
    public void Awake(){
        MakeInstance();
    }
    
    public void Start(){
        score_text = GameObject.Find("Score").GetComponent<Text>();
        Invoke(nameof(StartSpawning), 0.5f);
        if (objeto1.position.x > objeto2.position.x)
        {
            minX = objeto2.position.x;
            maxX = objeto1.position.x;
        }
        else
        {
            minX = objeto1.position.x;
            maxX = objeto2.position.x;
        }
        
        if (objeto1.position.z > objeto2.position.z)
        {
            minZ = objeto2.position.z;
            maxZ = objeto1.position.z;
        }
        else
        {
            minZ = objeto1.position.z;
            maxZ = objeto2.position.z;
        }
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
        CancelInvoke(nameof(StartSpawning));
    }

    IEnumerator SpawnPickUps(){
        Boolean validSpawn = false;
        GameObject nextSpawnable;
        Vector3 coord;
        
        if (Random.Range(0, 10) >= 2) {
            nextSpawnable = fruit_pickup;
        }
        else {
            nextSpawnable = bomb_pickup;
        }

        yield return new WaitForSeconds(Random.Range(1f, 1.5f));
        do {
            coord = new Vector3(Random.Range(minX, maxX), posY,Random.Range(minZ, maxZ));
            validSpawn = Physics.CheckSphere(coord, nextSpawnable.GetComponent<SphereCollider>().radius);
        } while (!validSpawn);
            
        Instantiate(nextSpawnable, coord, Quaternion.identity);
        
        Invoke(nameof(StartSpawning), 0f);
    }

    // Increments the score
    public void IncrementScore(){
        score_count++;
        score_text.text = "Score: " + score_count;
    }
}
