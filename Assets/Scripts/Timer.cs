using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float currentTime;
    private float initialTime = 21f;
    private float maximumTime = 30f;
    private Text currentTimeText;
    private ResultsCanvas resultsCanvasScript;

    [SerializeField]
    private GameObject resultsCanvasObject;

    public float GetCurrentTime() {
        return currentTime;
    }

    public float GetInitialTime() {
        return initialTime;
    }

    public float GetMaximumTime() {
        return maximumTime;
    }

    public void AddTime(float seconds) {
        currentTime += seconds;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTime = initialTime;
        resultsCanvasScript = resultsCanvasObject.GetComponent<ResultsCanvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime > 0) {
            currentTime -= Time.deltaTime;
        }
        else {
            currentTime = 0;
            resultsCanvasScript.Display();
        }

        GetComponent<Text>().text = "Timer: " + Mathf.FloorToInt(currentTime).ToString();
    }
}
