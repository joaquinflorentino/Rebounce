using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreResults : MonoBehaviour
{
    private float highscore;

    public void SetHighscoreResults() {
        string highscoreKey = GameObject.Find("Score").GetComponent<ScoreUI>().GetHighscoreKey();
        int highscore = PlayerPrefs.GetInt(highscoreKey);
        GetComponent<Text>().text = "Highscore: " + highscore.ToString();
    }
}
