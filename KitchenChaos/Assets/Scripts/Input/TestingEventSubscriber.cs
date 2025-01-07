using UnityEngine;

public class TestingEventSubscriber : MonoBehaviour
{
    private void Start()
    {
        // 获取附加在同一个 GameObject 上的 TestingEvents 组件
        TestingEvents testingEvents = GetComponent<TestingEvents>();
        // 订阅 OnSpacePressed 事件
        testingEvents.OnSpacePressed += TestingEvents_OnSpacePressed;
        // 订阅 OnFloatEvent 事件
        testingEvents.OnFloatEvent += TestingEvents_OnFloatEvent;
        // 订阅 OnActionEvent 事件
        testingEvents.OnActionEvent += TestingEvents_OnActionEvent;
    }

    // 事件处理方法，当 OnActionEvent 触发时被调用
    private void TestingEvents_OnActionEvent(bool arg1, int arg2)
    {
        // 打印传递的 bool 和 int 参数到控制台
        Debug.Log(arg1 + " " + arg2);
    }

    // 事件处理方法，当 OnFloatEvent 触发时被调用
    private void TestingEvents_OnFloatEvent(float f)
    {
        // 打印传递的 float 参数到控制台
        Debug.Log("Float: " + f);
    }

    // 事件处理方法，当 OnSpacePressed 触发时被调用
    private void TestingEvents_OnSpacePressed(object sender, TestingEvents.OnSpacePressedEventArgs e)
    {
        // 打印按下空格的次数到控制台
        Debug.Log("Space! " + e.spaceCount);
    }

    // 用于 UnityEvent 事件的处理方法
    public void TestingUnityEvent()
    {
        // 当 UnityEvent 触发时，打印消息到控制台
        Debug.Log("TestingUnityEvent");
    }
}
