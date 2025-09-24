using UnityEngine;

public class OpenCage : MonoBehaviour
{
    public GameObject princess;
    public AudioClip sfxOpen;
    public GameObject door;

    public void Open()
    {
        Debug.Log("La cage est ouverte");
        if (sfxOpen) AudioSource.PlayClipAtPoint(sfxOpen, transform.position, 0.8f);
        if (princess) princess.SetActive(true);
        if (door) door.SetActive(true);

        gameObject.SetActive(false);
    }

}
