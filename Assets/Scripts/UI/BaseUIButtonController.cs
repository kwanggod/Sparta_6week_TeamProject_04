using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public abstract class BaseUIButtonController : MonoBehaviour
{
    // ��ư�� �� ��ư�� ������ �� ������ Action�� �����ϴ� Ŭ����
    private class ButtonListener
    {
        public Button button;
        public UnityEngine.Events.UnityAction action;
    }

    private readonly List<ButtonListener> buttonListeners = new List<ButtonListener>();

    // ��ư�� �����ϰ� ����ϴ� �Լ�
    protected void RegisterButton(Button button, UnityEngine.Events.UnityAction action)
    {
        if (button == null)
        {
            Debug.LogWarning($"{name}: ����Ϸ��� ��ư�� null�Դϴ�!");
            return;
        }

        button.onClick.AddListener(action);
        buttonListeners.Add(new ButtonListener { button = button, action = action });
    }

    // ��� ��ϵ� ��ư �����ʸ� ����
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