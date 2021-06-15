using UnityEngine;
using System.IO;

public class Scoreboard : MonoBehaviour{
    [SerializeField] private int max_scoreboard_entries = 5;
    [SerializeField] private Transform highscore_holder_transform = null;
    [SerializeField] private GameObject scoreboard_entry_obj = null;

    [Header("Test")]
    [SerializeField] ScoreEntryData test_entry_data = new ScoreEntryData();

    private string save_path => $"{Application.persistentDataPath}/highscore.json";

    private void Start() {
        ScoreboardSaveData saved_scores = GetSavedScores();
        UpdateUI(saved_scores);
        SaveScores(saved_scores);
    }

    [ContextMenu("Add Test Entry")]
    public void AddTestEntry(){
        AddEntry(test_entry_data);
    }

    public void AddEntry(ScoreEntryData score_entry_data){
        ScoreboardSaveData saved_scores = GetSavedScores();
        bool score_added = false;
        // Adds the score if the value is higher than the ones in the list
        for(int i = 0; i < saved_scores.highscores.Count; i++){
            if(score_entry_data.entry_score > saved_scores.highscores[i].entry_score){
                saved_scores.highscores.Insert(i, score_entry_data);
                score_added = false;
                break;
            }
        }
        if(!score_added && saved_scores.highscores.Count < max_scoreboard_entries){
            saved_scores.highscores.Add(score_entry_data);
        }
        if(saved_scores.highscores.Count > max_scoreboard_entries){
            saved_scores.highscores.RemoveRange(max_scoreboard_entries, saved_scores.highscores.Count - max_scoreboard_entries);
        }
        UpdateUI(saved_scores);
        SaveScores(saved_scores);
    }

    private void UpdateUI(ScoreboardSaveData saved_scores){
        foreach(Transform child in highscore_holder_transform){
            Destroy(child.gameObject);
        }

        foreach(ScoreEntryData highscore in saved_scores.highscores){
            Instantiate(scoreboard_entry_obj, highscore_holder_transform).GetComponent<ScoreEntryUI>().Init(highscore);
        }
    }

    private ScoreboardSaveData GetSavedScores(){
        if(!File.Exists(save_path)){
            File.Create(save_path).Dispose();
            return new ScoreboardSaveData();
        }

        using(StreamReader stream = new StreamReader(save_path)){
            string json = stream.ReadToEnd();

            return JsonUtility.FromJson<ScoreboardSaveData>(json);
        }
    }

    private void SaveScores(ScoreboardSaveData scoreboard_save_data){
        using(StreamWriter stream = new StreamWriter(save_path)){
            string json = JsonUtility.ToJson(scoreboard_save_data, true);
            stream.Write(json);
        }
    }
}
