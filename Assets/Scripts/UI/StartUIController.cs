using UnityEngine;
using UnityEngine.UI;

// StartScene 전용 UI 컨트롤러
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
        Debug.Log("게임 시작 버튼 클릭됨");
        UIManager.Instance?.LoadScene("MainScene");
    }

    private void OnExitPressed()
    {
        Debug.Log("게임 종료 버튼 클릭됨");

#if UNITY_EDITOR
        // 에디터에서 테스트할 때는 Play모드 중단
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 빌드된 환경에서는 앱 종료
        Application.Quit();
#endif
    }
}
