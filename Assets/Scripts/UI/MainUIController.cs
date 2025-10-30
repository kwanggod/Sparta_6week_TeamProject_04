using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUIController : BaseUIButtonController
{
    [SerializeField] private Button stage1Button;
    [SerializeField] private Button stage2Button;
    [SerializeField] private Button stage3Button;

    private void Start()
    {
        RegisterButton(stage1Button, () => OnStageButtonPressed("Stage1Scene"));
        RegisterButton(stage2Button, () => OnStageButtonPressed("Stage2Scene"));
        RegisterButton(stage3Button, () => OnStageButtonPressed("Stage3Scene"));
    }

    private void OnStageButtonPressed(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
