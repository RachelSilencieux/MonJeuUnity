using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class AttackHitbox : MonoBehaviour
{
    public int damage = 1;

    private void Reset()
    {
        var col = GetComponent<Collider2D>();
        if (col) col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        var health = other.GetComponent<Health>();

        if (health)
        {
            health.TakeDamage(damage);
            Debug.Log("[AttackHitbox] Dégât infligés: " + damage);
        }
    }

    public void EnableHitBox() { gameObject.SetActive(true); }
    public void DisableHitbox() { gameObject.SetActive(false); }

}
