using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyDamageOnContact : MonoBehaviour
{
    /// <summary>
    /// Tag attendu sur la racine du joueur (celle qui porte le Rigidbody2D et PlayerHealth).
    /// </summary>
    public string playerTag = "Player";

    /// <summary>
    /// Dégâts infligés à chaque "hit".
    /// </summary>
    public int damage = 10;

    /// <summary>
    /// Délai minimal entre deux coups sur le même joueur (en secondes).
    /// Évite d'infliger plusieurs hits dans le même instant lors d'entrées/sorties rapides.
    /// </summary>
    public float hitCooldown = 0.4f;  // évite le spam

    // Mémorise le dernier moment où un coup a été porté (par ennemi).
    // Pour un cooldown par CIBLE, utiliser un Dictionary< PlayerHealth, float > (voir NOTE).
    float lastHitTime = -999f;

    void Reset()
    {
        // Comme il s'agit d'une hitbox, on force le collider en "trigger"
        var col = GetComponent<Collider2D>();
        if (col) col.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // On remonte à l'objet "racine" qui porte le Rigidbody2D (le vrai Player)
        // car le contact peut provenir d'un collider enfant du joueur.
        var root = other.attachedRigidbody ? other.attachedRigidbody.gameObject : other.gameObject;

        // Filtre : on ne réagit qu'au Player (par Tag)
        if (!root.CompareTag(playerTag)) return;

        // Récupère le script de vie du joueur (ta classe à toi)
        var hp = root.GetComponent<PlayerHealth>();
        if (!hp) return; // pas de santé trouvée ? rien à faire

        // Anti-spam : si le délai minimal n'est pas écoulé, on ignore ce hit
        if (Time.time - lastHitTime < hitCooldown) return;

        // Inflige les dégâts ? ta UI écoute PlayerHealth et mettra la barre à jour
        hp.TakeDamage(damage);

        // Mémorise l'heure du dernier coup
        lastHitTime = Time.time;
    }
}