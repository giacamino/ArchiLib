using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Module.QTE
{
    /// <summary>
    /// QTE事件管理
    /// </summary>
    /// <remarks>
    /// 触发、倒计时、输入检测和结果判定
    /// </remarks>
    public class QTEController : MonoBehaviour
    {
        public Text promptText;
        public Text countdownText;
        private float currentTime;
        private bool isQTEActive = false;
        private List<string> correctInputSequence;
        private int currentIndex = 0;
        public Action<bool> onQTESuccess; // 定义一个委托，用于在QTE结束时传递结果

        // 触发QTE事件的接口方法，可自定义输入和时限
        public void TriggerQTE(List<string> inputSequence, float duration)
        {
            correctInputSequence = inputSequence;
            currentTime = duration;
            isQTEActive = true;
            currentIndex = 0;
            UpdatePromptText();
        }

        void Update()
        {
            if (isQTEActive)
            {
                currentTime -= Time.deltaTime;
                countdownText.text = "剩余时间: " + currentTime.ToString("F1") + "s";

                if (currentTime <= 0f)
                {
                    EndQTE(false);
                }
                else if (CheckInput())
                {
                    currentIndex++;
                    if (currentIndex >= correctInputSequence.Count)
                    {
                        EndQTE(true);
                    }
                    else
                    {
                        UpdatePromptText();
                    }
                }
            }
        }

        // 检查玩家输入是否正确
        bool CheckInput()
        {
            if (currentIndex < correctInputSequence.Count)
            {
                string currentCorrectInput = correctInputSequence[currentIndex];
                switch (currentCorrectInput)
                {
                    case "Up":
                        return Input.GetKeyDown(KeyCode.UpArrow);
                    case "Down":
                        return Input.GetKeyDown(KeyCode.DownArrow);
                    case "Left":
                        return Input.GetKeyDown(KeyCode.LeftArrow);
                    case "Right":
                        return Input.GetKeyDown(KeyCode.RightArrow);
                    case "Space":
                        return Input.GetKeyDown(KeyCode.Space);
                    // 可以继续添加更多按键的判断
                    default:
                        return false;
                }
            }
            return false;
        }

        // 更新提示文本
        void UpdatePromptText()
        {
            if (currentIndex < correctInputSequence.Count)
            {
                promptText.text = "请按下 " + correctInputSequence[currentIndex] + " 键！";
            }
        }

        // 结束QTE事件
        void EndQTE(bool success)
        {
            isQTEActive = false;
            promptText.text = "";
            countdownText.text = "";
            if (onQTESuccess != null)
            {
                onQTESuccess(success);
            }
        }
    }
}
