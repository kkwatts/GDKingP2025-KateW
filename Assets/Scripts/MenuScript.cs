using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {
    public void GoToGame() {
        StartCoroutine(WaitForSoundAndTransition("MainGame"));
    }

    public void GoToMenu() {
        StartCoroutine(WaitForSoundAndTransition("MainMenu"));
    }

    public void GoToCharacterSelect() {
        StartCoroutine(WaitForSoundAndTransition("CharacterSelection"));
    }

    public void RestartGameScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator WaitForSoundAndTransition(string sceneName) {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        SceneManager.LoadScene(sceneName);
    }
}
