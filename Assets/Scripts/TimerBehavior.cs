using TMPro;
using UnityEngine;

public class TimerBehavior : MonoBehaviour {
    private float timer;
    private TextMeshProUGUI textField;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        textField = GetComponent<TextMeshProUGUI>();
        if (textField != null) {
            Debug.Log("No TextMeshProUGUI component found.");
        }
    }
    
    // Update is called once per frame
    void Update() {
        timer = Time.time;

        int minutes = (int) timer / 60;
        int seconds = (int) timer % 60;
        string message = string.Format("Time: {0:00}:{1:00}", minutes, seconds);

        textField.text = message.ToString();
    }
}
