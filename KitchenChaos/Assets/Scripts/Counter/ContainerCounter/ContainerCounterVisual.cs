using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    public const string OPEN_CLOSE="OpenClose";
    private Animator animator;
    [SerializeField] private ContainerCounter containerCounter;
    void Awake()
    {
        animator=GetComponent<Animator>();
    }

    void Start()
    {
        containerCounter.OnPlayerGradbbedObject+=OnPlayerGradbbedObject_ContainerCounter;
    }

    private void OnPlayerGradbbedObject_ContainerCounter(object sender, EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
