using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUIController : BaseUIButtonController
{
    [SerializeField] private Button stage1Button;
    [SerializeField] private Button stage2Button;
    [SerializeField] private Button stage3Button;

    [SerializeField] private Button SettingButton;
    [SerializeField] private GameObject SettingPanel;

    private void Start()
    {
        RegisterButton(stage1Button, () => OnStageButtonPressed("Stage1Scene"));
        RegisterButton(stage2Button, () => OnStageButtonPressed("Stage2Scene"));
        RegisterButton(stage3Button, () => OnStageButtonPressed("Stage3Scene"));

        RegisterButton(SettingButton, OnSettingButtonPressed);

        if (SettingPanel != null)
            SettingPanel.SetActive(false); // 시작 시 설정 패널 비활성화

    }

    private void OnStageButtonPressed(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void OnSettingButtonPressed()
    {
        if (SettingPanel != null)
        {
            bool isActive = SettingPanel.activeSelf;
            SettingPanel.SetActive(true); // 눌릴 때마다 on/off
            Debug.Log("설정 패널 열림");
        }
    }
}
