using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static UIManager Instance { get; private set; }
    [SerializeField] private GameObject optionPanel1;
    [SerializeField] private GameObject optionPanel2;
    [SerializeField] private GameObject uiPrefab;
    private Scene scene;

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
        SceneManager.sceneLoaded += OnSceneLoaded; //씬 로드 이벤트 등록
    }

    // 씬 전환 유틸
    public void LoadScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName)) return;
        Debug.Log($"{sceneName} 버튼 클릭됨");
        SceneManager.LoadScene(sceneName);
    }

    public void ShowOptionPanel1()
    {
        if (optionPanel1 == null) return;
        optionPanel1.SetActive(true);
    }

    public void ShowOptionPanel2()
    {
        if (optionPanel2 == null) return;
        optionPanel2.SetActive(true);
    }

    public void HideOptionPanel1()
    {
        if (optionPanel1 == null) return;
        optionPanel1.SetActive(false);
    }

    public void HideOptionPanel2()
    {
        if (optionPanel2 == null) return;
        optionPanel2.SetActive(false);
    }

    // 씬 로드 시 UI 프리팹 관리
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var existingUI = FindObjectOfType<GameUIController>();

        if (scene.name.StartsWith("Stage"))
        {
            if (existingUI == null)
            {
                Instantiate(uiPrefab);
            }
        }
        else
        {
            if (existingUI != null)
            {
                Destroy(existingUI.gameObject);
                Debug.Log("게임 UI 컨트롤러 제거됨");
            }
        }
    }
}

