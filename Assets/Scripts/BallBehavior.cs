using UnityEngine;

public class BallBehavior : MonoBehaviour {
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float minSpeed;
    public float maxSpeed;
    public int secondsToMaxSpeed;
    public Vector2 targetPosition;

    public GameObject target;
    Rigidbody2D body;

    public bool rerouting;
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
        //minX = -9.18f;
        //maxX = 9.18f;
        //minY = -4.4f;
        //maxY = 4.4f;

        minX = -4.4f;
        maxX = 4.44f;
        minY = -4.06f;
        maxY = 4.06f;

        cooldown = 2;

        targetPosition = getRandomPosition();
        initialPosition();
    }

    public void initialPosition() {
        body = GetComponent<Rigidbody2D>();
        body.position = getRandomPosition();
        targetPosition = getRandomPosition();
        launching = false;
        rerouting = true;
    }

    // Update is called once per frame
    void FixedUpdate() {
        Vector2 currentPos = body.position;
        float distance = Vector2.Distance(currentPos, targetPosition);

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
            body.MovePosition(newPos);
        }
        else {
            if (launching) {
                startCooldown();
            }
            else {
                targetPosition = getRandomPosition();
            }
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
        Rigidbody2D targetBody = target.GetComponent<Rigidbody2D>();
        targetPosition = targetBody.position;
        if (!launching) {
            timeLaunchStart = Time.time;
            launching = true;
        }
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

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Wall") {
            targetPosition = getRandomPosition();
        }
        if (collision.gameObject.tag == "Ball") {
            reroute(collision);
        }
        Debug.Log(this + " collided with: " + collision.gameObject.name);
    }

    public void reroute(Collision2D collision) {
        GameObject otherBall = collision.gameObject;
        if (rerouting) {
            otherBall.GetComponent<BallBehavior>().rerouting = false;
            Rigidbody2D ballBody = otherBall.GetComponent<Rigidbody2D>();
            Vector2 contact = collision.GetContact(0).normal;
            targetPosition = Vector2.Reflect(targetPosition, contact).normalized;
            launching = false;
            float separationDistance = 0.1f;
            ballBody.position += contact * separationDistance;
        }
        else {
            rerouting = true;
        }
    }
}
