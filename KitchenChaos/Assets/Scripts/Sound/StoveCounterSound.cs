using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField]private StoveCounter stoveCounter;
    private AudioSource audioSource;
    private bool playWaringSound;
    private float warningSoundTimer;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
       
    }
    void Start()
    {
        stoveCounter.OnStateChanged += stoveCounter_OnStateChanged;
        stoveCounter.OnProgressChanged+=stoveCounter_OnProgressChanged;
    }
    void Update()
    {
        if (playWaringSound)
        {
            warningSoundTimer -= Time.deltaTime;
            if (warningSoundTimer <= 0)
            {
                float warningSoundTimerMax=.2f;
                warningSoundTimer=warningSoundTimerMax;
                SoundManager.Instance.PlayStoveWarningSound(stoveCounter.transform.position);
            }
        }
    }

    private void stoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowPRogressAmount=0.5f;
        playWaringSound=burnShowPRogressAmount<=e.progressNormalized&&stoveCounter.Isfried();
    }

    private void stoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool PlaySound=e.state==StoveCounter.State.Frying||e.state==StoveCounter.State.Fried;
       if(PlaySound)
       {
            if (!audioSource.isPlaying)  // 检查是否正在播放
            {
                audioSource.Play();
            }    
       }else
       {
            audioSource.Pause();
       }
    }
}
