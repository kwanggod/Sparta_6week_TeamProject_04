using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultUIController : BaseUIButtonController
{
    [Header("Buttons")]
    [SerializeField] private Button retryButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button optionButton;

    [Header("Score Text")]
    [SerializeField] private TextMeshProUGUI CurrnetscoreText;
    [SerializeField] private TextMeshProUGUI HighscoreText;

    private void Start()
    {
        RegisterButton(retryButton, OnRetryPressed);
        RegisterButton(exitButton, OnExitPressed);
        RegisterButton(optionButton, OnOptionPressed);

        UpdateBestScoreInResult();
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

    private void UpdateBestScoreInResult()
    {
        string lastSceneName = GameManager.instance.GetLastPlayedScene();

        int currentScore = GameManager.instance.score;
        var bestScores = GameManager.instance.GetBestScores(); //씬이름과 최고점수를 딕셔너리에서 가져옴

        int bestScore = bestScores.ContainsKey(lastSceneName) ? bestScores[lastSceneName] : 0;

        CurrnetscoreText.text = $"{currentScore}";
        HighscoreText.text = $"{bestScore}";
    }
}
