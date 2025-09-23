using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    public string nextSceneName = "LevelComplete";
    public AudioClip sfxExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (sfxExit) AudioSource.PlayClipAtPoint(sfxExit, transform.position, 0.8f);
        collision.gameObject.SetActive(false);

        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

}
