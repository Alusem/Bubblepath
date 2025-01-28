using UnityEngine;

public class MusicController : MonoBehaviour
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

    public void PlayMusic(AudioClip Music)
    {
        if(Music != null){
            audioSource.clip = Music;
        }
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}
