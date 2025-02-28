using UnityEngine;
using TMPro;

public class HighScoreManager : MonoBehaviour {
    public HighScore highScore;

    private TextMeshProUGUI textField;
    private GameObject score;
    private int currentScore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        score = GameObject.FindGameObjectWithTag("Score");
        textField = GetComponent<TextMeshProUGUI>();
        Debug.Log(score.GetComponent<ScoreBehavior>().GetScore());
    }

    // Update is called once per frame
    void Update() {
        currentScore = score.GetComponent<ScoreBehavior>().GetScore();
        if (currentScore > highScore.GetHighScore()) {
            highScore.UpdateHighScore(currentScore);
        }

        string message = string.Format("High Score: {000}", highScore.GetHighScore());
        textField.text = message.ToString();
    }
}
