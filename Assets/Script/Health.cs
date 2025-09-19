using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Santé")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int startHealth = 100;

    public int Max => maxHealth;
    public int Current { get; private set; }

    [Header("Événements")]
    public UnityEvent<int, int> OnHealthChanged; // (current, max)
    public UnityEvent OnDeath;

    public bool IsDead => Current <= 0;

    void Awake()
    {
        Current = Mathf.Clamp(startHealth, 0, maxHealth);
        OnHealthChanged?.Invoke(Current, maxHealth);
    }

    public void TakeDamage(int amount)
    {
        if (IsDead || amount <= 0) return;
        Current = Mathf.Clamp(Current - amount, 0, maxHealth);
        OnHealthChanged?.Invoke(Current, maxHealth);
        if (IsDead) OnDeath?.Invoke();
    }

    public void Kill()
    {
        if (IsDead) return;
        Current = 0;
        OnHealthChanged?.Invoke(Current, maxHealth);
        OnDeath?.Invoke();
    }
}