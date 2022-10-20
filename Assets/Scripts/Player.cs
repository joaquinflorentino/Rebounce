using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float force = 20f;
    private float numberOfTargetsCleared;
    private bool hasCollidedWithWall;
    private float threshold = 2.2f;
    private float combo;
    private Timer timerScript;
    private bool isFollowingMouse;
    private bool isLaunched;
    private bool hasStartedLaunch;
    private bool isStopped;
    private ResultsCanvas resultsCanvasScript;
    private float startTime;
    private float forceMultiplier;
    private GameObject arrow;
    private SpriteRenderer arrowSpriteRenderer;
    private float boostTimeThreshold = 0.8f;
    private AudioSource audioSource;

    [SerializeField]
    private GameObject resultsCanvasObject;
    [SerializeField]
    private Sprite arrowSprite;
    [SerializeField]
    private Sprite doubleArrowSprite;
    [SerializeField]
    private AudioClip wallHitSound;
    [SerializeField]
    private AudioClip targetHitSound;

    public bool IsSlowerThanThreshold() {
        return rb.velocity.magnitude < threshold;
    }

    public float GetNumberOfTargetsCleared() {
        return numberOfTargetsCleared;
    }

    public void ResetNumberOfTargetsCleared() {
        numberOfTargetsCleared = 0;
    }

    private void OnTriggerEnter2D(Collider2D coll) {
        if (coll.CompareTag("Wall")) {
            hasCollidedWithWall = true;
            audioSource.PlayOneShot(wallHitSound);
        }
        else if (coll.CompareTag("Target")) {
            if (!hasCollidedWithWall && !isFollowingMouse) {
                Time.timeScale = 0f;
                resultsCanvasScript.Display();
            }
            else {
                Destroy(coll.gameObject);
                numberOfTargetsCleared++;
                GameObject.Find("Score").GetComponent<ScoreUI>().AddScore(1);
                combo++;
                audioSource.PlayOneShot(targetHitSound);

                if (combo > 1) {
                    float timeToAdd = combo + Mathf.Floor(combo / 2);
                    timerScript.AddTime(Mathf.Clamp(timeToAdd, 0, timerScript.GetMaximumTime() - timerScript.GetCurrentTime()));
                }
            }
        }
    }

    private void LookTowardMouse() {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10f);
        float angle = AngleBetweenTwoPoints(transform.position, mousePosition);

        if (Vector3.Distance(mousePosition, transform.position) > 1) {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        }
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        rb = GetComponent<Rigidbody2D>();
        timerScript = GameObject.Find("Timer").GetComponent<Timer>();
        resultsCanvasScript = resultsCanvasObject.GetComponent<ResultsCanvas>();
        arrow = GameObject.Find("Arrow");
        arrowSpriteRenderer = arrow.GetComponent<SpriteRenderer>();
        arrowSpriteRenderer.sprite = arrowSprite;
        audioSource = GetComponent<AudioSource>();
        arrow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsSlowerThanThreshold()) {
            hasCollidedWithWall = false;
            isLaunched = false;
            combo = 0;
            if (!isLaunched && !isStopped && Time.timeScale == 1f) {
                GetComponent<Collider2D>().enabled = false;
                Vector3 mousePosition = Input.mousePosition;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                transform.position = Vector2.Lerp(transform.position, mousePosition, 0.1f);
                transform.position = new Vector2(Mathf.Clamp(transform.position.x, -7.5f, 7.5f), Mathf.Clamp(transform.position.y, -3.5f, 3.5f));
                isFollowingMouse = true;
            }

            if (Input.GetMouseButtonDown(0)) {
                if (!isFollowingMouse && isStopped) {
                    forceMultiplier = 1f;
                    hasStartedLaunch = true;
                    startTime = Time.time;
                    GetComponent<Collider2D>().enabled = true;
                }
                else if (isFollowingMouse && !isStopped) {
                    rb.velocity = Vector2.zero;
                    isStopped = true;
                    isFollowingMouse = false;
                    arrow.SetActive(true);
                    arrowSpriteRenderer.sprite = arrowSprite;
                }
            }
            if (hasStartedLaunch && Time.time - startTime > boostTimeThreshold) {
                arrowSpriteRenderer.sprite = doubleArrowSprite;
            }
            if (Input.GetMouseButtonUp(0)) {
                if (!isFollowingMouse && isStopped && hasStartedLaunch) {
                    if (Time.time - startTime > boostTimeThreshold) {
                        forceMultiplier = 1.5f;
                        rb.drag = 1.7f;
                    }
                    Vector3 clickPosition = GameObject.Find("Main Camera").GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
                    Vector2 dir = clickPosition - transform.position;
                    dir = dir.normalized;
                    rb.velocity = Vector2.zero;
                    rb.AddForce(dir * (force * forceMultiplier), ForceMode2D.Impulse);
                    isLaunched = true;
                    isStopped = false;
                    hasStartedLaunch = false;
                    arrow.SetActive(false);
                }
            }
        }

        LookTowardMouse();
    }
}
