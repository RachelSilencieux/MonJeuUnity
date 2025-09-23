using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    // Nombre de points de vie maximum du joueur (réglable dans l’Inspector).
    public int maxHealth = 5;

    // Points de vie courants (privé pour éviter des modifications externes accidentelles).
    private int currentHealth;

    // Awake est appelé au chargement du GameObject (avant Start).
    // On initialise les PV courants au maximum.

    private float reloadDelay = 2f;

    public string sceneToLoad = "GameOver";

    void Awake()
    {
        currentHealth = maxHealth;

        if(heartUI != null) heartUI.UpdateHearts(currentHealth, maxHealth);
    }

    public HeartUI heartUI;

    public void AddHealth(int v)
    {
        currentHealth = Mathf.Clamp(currentHealth + v, 0, maxHealth);
        // TODO: UI
        Debug.Log("Player gagne " + v + " PV. HP = " + currentHealth);
        if (heartUI != null) heartUI.UpdateHearts(currentHealth, maxHealth);

    }

    // Méthode à appeler quand le joueur subit des dégâts.
    // 'dmg' représente la quantité de dégâts à retirer.
    public void TakeDamage(int dmg)
    {
        // On soustrait les PV.
        currentHealth -= dmg;

        // Log de debug pour visualiser la perte de PV dans la Console.
        Debug.Log("Player prend " + dmg + " dégâts. HP restants = " + currentHealth);

        // Si les PV tombent à 0 ou moins, on déclenche la mort.
        if (currentHealth <= 0) Die();

        // NOTE :
        // - Tu peux ajouter un Mathf.Clamp pour empêcher les PV de passer sous 0 :
        //   currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        // - Tu peux aussi déclencher ici une mise à jour d’UI (barre de vie).
    }

    // Gère la mort du joueur : animations, désactivation d’input, rechargement de scène, etc.
    public void Die()
    {
        Debug.Log("Player est mort !");
        // Ici : désactiver le joueur, lancer une animation, recharger la scène, etc.
        // Exemple (selon ton architecture) :
        GetComponent<PlayerMove>().enabled = false;
        //animator.SetTrigger("Dead");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (reloadDelay <= 0f) SceneManager.LoadScene(sceneToLoad);
        else StartCoroutine(CoDelay(sceneToLoad, reloadDelay));
    }

    IEnumerator CoDelay(string name, float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        SceneManager.LoadScene(name);
    }


}