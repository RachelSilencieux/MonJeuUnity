using UnityEngine;

public class AcidKill : MonoBehaviour
{
    [Tooltip("Tag du volume d'acide (ex: Acid).")]
    public string acidTag = "Droop"; // mets "Acid" si tu préfères

    [Tooltip("Dégâts instantanés à l'entrée. 9999 = mort instantanée.")]
    public int damageOnEnter = 9999;

    [Tooltip("Optionnel: dégâts par seconde tant qu'on reste dedans (0 = off).")]
    public int damagePerSecond = 0;

    private Health playerHealth;
    private Animator playerAnimator;

    void Start()
    {
        // Récupère Health + Animator sur le player une fois pour toutes
        var player = FindAnyObjectByType<PlayerMove>(); // on part de ton script existant :contentReference[oaicite:2]{index=2}
        if (player)
        {
            playerHealth = player.GetComponent<Health>();
            playerAnimator = player.GetComponent<Animator>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(acidTag)) return;

        if (playerHealth)
        {
            if (damageOnEnter >= 9999) playerHealth.Kill();
            else playerHealth.TakeDamage(damageOnEnter);
        }
        if (playerAnimator) playerAnimator.SetTrigger("Dead"); // tu avais déjà ce trigger :contentReference[oaicite:3]{index=3}
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (damagePerSecond <= 0 || !other.CompareTag(acidTag) || playerHealth == null || playerHealth.IsDead) return;
        playerHealth.TakeDamage(Mathf.CeilToInt(damagePerSecond * Time.deltaTime));
    }
}