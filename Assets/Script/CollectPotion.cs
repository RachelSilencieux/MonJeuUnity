using UnityEngine;

public class CollectPotion : MonoBehaviour
{
    public int healAmount = 1;
    public AudioClip sfxPickup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        var hp = collision.GetComponent<PlayerHealth>();
        if (hp) hp.AddHealth(healAmount);
        Debug.Log("Potion collectée, + " + healAmount + " PV");

        if (sfxPickup) AudioSource.PlayClipAtPoint(sfxPickup, transform.position, 0.8f);
        Destroy(gameObject);
    }

}
