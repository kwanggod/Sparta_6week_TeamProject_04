using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // ΩÃ±€≈Ê ¿ŒΩ∫≈œΩ∫
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        // DDOL ΩÃ±€≈Ê ∆–≈œ ±∏«ˆ
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // æ¿ ¿¸»Ø ¿Ø∆ø
    public void LoadScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName)) return;
        SceneManager.LoadScene(sceneName);
    }
}

