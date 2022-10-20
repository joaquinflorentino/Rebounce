using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    private int score;
    private Text scoreText;
    private int highscore;
    private string highscoreKey = "Highscore";

    public int GetScore() {
        return score;
    }

    public void AddScore(int n) {
        score += n;
    }

    private void UpdateHighscore() {
        if (score > PlayerPrefs.GetInt(highscoreKey)) {
            highscore = score;
            PlayerPrefs.SetInt(highscoreKey, score);
            PlayerPrefs.Save();
        }
    }

    public string GetHighscoreKey() {
        return highscoreKey;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHighscore();
        GetComponent<Text>().text = "Score: " + score.ToString();
    }
}
