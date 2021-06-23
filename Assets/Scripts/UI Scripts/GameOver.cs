using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour{
    public static GameOver instance;
    public GameObject deathScreen;

     public void Start(){
        MakeInstance();
         deathScreen.SetActive(false);

    }
     void MakeInstance(){
        if(instance == null){
            instance = this;
        }
    }
    public void Setup(){
        deathScreen.SetActive(true);
    }

    public void Restart(){
        SceneManager.LoadScene("SampleScene");
    }

    public void ReturnToMenu(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    
}
