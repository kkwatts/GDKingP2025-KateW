using UnityEngine;

public class MenuScript : MonoBehaviour {
    public void GoToGame() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }
}
