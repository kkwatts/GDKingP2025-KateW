using UnityEngine;
using System.Collections;

public class PinBehavior : MonoBehaviour {
    public float dashSpeed = 5.0f;
    public float baseSpeed = 2.0f;
    public float speed;

    public bool dashing;
    public bool invincible;

    public float dashDuration;
    public float timeDashStart;
    public float invinDuration;
    public float timeInvinStart;

    public static float dashCooldownRate = 1.0f;
    public static float dashCooldown;
    public static float invinCooldownRate = 5.0f;
    public static float invinCooldown;

    public float timeLastDashEnded;
    public float timeLastInvinEnded;

    public Vector2 newPosition;
    public Vector3 mousePosG;

    public AudioSource[] audioSources;

    Camera cam;
    Rigidbody2D body;
    SpriteRenderer render;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        speed = baseSpeed;
        cam = Camera.main;
        body = GetComponent<Rigidbody2D>();
        dashing = false;
        invincible = false;
        render = GetComponent<SpriteRenderer>();
        render.color = new Color(render.color.r, render.color.g, render.color.b, 1.0f);
        audioSources = GetComponents<AudioSource>();
    }

    void Update() {
        Dash();
        Invincibility();
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
        if ((collided == "Ball" || collided == "Wall") && !invincible) {
            audioSources[0].Play();
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
            StartCoroutine(WaitForSoundAndTransition());
        }
    }

    private void Dash() {
        if (dashing) {
            float howLong = Time.time - timeDashStart;
            if (howLong > dashDuration) {
                dashing = false;
                speed = baseSpeed;
                timeLastDashEnded = Time.time;
                dashCooldown = dashCooldownRate;
            }
        }
        else {
            dashCooldown -= Time.deltaTime;
            if (Input.GetMouseButtonDown(0) && dashCooldown <= 0) {
                dashCooldown = 0;
                dashing = true;
                speed = dashSpeed;
                timeDashStart = Time.time;
                if (audioSources[1].isPlaying) {
                    audioSources[1].Stop();
                }
                audioSources[1].Play();
            }
        }
    }

    private void Invincibility() {
        if (invincible) {
            float howLong = Time.time - timeInvinStart;
            if (howLong > invinDuration) {
                invincible = false;
                timeLastInvinEnded = Time.time;
                invinCooldown = invinCooldownRate;
                render.color = new Color(render.color.r, render.color.g, render.color.b, 1.0f);
            }
        }
        else {
            invinCooldown -= Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space) && invinCooldown <= 0) {
                invinCooldown = 0;
                invincible = true;
                timeInvinStart = Time.time;
                render.color = new Color(render.color.r, render.color.g, render.color.b, 0.5f);
            }
        }
    }

    private IEnumerator WaitForSoundAndTransition() {
        yield return new WaitForSeconds(audioSources[0].clip.length);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }
}