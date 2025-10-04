using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Paramètres d’attaque")]
    public Transform attackPoint;        
    public float attackRange = 1.5f;    
    public int attackDamage = 1;                  
    public float attackCooldown = 0.5f; 
    public LayerMask enemyLayers;      


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>()?.TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
