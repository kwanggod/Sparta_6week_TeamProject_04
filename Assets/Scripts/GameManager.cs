using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; } //현재 클래스를 싱글톤
    public bool IsPlaying { get; private set; } //게임 진행 여부
    public int score { get; private set; } //점수

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); //씬 전환시 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); //이미 인스턴스가 존재하면 새로 생성된 오브젝트 파괴
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
}
