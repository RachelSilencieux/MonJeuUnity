using UnityEngine;

public class GameMusic : MonoBehaviour
{
    [SerializeField] AudioClip gameMusicClip;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = gameMusicClip;
        audioSource.loop = true;
        audioSource.Play();
    }
}
