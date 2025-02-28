using UnityEngine;

[CreateAssetMenu(fileName = "HighScore", menuName = "Scriptable Objects/HighScore")]
public class HighScore : ScriptableObject {
    public int highScore;

    public void UpdateHighScore(int score) {
        highScore = score;
    }

    public int GetHighScore() {
        return highScore;
    }
}
