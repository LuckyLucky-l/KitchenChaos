using System;
using UnityEngine;
using UnityEngine.Events;

public class TestingEvents : MonoBehaviour
{
    // 声明一个事件，当按下空格时触发
    public event EventHandler<OnSpacePressedEventArgs> OnSpacePressed;

    // 定义一个用于传递事件参数的类，包含按下空格的次数
    public class OnSpacePressedEventArgs : EventArgs
    {
        public int spaceCount;  // 记录按下空格的次数
    }

    // 声明一个自定义委托事件，传递 float 类型的参数
    public event TestEventDelegate OnFloatEvent;
    public delegate void TestEventDelegate(float f);

    // 声明一个使用 Action 委托的事件，传递 bool 和 int 参数
    public event Action<bool, int> OnActionEvent;

    // Unity 内置的事件类型 UnityEvent，支持在 Inspector 中配置
    public UnityEvent OnUnityEvent;

    private int spaceCount;  // 用来记录按下空格的次数

    private void Start()
    {
        // 初始化代码（如果需要）
    }

    private void Update()
    {
        // 检测玩家是否按下空格键
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 空格键被按下
            spaceCount++;  // 增加按下次数
            // 触发 OnSpacePressed 事件，并传递当前空格按下次数
            OnSpacePressed?.Invoke(this, new OnSpacePressedEventArgs { spaceCount = spaceCount });
            // 触发 OnFloatEvent 事件，传递浮点值 5.5f
            OnFloatEvent?.Invoke(5.5f);
            // 触发 OnActionEvent 事件，传递布尔值 true 和整数 56
            OnActionEvent?.Invoke(true, 56);
            // 触发 UnityEvent
            OnUnityEvent?.Invoke();
        }
    }
}
