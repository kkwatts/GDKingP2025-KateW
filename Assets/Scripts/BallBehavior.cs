using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float minSpeed;
    public float maxSpeed;
    Vector2 targetPosition;
    public int secondsToMaxSpeed;
    public GameObject target;
    public float minLaunchSpeed;
    public float maxLaunchSpeed;
    public float minTimeToLaunch;
    public float maxTimeToLaunch;
    public float cooldown;
    public bool launching;
    public float launchDuration;
    public float timeLastLaunch;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        minX = -9.18f;
        maxX = 9.18f;
        minY = -4.4f;
        maxY = 4.4f;

        //secondsToMaxSpeed = 30;
        //minSpeed = 0.75f;
        //maxSpeed = 2.0f;
        targetPosition = getRandomPosition();
    }

    // Update is called once per frame
    void Update() {
        Vector2 currentPos = transform.position;
        float distance = Vector2.Distance(currentPos, targetPosition);

        if (distance > 0.1f) {
            float difficulty = getDifficultyPercentage();
            float currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, difficulty);
            currentSpeed *= Time.deltaTime;

            Vector2 newPos = Vector2.MoveTowards(currentPos, targetPosition, currentSpeed);
            transform.position = newPos;
        }
        else {
            targetPosition = getRandomPosition();
        }

        getRandomPosition();
    }

    Vector2 getRandomPosition() {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        Vector2 v = new Vector2(randomX, randomY);

        return v;
    }

    public float getDifficultyPercentage() {
        float difficulty = Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxSpeed);
        return difficulty;
    }

    public void launch() {
        
    }
}
