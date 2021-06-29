using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour{
    // Controls wether or not the game is paused
    public static bool game_paused = false;
    // UI Menu that shows when the game is paused
    public GameObject pause_menu_ui;

    // Start is called before the first frame update
    void Start(){
        pause_menu_ui.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0f) 
            return;
        
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(game_paused == true){
                Resume();
            }else{
                Pause();
            }
        }
    }

    public void Resume(){
        pause_menu_ui.SetActive(false);
        Time.timeScale = 1f;
        game_paused = false;
    }

    void Pause(){
        pause_menu_ui.SetActive(true);
        Time.timeScale = 0f;
        game_paused = true;
    }

    public void LoadMenu(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        // Debug.Log("Menu");
    }
}