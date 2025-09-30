using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerDead : MonoBehaviour
{
    public GameObject player;
    private Animator playerAnimator;
    [SerializeField] AudioClip sfxDead;
    private AudioSource audioSource;

    private void Start()
    {
        playerAnimator = player.GetComponent<Animator>();
        audioSource = player.GetComponent<AudioSource>();
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerAnimator.SetTrigger("Dead");
            audioSource.PlayOneShot(sfxDead);

            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null) playerMovement.enabled = false;

            StartCoroutine(WaitAndGameOver(other.gameObject));
        }
    }

    private IEnumerator WaitAndGameOver(GameObject player)
    {
        yield return new WaitForSeconds(2f); 

        var health = player.GetComponent<PlayerHealth>();
        if (health != null)
            health.TakeDamage(health.maxHealth);
        else
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }


}
