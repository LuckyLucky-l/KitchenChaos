using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
   [SerializeField] private Image barImage;
   [SerializeField] private GameObject hasProgressGameObject;

   private IHasProgress hasProgress;

    void Start()
    {
        hasProgress = hasProgressGameObject.GetComponentInParent<IHasProgress>();
        hasProgress.OnProgressChanged+=IHasProgress_OnProgressChanged;
        Hide();
    }

    private void IHasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount=e.progressNormalized;
        if (e.progressNormalized==0||e.progressNormalized==1)
        {
            Hide();
        }else{
            Show();
        }
    }
    public void Show(){
        gameObject.SetActive(true);
    }
    public void Hide(){
        gameObject.SetActive(false);
    }
}
