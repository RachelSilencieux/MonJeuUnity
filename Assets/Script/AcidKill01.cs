using UnityEngine;

public class AcidKill01 : MonoBehaviour
{
    public GameObject player;
    private Animator playerAnimator;

    private void Start()
    {
        if (player != null)
            playerAnimator = player.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)   
    {
        if (other.CompareTag("Player"))
        {
            playerAnimator.SetTrigger("OpenDoor");
        }
    }

    private void OnTriggerExit2D(Collider2D other)  
    {
        if (other.CompareTag("Player"))
        {
            playerAnimator.SetTrigger("CloseDoor");
        }
    }
}
