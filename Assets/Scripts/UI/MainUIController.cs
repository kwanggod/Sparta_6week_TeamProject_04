using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor.SceneManagement;

public class MainUIController : BaseUIButtonController
{
    [Header("Stage Buttons")]
    [SerializeField] private Button stage1Button;
    [SerializeField] private Button stage2Button;
    [SerializeField] private Button stage3Button;

    [Header("Best Score Text")]
    [SerializeField] private TextMeshProUGUI Stage1bestScoreText;
    [SerializeField] private TextMeshProUGUI Stage2bestScoreText;
    [SerializeField] private TextMeshProUGUI Stage3bestScoreText;

    [SerializeField] private Button optionButton;

    private void Start()
    {
        RegisterButton(stage1Button, () => OnStageButtonPressed("Stage1Scene"));
        RegisterButton(stage2Button, () => OnStageButtonPressed("Stage2Scene"));
        RegisterButton(stage3Button, () => OnStageButtonPressed("Stage3Scene"));
        RegisterButton(optionButton, OnOptionPressed);

        UpdateBestScoreUI();
    }

    private void OnStageButtonPressed(string sceneName) => SceneManager.LoadScene(sceneName);

    private void OnOptionPressed() => UIManager.Instance.ShowLobbyOption();

    private void UpdateBestScoreUI()
    {
        var bestScore = GameManager.instance.GetBestScores();

        int stage1Score = bestScore.ContainsKey("Stage1Scene") ? bestScore["Stage1Scene"] : 0;
        Stage1bestScoreText.text = $"최고점수 {stage1Score}";


        int stage2Score = bestScore.ContainsKey("Stage2Scene") ? bestScore["Stage2Scene"] : 0;
        Stage2bestScoreText.text = $"최고점수 {stage2Score}";


        int stage3Score = bestScore.ContainsKey("Stage3Scene") ? bestScore["Stage3Scene"] : 0;
        Stage3bestScoreText.text = $"최고점수 {stage3Score}";

    }
}
