using UnityEngine;
using TMPro;

public class ScoreBehavior : MonoBehaviour {
    private TextMeshProUGUI textField;
    private int score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        textField = GetComponent<TextMeshProUGUI>();
        if (textField != null) {
            Debug.Log("No TextMeshProUGUI component found.");
        }

        score = 0;
    }

    // Update is called once per frame
    void Update() {
        string message = string.Format("Score: {000}", score);
        textField.text = message.ToString();
    }

    public void AddToScore() {
        score++;
    }

    public int GetScore() {
        return score;
    }
}
