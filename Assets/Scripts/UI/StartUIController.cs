using UnityEngine;
using UnityEngine.UI;

// StartScene ���� UI ��Ʈ�ѷ�
public class StartUIController : BaseUIButtonController
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;

    private void Start()
    {
        RegisterButton(startButton, OnStartPressed);
        RegisterButton(exitButton, OnExitPressed);
    }

    private void OnStartPressed()
    {
        Debug.Log("���� ���� ��ư Ŭ����");
        UIManager.Instance?.LoadScene("MainScene");
    }

    private void OnExitPressed()
    {
        Debug.Log("���� ���� ��ư Ŭ����");

#if UNITY_EDITOR
        // �����Ϳ��� �׽�Ʈ�� ���� Play��� �ߴ�
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ����� ȯ�濡���� �� ����
        Application.Quit();
#endif
    }
}
