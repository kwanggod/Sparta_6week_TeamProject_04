using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUIController : BaseUIButtonController
{
    [SerializeField] private Button stage1Button;
    [SerializeField] private Button stage2Button;
    [SerializeField] private Button stage3Button;

    [SerializeField] private Button optionButton;

    private void Start()
    {
        RegisterButton(stage1Button, () => OnStageButtonPressed("Stage1Scene"));
        RegisterButton(stage2Button, () => OnStageButtonPressed("Stage2Scene"));
        RegisterButton(stage3Button, () => OnStageButtonPressed("Stage3Scene"));

        RegisterButton(optionButton, OnOptionPressed);
    }

    private void OnStageButtonPressed(string sceneName) => UIManager.Instance.LoadScene(sceneName);

    private void OnOptionPressed() => UIManager.Instance.ShowLobbyOption();
}
