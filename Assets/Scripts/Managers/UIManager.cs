using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static UIManager Instance { get; private set; }
    [SerializeField] private GameObject optionPanel;

    private void Awake()
    {
        // DDOL 싱글톤 패턴 구현
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(optionPanel);
    }

    // 씬 전환 유틸
    public void LoadScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName)) return;
        Debug.Log($"{sceneName} 버튼 클릭됨");
        SceneManager.LoadScene(sceneName);
    }

    public void ShowOptionPanel()
    {
        if (optionPanel == null)
        {
            Debug.LogWarning("옵션 패널이 Null입니다!");
            return;
        }
        optionPanel.SetActive(true);
    }

    public void HideOptionPanel()
    {
        if (optionPanel == null) return;
        optionPanel.SetActive(false);
    }
}

