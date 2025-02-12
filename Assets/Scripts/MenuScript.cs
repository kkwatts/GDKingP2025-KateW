using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {
    public void GoToGame() {
        SceneManager.LoadScene("MainGame");
    }

    public void GoToMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGameScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
