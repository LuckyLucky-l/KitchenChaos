using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private PlayerControl playerControl;
    private float footstepTimer;
    private float footstepTimerMax=.1f;
    void Awake()
    {
        playerControl = GetComponent<PlayerControl>();
    }
    void Update()
    {
        footstepTimer-= Time.deltaTime;
        if (footstepTimer <0f)
        {
            footstepTimer = footstepTimerMax;
            if (playerControl.IsWalking())
            {
                float volume=1f;
               SoundManager.Instance.PlayFootStepSound(playerControl.transform.position,volume);   
            }
        }
    }
}
