using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    [SerializeField]private PlayerControl playerControl;
    private const string palyer_IsWalking="IsWalking";
    void Start()
    {
        animator=GetComponent<Animator>();
    }
    void Update()
    {
        animator.SetBool(palyer_IsWalking,playerControl.IsWalking());
    }
}
