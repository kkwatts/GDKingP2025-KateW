using UnityEngine;

public class SpawningBehavior : MonoBehaviour {
    public GameObject[] ballVariants;
    public GameObject targetObject;
    GameObject newObject;
    public float startTime;
    public float spawnRatio = 1.0f;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        SpawnBall();
    }

    void SpawnBall() {
        int numVariants = ballVariants.Length;
        if (numVariants > 0) {
            int selection = Random.Range(0, numVariants);
            newObject = Instantiate(ballVariants[selection], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            BallBehavior ballBehavior = newObject.GetComponent<BallBehavior>();
            ballBehavior.SetBounds(minX, maxX, minY, maxY);
            ballBehavior.InitialPosition();
        }
    }
}
