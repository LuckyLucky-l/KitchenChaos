using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnFlashingBarUI : MonoBehaviour
{
    [SerializeField]private StoveCounter stoveCounter;
    private Animator animator;
    private const string Is_Flashing = "Isflashing";
  void Awake()
    {
       animator = GetComponent<Animator>();
    }
   void Start()
   {
        animator.SetBool(Is_Flashing,false);
        stoveCounter.OnProgressChanged+=stoveCounter_OnProgressChanged;
   }

    private void stoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowPRogressAmount = 0.5f ;
        bool flashing=burnShowPRogressAmount<=e.progressNormalized&&stoveCounter.Isfried();
        if (flashing)
        {
            animator.SetBool(Is_Flashing,true);
        }
    }
}
