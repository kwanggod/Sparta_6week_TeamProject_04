using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionPanel : BaseUIButtonController
{
    [Header("소리 설정 슬라이더")]
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    [Header("버튼")]
    [SerializeField] private Button closeButton;
    [SerializeField] private Button panelCloseButton;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button quitGameButton;

    void OnEnable()
    {
        if (BGMPlayer.instance != null)
            bgmSlider.value = BGMPlayer.instance.GetVolume();

        if (SFXManager.instance != null)
            sfxSlider.value = SFXManager.instance.GetVolume();

        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);

        RegisterButton(closeButton, ClosePanel);
        RegisterButton(panelCloseButton, ClosePanel);
        RegisterButton(retryButton, Retry);
        RegisterButton(exitButton, ExitStage);
        RegisterButton(quitGameButton, QuitGame);
    }

    private void OnBGMVolumeChanged(float value)
    {
        if (BGMPlayer.instance != null)
        {
            BGMPlayer.instance.SetVolume(value);
            Debug.Log($"BGM 볼륨 변경: {value}");
        }
    }

    private void OnSFXVolumeChanged(float value)
    {
        if (SFXManager.instance != null)
        {
            SFXManager.instance.SetVolume(value);
            Debug.Log($"SFX 볼륨 변경: {value}");
        }
    }

    private void ClosePanel()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        Debug.Log("설정 패널 닫힘");
    }

    private void Retry()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        UIManager.Instance.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ExitStage()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        UIManager.Instance.LoadScene("MainScene");
    }

    private void QuitGame()
    {
        Debug.Log("게임 종료");
#if UNITY_EDITOR //에디터 환경에서 종료된 거 처럼 보이게
        UnityEditor.EditorApplication.isPlaying = false;
#else //그 외는 종료
        Application.Quit();
#endif
    }
}
