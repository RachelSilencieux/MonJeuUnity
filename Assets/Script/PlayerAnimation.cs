using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // ---- Déplacement horizontal ----
        float x = Input.GetAxis("Horizontal");

        // Active l’animation de course selon la vitesse
        animator.SetFloat("x", Mathf.Abs(x));

        // Oriente le sprite
        if (x > 0f) spriteRenderer.flipX = false;
        if (x < 0f) spriteRenderer.flipX = true;

        // ---- Animation de saut ----
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            animator.SetTrigger("Jump");
        }

        // ---- Animation d’attaque ----
        if (Input.GetKey(KeyCode.Space))
            animator.SetBool("Attack", true);
        else
            animator.SetBool("Attack", false);
    }
}
