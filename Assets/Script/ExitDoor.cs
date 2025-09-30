using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    [Header("Transition")]
    public string nextSceneName = "LevelComplete";
    public float delay = 2f;

    [Header("FX de victoire")]
    public AudioClip sfxExit;
    public AudioSource audioSource;  
    public ParticleSystem[] confettis;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered) return;
        if (!collision.CompareTag("Player")) return;
        triggered = true;

        if (audioSource && sfxExit)
        {
            audioSource.spatialBlend = 0f; 
            audioSource.PlayOneShot(sfxExit);
        }

        if (confettis != null)
        {
            foreach (var ps in confettis)
                if (ps) ps.Play();
        }

        var rb = collision.attachedRigidbody;
        if (rb) { rb.linearVelocity = Vector2.zero; rb.bodyType = RigidbodyType2D.Static; }
        var move = collision.GetComponent<PlayerMove>();
        if (move) move.enabled = false;

        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(delay);
        if (!string.IsNullOrEmpty(nextSceneName))
            SceneManager.LoadScene(nextSceneName);
    }
}
