using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// 该类负责显示炉灶计数器的视觉表现。
/// </summary>
public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField]private StoveCounter stoveCounter;
    [SerializeField]private GameObject stoveOnGameObject;
    [SerializeField]private GameObject particlesGameObject;
    void Start()
    {
        stoveCounter.OnStateChanged +=StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool ShowVisual=e.state==StoveCounter.State.Frying||e.state==StoveCounter.State.Fried;
        stoveOnGameObject.SetActive(ShowVisual);
        particlesGameObject.SetActive(ShowVisual);
    }
}
