using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem; // New Input System
#endif

/// Déplacement 2D simple, compatible ancien ET nouveau système d'input.
/// - Si le New Input System est actif: lit Keyboard/Gamepad (sans asset .inputactions)
/// - Sinon: lit Input.GetAxisRaw("Horizontal/Vertical")
[RequireComponent(typeof(Transform))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Mouvement")]
    public float speed = 5f;
    public bool useRigidbody2D = false;

    private Rigidbody2D rb;

    void Awake()
    {
        if (useRigidbody2D) rb = GetComponent<Rigidbody2D>();
    }

    Vector2 ReadMove()
    {
        Vector2 v = Vector2.zero;

#if ENABLE_INPUT_SYSTEM
        // --- Nouveau système ---
        if (Keyboard.current != null)
        {
            float x = 0, y = 0;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) x -= 1;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) x += 1;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) y -= 1;
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) y += 1;
            v = new Vector2(x, y);
        }
        if (Gamepad.current != null)
        {
            // On prend le stick si son amplitude est > au clavier (utile quand manette branchée)
            Vector2 stick = Gamepad.current.leftStick.ReadValue();
            if (stick.sqrMagnitude > v.sqrMagnitude) v = stick;
        }
#else
        // --- Ancien système ---
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        v = new Vector2(x, y);
#endif

        if (v.sqrMagnitude > 1f) v.Normalize();
        return v;
    }

    void Update()
    {
        if (useRigidbody2D && rb) return; // géré en FixedUpdate
        Vector2 move = ReadMove();
        transform.position += (Vector3)(move * speed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        if (!useRigidbody2D || !rb) return;
        Vector2 move = ReadMove();
#if UNITY_2022_2_OR_NEWER
        rb.linearVelocity = move * speed;
#else
        rb.velocity = move * speed;
#endif
    }
}