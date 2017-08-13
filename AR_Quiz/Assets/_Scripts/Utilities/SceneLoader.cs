using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Responsible for load a scene asynchronously, showing the progress.
/// </summary>
public class SceneLoader : MonoBehaviour {

    [SerializeField] private GameObject LoadingScreenPanel;
    [SerializeField] private Image LoadingProgress;
    [SerializeField] private Text ProgressText;

    /// <summary>
    /// Load a scene asynchronously, showing the progress at the LoadingScreenPanel
    /// </summary>
    /// <param name="sceneIndex">Index of the scene to be loaded.</param>
    /// Called in some OnClick methods that change scene.
    public void LoadLevel(int sceneIndex) {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex) {
        LoadingScreenPanel.SetActive(true);

        AsyncOperation assync = SceneManager.LoadSceneAsync(sceneIndex);
        assync.allowSceneActivation = false;

        while (!assync.isDone) {

            LoadingProgress.fillAmount = assync.progress;
            ProgressText.text = (int)(assync.progress * 100) + "%";

            if (assync.progress == .9f) {
                LoadingProgress.fillAmount = 1f;
                ProgressText.text = (int)(LoadingProgress.fillAmount * 100) + "%";

                assync.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
