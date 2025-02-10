using UnityEngine;

public class PinBehavior : MonoBehaviour {
    public float dashSpeed = 5.0f;
    public float baseSpeed = 2.0f;
    public bool dashing;
    public float speed;
    public float dashDuration;
    public float timeDashStart;
    public static float cooldownRate = 1.0f;
    public static float cooldown;
    public float timeLastDashEnded;

    public Vector2 newPosition;
    public Vector3 mousePosG;

    Camera cam;
    Rigidbody2D body;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        speed = baseSpeed;
        cam = Camera.main;
        body = GetComponent<Rigidbody2D>();
        dashing = false;
    }

    void Update() {
        Dash();
    }

    // Update is called once per frame
    void FixedUpdate() {
        mousePosG = cam.ScreenToWorldPoint(Input.mousePosition);
        newPosition = Vector2.MoveTowards(transform.position, mousePosG, speed * Time.fixedDeltaTime);
        body.MovePosition(newPosition);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        string collided = collision.gameObject.tag;
        Debug.Log("Collided with " + collided);
        if (collided == "Ball" || collided == "Wall") {
            Debug.Log("Game over");
        }
    }

    private void Dash() {
        if (dashing) {
            float howLong = Time.time - timeDashStart;
            if (howLong > dashDuration) {
                dashing = false;
                speed = baseSpeed;
                timeLastDashEnded = Time.time;
                cooldown = cooldownRate;
            }
        }
        else {
            cooldown -= Time.deltaTime;
            if (Input.GetMouseButtonDown(0) && cooldown <= 0) {
                cooldown = 0;
                dashing = true;
                speed = dashSpeed;
                timeDashStart = Time.time;
            }
        }
    }
}
