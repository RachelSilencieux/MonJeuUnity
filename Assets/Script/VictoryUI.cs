using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryUI : MonoBehaviour
{
    [Header("UI de victoire")]
    public CanvasGroup victoryPanel;
    public float fadeDuration = 0.5f;
    public AudioSource victoryMusic;
    public VictoryUI victoryUI;

    [Header("Animation optionnelle")]
    public Animator victoryAnimator;
    public string victoryTrigger = "Play";

    private void Awake()
    {
        if (victoryPanel)
        {
            victoryPanel.alpha = 0f;
            victoryPanel.interactable = false;
            victoryPanel.blocksRaycasts = false;
        }
    }

    public void PlayVictory()
    {
        if (victoryAnimator && !string.IsNullOrEmpty(victoryTrigger))
        {
            victoryAnimator.SetTrigger(victoryTrigger);
        }

        if (victoryMusic)
            victoryMusic.Play();

        if (victoryPanel)
            StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float t = 0f;
        victoryPanel.interactable = true;
        victoryPanel.blocksRaycasts = true;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            victoryPanel.alpha = Mathf.InverseLerp(0f, fadeDuration, t);
            yield return null;
        }

        victoryPanel.alpha = 1f;
    }

    public string gameSceneName = "Level1";
    public void PlayAgain()
    {
        if (!string.IsNullOrEmpty(gameSceneName))
        {
            SceneManager.LoadScene(gameSceneName);
        }
        else
        {
            Debug.LogError("Impossible de reload le jeu!");

        }
    }


    public void MainPage()
    {
        SceneManager.LoadScene("PlayGame");
    }
}
