using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";
   public static MusicManager Instance;
    private AudioSource audioSource;
    private float volume=1f;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Instance = this;
        volume=PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, volume);
        audioSource.volume=volume;
    }
    public void ChangeVolume(){
        volume+=0.1f;
        if (volume>=1.1f)
        {
            volume=0f;
        }
        audioSource.volume=volume;
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, volume);
    }
    public float GetVolume(){
        return volume;
    }
}
