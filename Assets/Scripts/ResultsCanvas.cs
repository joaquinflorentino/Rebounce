using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultsCanvas : MonoBehaviour
{
    private ScoreResults scoreResultsScript;
    private HighscoreResults highscoreResultsScript;

    [SerializeField]
    private GameObject scoreResultsObject;
    [SerializeField]
    private GameObject highscoreResultsObject;

    public void Display() {
        scoreResultsScript.SetScoreResults();
        highscoreResultsScript.SetHighscoreResults();
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart() {
        SceneManager.LoadScene("Game");
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreResultsScript = scoreResultsObject.GetComponent<ScoreResults>();
        highscoreResultsScript = highscoreResultsObject.GetComponent<HighscoreResults>();
        gameObject.SetActive(false);
    }
}
