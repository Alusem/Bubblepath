using UnityEngine;

public class SoundController : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent <AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void PlaySound(AudioClip Sound)
    {
        audioSource.PlayOneShot(Sound);
    }
}
