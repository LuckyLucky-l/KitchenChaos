using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasProgress
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;//发布切菜的事件
    public class OnProgressChangedEventArgs:EventArgs{
        public float progressNormalized;//进度条
    }

}
