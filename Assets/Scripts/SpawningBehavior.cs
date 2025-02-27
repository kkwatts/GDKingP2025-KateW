using UnityEngine;

public class SpawningBehavior : MonoBehaviour {
    public GameObject[] ballVariants;
    public GameObject targetObject;
    public Pins pinsDB;
    GameObject newObject;

    public float startTime;
    public float spawnRatio = 1.0f;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        SpawnPin();
        SpawnBall();
    }

    // Update is called once per frame
    void Update() {
        float currentTime = Time.time;
        float timeElapsed = currentTime - startTime;
        if (timeElapsed > spawnRatio) {
            SpawnBall();
            spawnRatio = Random.Range(2f, 4.5f);
        }
    }

    void SpawnBall() {
        int numVariants = ballVariants.Length;
        if (numVariants > 0) {
            int selection = Random.Range(0, numVariants);
            bool ready = false;
            Vector3 position = new Vector3(0f, 0f, 0f);

            while (!ready) {
                position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
                if (Mathf.Abs(position.x - targetObject.transform.position.x) > 1.55f && Mathf.Abs(position.y - targetObject.transform.position.y) > 2f) {
                    ready = true;
                }
            }

            newObject = Instantiate(ballVariants[selection], position, Quaternion.identity);

            BallBehavior ballBehavior = newObject.GetComponent<BallBehavior>();
            ballBehavior.SetBounds(minX, maxX, minY, maxY);
            ballBehavior.SetTarget(targetObject);
        }
        startTime = Time.time;
    }

    void SpawnPin() {
        targetObject = Instantiate(pinsDB.GetPin(CharacterManager.selection).prefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
    }
}
