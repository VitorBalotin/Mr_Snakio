using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedStartScript : MonoBehaviour
{
    public GameObject countDown;
    void Start(){
        StartCoroutine("StartDelay");
    }

    void Update(){
        
    }

    IEnumerator StartDelay(){
        Time.timeScale = 0;
        float pauseTime = Time.realtimeSinceStartup + 3f;
        while(Time.realtimeSinceStartup < pauseTime){
            yield return 0;
        }
        countDown.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
