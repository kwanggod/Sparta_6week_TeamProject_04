using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBGMController : MonoBehaviour
{
    [Header("BGM Clips")]
    [SerializeField] private AudioClip lobbyBGM;
    [SerializeField] private AudioClip stageBGM;
    [SerializeField] private AudioClip resultBGM;

    private string currentSceneName;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void Start()
    {

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != currentSceneName)
        {
            currentSceneName = scene.name;
            PlayBGMForScene(currentSceneName);
        }
    }

    private void PlayBGMForScene(string sceneName)
    {
        if (BGMPlayer.instance == null) return;

        AudioClip bgmToPlay = null;

        if (sceneName == "StartScene" || sceneName == "MainScene")
        {
            bgmToPlay = lobbyBGM;
        }
        else if (sceneName.StartsWith("Stage"))
        {
            bgmToPlay = stageBGM;
        }
        else if (sceneName == "ResultScene")
        {
            bgmToPlay = resultBGM;
        }

        if (bgmToPlay != null)
        {
            BGMPlayer.instance.PlayBGM(bgmToPlay);
        }

    }
}
