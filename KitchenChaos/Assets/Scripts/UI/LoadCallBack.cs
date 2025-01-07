using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCallBack : MonoBehaviour
{
    private bool isFirst=true;
    void Update()
    {
        if(isFirst)
        {
            isFirst=false;
            Loader.LoadCallBack();
        }
    }
}
