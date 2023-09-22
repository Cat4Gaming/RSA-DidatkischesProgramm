using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralSceneBehaviour : MonoBehaviour {
    public void ExitGame() {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    public void LoadScene(int sceneNumber) {
        SceneManager.LoadScene(sceneNumber);
    }

    public void OpenKeyboard() {
        TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, true);
    }
}