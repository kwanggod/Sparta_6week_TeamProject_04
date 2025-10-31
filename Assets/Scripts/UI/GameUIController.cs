using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : BaseUIButtonController
{
    [Header("게임 UI 컨트롤러")]
    [SerializeField] private Slider hpBar;
    [SerializeField] private Text scoreText;
    [SerializeField] private Button pauseButton;

    [Header("모바일 전용 UI")]
    [SerializeField] private Button jumpButton;
    [SerializeField] private Button slideButton;

    private PlayerMove playerMove;

    private void Start()
    {
        playerMove = FindObjectOfType<PlayerMove>();

        RegisterButton(pauseButton, OnPauseButtonPressed);

#if UNITY_ANDROID || UNITY_IOS // 모바일 플랫폼일 경우에만 점프 및 슬라이드 버튼 등록
        {
            jumpButton.gameObject.SetActive(true);
            slideButton.gameObject.SetActive(true);
            RegisterButton(jumpButton, player.StartJump);
            RegisterButton(slideButton, player.Startslide);
        }

#else
        {
            jumpButton.gameObject.SetActive(false);
            slideButton.gameObject.SetActive(false);
        }
#endif
    }

    private void Update()
    {
        // HP 바와 점수 텍스트 업데이트 로직은 여기에 추가
    }

    private void OnPauseButtonPressed()
    {
        Time.timeScale = 0f;
        Debug.Log("게임 일시정지");
    }
}
