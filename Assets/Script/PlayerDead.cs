using UnityEngine;

public class PlayerDead : MonoBehaviour
{
    public GameObject player;
    private Animator playerAnimator;

    private void Start()
    {
        playerAnimator = player.GetComponent<Animator>();
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerAnimator.SetTrigger("Dead");

            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if(playerMovement != null)
            {
                playerMovement.enabled = false;
            }

        }

    



    }

}
