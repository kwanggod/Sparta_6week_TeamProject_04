using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : BaseUIButtonController
{
    [Header("소리 설정 슬라이더")]
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    [Header("버튼")]
    [SerializeField] private Button closeButton;
    [SerializeField] private Button panelCloseButton;
    [SerializeField] private Button exitButton;

    void Start()
    {
        if (BGMManager.instance != null)
            bgmSlider.value = BGMManager.instance.GetVolume();

        if (SFXManager.instance != null)
            sfxSlider.value = SFXManager.instance.GetVolume();

        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);

        RegisterButton(closeButton, ClosePanel);
        RegisterButton(panelCloseButton, ClosePanel);
        RegisterButton(exitButton, ExitGame);

    }

    private void OnBGMVolumeChanged(float value)
    {
        if (BGMManager.instance != null)
        {
            BGMManager.instance.SetVolume(value);
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
        gameObject.SetActive(false);
        Debug.Log("설정 패널 닫힘");
    }

    private void ExitGame()
    {
        Debug.Log("게임 종료");
#if UNITY_EDITOR //에디터 환경에서 종료된 거 처럼 보이게
        UnityEditor.EditorApplication.isPlaying = false;
#else //그 외는 종료
        Application.Quit();
#endif
    }
}
