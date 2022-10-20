using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreResults : MonoBehaviour
{
    private float score;

    public void SetScoreResults() {
        score = GameObject.Find("Score").GetComponent<ScoreUI>().GetScore();
        GetComponent<Text>().text = "Score: " + score.ToString();
    }
}
