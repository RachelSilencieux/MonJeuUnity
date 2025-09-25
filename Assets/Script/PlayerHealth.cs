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
    private static int currentLives = -1;  

    private bool isDead = false;            

    private float reloadDelay = 2f;
    public string sceneToLoad = "GameOver";

    

    void Awake()
    {
        currentHealth = maxHealth;

        if (currentLives < 0)
            currentLives = maxLives;



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
            StartCoroutine(CoDelay(SceneManager.GetActiveScene().name, reloadDelay));
        }
        else
        {
            StartCoroutine(CoDelay(sceneToLoad, reloadDelay));
        }
    }

    IEnumerator CoDelay(string name, float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        SceneManager.LoadScene(name);
    }

    public static void ResetLivesForNewGame()
    {
        currentLives = -1; 
    }
}
