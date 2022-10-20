using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Player playerScript;
    private int numberOfTargetsToSpawn;

    [SerializeField]
    private GameObject target;

    private void Spawn() {
        bool spawned = false;

        while (!spawned) {
            Vector3 spawnPoint = new Vector2(Random.Range(-7f, 7f), Random.Range(-3.3f, 3.3f));

            if (!FoundSpawnPoint(spawnPoint)) {
                continue;
            }
            else {
                GameObject newTarget = Instantiate(target, spawnPoint, Quaternion.identity);
                spawned = true;
            }
        }
    }

    private bool FoundSpawnPoint(Vector3 spawnPoint) {
        GameObject[] allGameObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (GameObject go in allGameObjects) {
            if (go.activeInHierarchy) {
                if ((spawnPoint - go.transform.position).magnitude < 2f) {
                    return false;
                }
            }
        }
        return true;
    }

    private void SpawnWave(int n) {
        for (int i = 0; i < n; i++) {
            Spawn();
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.GetNumberOfTargetsCleared() == numberOfTargetsToSpawn) {
            if (playerScript.IsSlowerThanThreshold()) {
                playerScript.ResetNumberOfTargetsCleared();
                numberOfTargetsToSpawn = (int)(Random.Range(2f, 6f));
                SpawnWave(numberOfTargetsToSpawn);
            }
        }
    }
}
