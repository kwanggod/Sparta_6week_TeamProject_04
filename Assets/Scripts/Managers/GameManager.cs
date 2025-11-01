using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; } //현재 클래스를 싱글톤
    public bool IsPlaying { get; private set; } //게임 진행 여부
    public int score { get; private set; } //점수

    public float groundSpeed { get; private set; }

    [SerializeField] private GameObject uiPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); //씬 전환시 파괴되지 않도록 설정

            SceneManager.sceneLoaded += OnSceneLoaded; //씬 로드 이벤트 등록
        }
        else
        {
            Destroy(gameObject); //이미 인스턴스가 존재하면 새로 생성된 오브젝트 파괴
        }
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        if (scene.name.StartsWith("Stage"))
        {
            if (FindObjectOfType<GameUIController>() == null)
            {
                Instantiate(uiPrefab);
            }
        }
        else
        {
            var existingUI = FindObjectOfType<GameUIController>();
            if (existingUI != null)
            {
                Destroy(existingUI.gameObject);
                Debug.Log("게임 UI 컨트롤러 제거됨");
            }
        }
    }

    public void StartGame()
    {
        IsPlaying = true;
        score = 0;
    }

    public void AddScore(int amount)
    {
        score += amount;
    }

    public void EndGame()
    {
        IsPlaying = false;
    }
    public void SetGroundSpeed(float speed)
    {
        groundSpeed += speed;
    }
    public void BoostSpeed(float speed, float count)
    {
        StartCoroutine(SpeedBoost(speed, count));
    }

    private IEnumerator SpeedBoost(float speed, float count)
    {
        groundSpeed += speed;
        yield return new WaitForSeconds(count);
        groundSpeed -= speed;
    }
}
