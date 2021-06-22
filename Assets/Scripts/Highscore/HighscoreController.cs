using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HighscoreController : MonoBehaviour{
    public static HighscoreController instance;
    private string save_path => $"{Application.persistentDataPath}/highscore.json";
    public struct ScoreInfo{
        string position;
        int score;
    }
    private void Start() {
        MakeInstance();
    }
    void MakeInstance(){
        if(instance == null){
            instance = this;
        }
        Debug.Log("entrou");
        Debug.Log(GetSavedScores());
    }
    public List<ScoreInfo> highscores = new List<ScoreInfo>();
    public void SetScore(int score){
        
    }
    public List<ScoreInfo> GetSavedScores(){
        if(!File.Exists(save_path)){
            File.Create(save_path).Dispose();
            return new List<ScoreInfo>();
        }

        using(StreamReader stream = new StreamReader(save_path)){
            string json = stream.ReadToEnd();
            highscores = JsonUtility.FromJson<ScoreInfo>(json);
            return highscores;
        }
    }
    private void SaveScores(ScoreInfo scoreboard_save_data){
        using(StreamWriter stream = new StreamWriter(save_path)){
            string json = JsonUtility.ToJson(scoreboard_save_data, true);
            stream.Write(json);
        }
    }
}
