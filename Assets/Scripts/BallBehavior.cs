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
        //minX = -4.4f;
        //maxX = 4.44f;
        //minY = -4.06f;
        //maxY = 4.06f;

        cooldown = 2;

        targetPosition = GetRandomPosition();
        InitialPosition();
    }

    public void InitialPosition() {
        body = GetComponent<Rigidbody2D>();
        body.position = GetRandomPosition();
        targetPosition = GetRandomPosition();
        launching = false;
        rerouting = true;
    }

    // Update is called once per frame
    void FixedUpdate() {
        Vector2 currentPos = body.position;
        float distance = Vector2.Distance(currentPos, targetPosition);

        if (!OnCooldown()) {
            if (launching) {
                float currentLaunchTime = Time.time - timeLaunchStart;
                if (currentLaunchTime > launchDuration) {
                    StartCooldown();
                }
            }
            else {
                Launch();
            }
        }
        
        if (distance > 0.1f) {
            float difficulty = GetDifficultyPercentage();
            float currentSpeed;

            if (launching) {
                float launchingForHowLong = Time.time - timeLaunchStart;

                if (launchingForHowLong > launchDuration) {
                    StartCooldown();
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
                StartCooldown();
            }
            else {
                targetPosition = GetRandomPosition();
            }
        }

        GetRandomPosition();
    }

    Vector2 GetRandomPosition() {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        Vector2 v = new Vector2(randomX, randomY);

        return v;
    }

    public float GetDifficultyPercentage() {
        float difficulty = Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxSpeed);
        return difficulty;
    }

    public void Launch() {
        Rigidbody2D targetBody = target.GetComponent<Rigidbody2D>();
        targetPosition = targetBody.position;
        if (!launching) {
            timeLaunchStart = Time.time;
            launching = true;
        }
    }

    public bool OnCooldown() {
        bool result = false;
        float timeSinceLastLaunch = Time.time - timeLastLaunch;

        if (timeSinceLastLaunch < cooldown) {
            result = true;
        }

        return result;
    }

    public void StartCooldown() {
        timeLastLaunch = Time.time;
        launching = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Wall") {
            targetPosition = GetRandomPosition();
        }
        if (collision.gameObject.tag == "Ball") {
            Reroute(collision);
        }
        Debug.Log(this + " collided with: " + collision.gameObject.name);
    }

    public void Reroute(Collision2D collision) {
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

    public void SetBounds(float miX, float maX, float miY, float maY) {
        minX = miX;
        maxX = maX;
        minY = miY;
        maxY = maY;
    }
}
