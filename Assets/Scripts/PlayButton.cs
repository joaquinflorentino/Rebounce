using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    private GameObject gameCanvas;
    private GameObject player;
    private GameObject spawner;
    private static bool hasStarted;

    [SerializeField]
    private GameObject menuCanvas;

    public void Play() {
        hasStarted = true;
        SceneManager.LoadScene("Game");
        gameCanvas.SetActive(true);
        player.SetActive(true);
        spawner.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!hasStarted) {
            gameCanvas = GameObject.Find("GameCanvas");
            player = GameObject.Find("Player");
            spawner = GameObject.Find("Spawner");
            spawner.SetActive(false);
            gameCanvas.SetActive(false);
            menuCanvas.SetActive(true);
            player.SetActive(false);
        }
        else {
            menuCanvas.SetActive(false);
        }
    }
}
