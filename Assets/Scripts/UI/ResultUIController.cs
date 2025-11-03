using UnityEngine;
using UnityEngine.UI;

public class ResultUIController : BaseUIButtonController
{
    [SerializeField] private Button retryButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button optionButton;

    private void Start()
    {
        RegisterButton(retryButton, OnRetryPressed);
        RegisterButton(exitButton, OnExitPressed);
        RegisterButton(optionButton, OnOptionPressed);
    }
    private void OnRetryPressed()
    {
        //UIManager.Instance?.LoadScene("MainScene");
    }

    private void OnExitPressed()
    {
        UIManager.Instance?.LoadScene("MainScene");
    }

    private void OnOptionPressed() => UIManager.Instance.ShowLobbyOption();
}
