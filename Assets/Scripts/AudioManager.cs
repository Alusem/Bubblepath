using UnityEngine;

public class AudioManager : MonoBehaviour
{   
    public SoundController soundController;
    public MusicController musicController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //if(apareceu_Boss){
          //  musicController.PlayMusic(bossMusic);
        //}
    }

    public void PlaySound(AudioClip Sound)
    {
        soundController.PlaySound(Sound);
    }

    public void PlayMusic(AudioClip Music)
    {
        musicController.StopMusic();
        musicController.PlayMusic(Music);
    }

    public void PauseMusic(){
        musicController.StopMusic();
    }
}
