using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public float minX = -9.8f;
    public float maxX = 9.8f;
    public float minY = -4.99f;
    public float maxY = 4.99f;
    public float minSpeed;
    public float maxSpeed;
    Vector2 targetPosition;
    public int secondsToMaxSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        secondsToMaxSpeed = 30;
        minSpeed = 0.75f;
        maxSpeed = 2.0f;
        targetPosition = getRandomPosition();
    }

    // Update is called once per frame
    void Update() {
        Vector2 currentPos = transform.position;
        float distance = Vector2.Distance((Vector2)transform.position, targetPosition);

        if (distance > 0.1f) {
            float currentSpeed = minSpeed;
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
}
