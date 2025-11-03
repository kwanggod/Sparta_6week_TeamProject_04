using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIController : BaseUIButtonController
{
    [Header("게임 UI 컨트롤러")]
    [SerializeField] private Slider hpBar;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Button pauseButton;

    [Header("모바일 전용 UI")]
    [SerializeField] private Button jumpButton;
    [SerializeField] private Button slideButton;

    private PlayerMove playerMove;

    private void Start()
    {
        playerMove = FindObjectOfType<PlayerMove>();

        RegisterButton(pauseButton, OnPauseButtonPressed);


#if true //UNITY_ANDROID || UNITY_IOS // 모바일 플랫폼일 경우에만 점프 및 슬라이드 버튼 등록
        {
            jumpButton.gameObject.SetActive(true);
            slideButton.gameObject.SetActive(true);
            RegisterButton(jumpButton, playerMove.TryJump);
            RegisterButton(slideButton, playerMove.TrySlide);
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
        float ratio = (float)playerMove.currentHp / playerMove.maxHp;
        hpBar.value = ratio;

        if (GameManager.instance != null)
        {
            scoreText.text = GameManager.instance.score.ToString("N0");
        }
    }

    private void OnPauseButtonPressed()
    {
        Time.timeScale = 0f;
        UIManager.Instance.ShowInGameOption();
        Debug.Log("게임 일시정지");
    }
}
