using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public abstract class BaseUIButtonController : MonoBehaviour
{
    // 버튼과 그 버튼이 눌렸을 때 실행할 Action을 저장하는 클래스
    private class ButtonListener
    {
        public Button button;
        public UnityEngine.Events.UnityAction action;
    }

    private readonly List<ButtonListener> buttonListeners = new List<ButtonListener>();

    // 버튼을 안전하게 등록하는 함수
    protected void RegisterButton(Button button, UnityEngine.Events.UnityAction action)
    {
        if (button == null)
        {
            Debug.LogWarning($"{name}: 등록하려는 버튼이 null입니다!");
            return;
        }

        button.onClick.AddListener(action);
        buttonListeners.Add(new ButtonListener { button = button, action = action });
    }

    // 모든 등록된 버튼 리스너를 해제
    protected virtual void OnDestroy()
    {
        foreach (var listener in buttonListeners)
        {
            if (listener.button != null)
                listener.button.onClick.RemoveListener(listener.action);
        }

        buttonListeners.Clear();
    }
}