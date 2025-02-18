using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module.QTE;

public class QTESample : MonoBehaviour
{
    public QTEController qteController;

    void Start()
    {
        // 示例：在游戏开始3秒后触发QTE事件，自定义输入为空格键，时限为5秒
        Invoke("TriggerQTEEvent", 3f);
    }

    void TriggerQTEEvent()
    {
        // 定义连续输入序列
        List<string> inputSequence = new List<string> { "Up", "Left", "Down", "Right" };
        // 触发QTE事件，时限为10秒
        qteController.TriggerQTE(inputSequence, 10f);
        // 订阅QTE结果事件
        qteController.onQTESuccess += HandleQTESuccess;
    }

    // 处理QTE结果
    void HandleQTESuccess(bool success)
    {
        if (success)
        {
            Debug.Log("QTE成功！");
        }
        else
        {
            Debug.Log("QTE失败！");
        }
        // 取消订阅事件
        qteController.onQTESuccess -= HandleQTESuccess;
    }
}
