using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnWaringUI : MonoBehaviour
{
    [SerializeField]private StoveCounter stoveCounter;
    void Start()
    {
        stoveCounter.OnProgressChanged+=stoveCounter_OnProgressChanged;
        Hide();
    }

    private void stoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowPRogressAmount = 0.5f ;
        bool show=burnShowPRogressAmount<=e.progressNormalized&&stoveCounter.Isfried();
        if (show)
        {
            Show();
        }else{
            Hide();
        }
    }
    public void Show(){
        gameObject.SetActive(true);
    }
    public void Hide(){
        gameObject.SetActive(false);
    }
}
