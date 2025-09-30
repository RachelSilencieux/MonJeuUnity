using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Santé du joueur")]
    public int maxHealth = 5;
    private int currentHealth;

    [Header("Vies du joueur")]
    public int maxLives = 3;
    private int currentLives;

    private bool isDead = false;

    [Header("Transition")]
    public float respawnDelay = 2f;
    public string gameOverScene = "GameOver";

    [Header("Checkpoint")]
    private Vector3 lastCheckpointPos;
    private bool checkpointSet = false;

    void Awake()
    {
        currentHealth = maxHealth;
        currentLives = maxLives;
        lastCheckpointPos = transform.position;
        checkpointSet = true;

        Debug.Log($"[Awake] HP={currentHealth}/{maxHealth} | Lives={currentLives}/{maxLives}");
    }



    public void AddHealth(int v)
    {
        currentHealth = Mathf.Clamp(currentHealth + v, 0, maxHealth);
        Debug.Log("Player gagne " + v + " PV. HP = " + currentHealth);
    }

    public void TakeDamage(int dmg)
    {
        if (isDead) return;

        currentHealth -= dmg;
        Debug.Log("Player prend " + dmg + " dégâts. HP restants = " + currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        currentLives--;
        Debug.Log("Player est mort ! Vies restantes = " + currentLives);

        var move = GetComponent<PlayerMove>();
        if (move) move.enabled = false;

        if (currentLives > 0)
        {
            StartCoroutine(Respawn());
        }
        else
        {
            StartCoroutine(GameOver());
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnDelay);

        currentHealth = maxHealth;
        isDead = false;

        if (checkpointSet)
            transform.position = lastCheckpointPos;

        var anim = GetComponent<Animator>();
        if (anim)
        {
            anim.ResetTrigger("Dead");
            anim.Play("Idle");
        }


        var move = GetComponent<PlayerMove>();
        if (move) move.enabled = true;

        Debug.Log("Respawn au checkpoint " + lastCheckpointPos);
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(respawnDelay);
        SceneManager.LoadScene(gameOverScene);
    }

    public void SetCheckpoint(Vector3 pos)
    {
        lastCheckpointPos = pos;
        checkpointSet = true;
        Debug.Log("Checkpoint activé en " + pos);
    }
}
