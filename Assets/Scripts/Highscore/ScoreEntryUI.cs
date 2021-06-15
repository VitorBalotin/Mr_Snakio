using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreEntryUI : MonoBehaviour{
    private Text entry_name_text;
    private Text entry_score_text;

    void Start() {
        entry_name_text = GameObject.Find("Text_Name").GetComponent<Text>();
        entry_score_text = GameObject.Find("Text_Score").GetComponent<Text>();
    }

    public void Init(ScoreEntryData scoreboard_entry_data){
        entry_name_text.text = scoreboard_entry_data.entry_name;
        entry_score_text.text = scoreboard_entry_data.entry_score.ToString();
    }
}
