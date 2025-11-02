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
    public float baseGroundspeed { get; private set; }

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

    public void StartGame()//현재 그라운드 무드에서 호출중; 추후 플레이어쪽 또는 게임매니저를 통해 로직변경해야함, 씬이 시작될시 호출하게.
    {
        IsPlaying = true;
        score = 0;
        baseGroundspeed = 5f;
        groundSpeed = baseGroundspeed;
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
    public void SpeedUp(float speed)
    {
        baseGroundspeed += speed;
        groundSpeed = baseGroundspeed;
    }
    public void BoostSpeed(float speed, float count)
    {
        StartCoroutine(SpeedBoost(speed, count));
    }

    private IEnumerator SpeedBoost(float speed, float count)
    {
        groundSpeed = baseGroundspeed + speed;
        yield return new WaitForSeconds(count);
        groundSpeed = baseGroundspeed;
    }
}
