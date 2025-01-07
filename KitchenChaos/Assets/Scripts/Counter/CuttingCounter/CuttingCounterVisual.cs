using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    private const string CUT="Cut";
    private Animator animator;
    [SerializeField] private CuttingCounter cuttingCounter;
    void Awake()
    {
        animator=GetComponent<Animator>();
    }
    void Start()
    {
         cuttingCounter.OutCut+=CuttingCounter_AnimatorCuttingevent;
    }
    private void CuttingCounter_AnimatorCuttingevent(object sender, EventArgs e)
    {
        animator.SetTrigger("Cut");
    }

}
