using UnityEngine;
using UnityEngine.UI;

// StartScene 전용 UI 컨트롤러
public class StartUIController : BaseUIButtonController
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button optionButton;

    private void Start()
    {
        RegisterButton(startButton, OnStartPressed);
        RegisterButton(optionButton, OnOptionPressed);
    }

    private void OnStartPressed()
    {
        UIManager.Instance?.LoadScene("MainScene");
    }

    private void OnOptionPressed() => UIManager.Instance.ShowOptionPanel1();
}
