using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 置空静态的事件管理器
/// </summary>
public class ResetStaticDataManager : MonoBehaviour
{
    void Awake()
    {
        CuttingCounter.ResetStaticDataManager();
        BaseCounter.ResetStaticDataManager();
        TrashCounter.ResetStaticDataManager();
    }
}
