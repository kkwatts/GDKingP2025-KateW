using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float minSpeed;
    public float maxSpeed;
    public int secondsToMaxSpeed;
    Vector2 targetPosition;

    public GameObject target;

    public float minLaunchSpeed;
    public float maxLaunchSpeed;
    public float minTimeToLaunch;
    public float maxTimeToLaunch;
    public float cooldown;
    public bool launching;
    public float launchDuration;
    public float timeLastLaunch;
    public float timeLaunchStart;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        minX = -9.18f;
        maxX = 9.18f;
        minY = -4.4f;
        maxY = 4.4f;

        cooldown = 2;

        //secondsToMaxSpeed = 30;
        //minSpeed = 0.75f;
        //maxSpeed = 2.0f;
        targetPosition = getRandomPosition();
    }

    // Update is called once per frame
    void Update() {
        Vector2 currentPos = transform.position;
        float distance = Vector2.Distance(currentPos, targetPosition);
        Debug.Log(targetPosition);

        if (!onCooldown()) {
            if (launching) {
                float currentLaunchTime = Time.time - timeLaunchStart;
                if (currentLaunchTime > launchDuration) {
                    startCooldown();
                }
            }
            else {
                launch();
            }
        }
        
        if (distance > 0.1f) {
            float difficulty = getDifficultyPercentage();
            float currentSpeed;

            if (launching) {
                float launchingForHowLong = Time.time - timeLaunchStart;

                if (launchingForHowLong > launchDuration) {
                    startCooldown();
                }

                currentSpeed = Mathf.Lerp(minLaunchSpeed, maxLaunchSpeed, difficulty);
            }
            else {
                currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, difficulty);
            }

            currentSpeed *= Time.deltaTime;

            Vector2 newPos = Vector2.MoveTowards(currentPos, targetPosition, currentSpeed);
            transform.position = newPos;
        }
        else {
            if (launching)
            {
                startCooldown();
            }
            else
            {
                targetPosition = getRandomPosition();
            }
        }

        //float timeLaunching = Time.time - timeLastLaunch;
        //if (timeLaunching > launchDuration) {
        //    startCooldown();
        //}
        //else {
        //    launch();
        //}

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
        targetPosition = target.transform.position;
        if (!launching) {
            timeLaunchStart = Time.time;
            launching = true;
        }

        //cooldown = Random.Range(0.5f, 3.5f);
    }

    public bool onCooldown() {
        bool result = false;
        float timeSinceLastLaunch = Time.time - timeLastLaunch;

        if (timeSinceLastLaunch < cooldown) {
            result = true;
        }

        return result;
    }

    public void startCooldown() {
        timeLastLaunch = Time.time;
        launching = false;
    }
}
