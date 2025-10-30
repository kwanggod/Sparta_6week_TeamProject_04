using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance { get; private set; } //현재 클래스를 싱글톤
    private AudioSource sfxSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); //씬 전환시 파괴되지 않도록 설정
            sfxSource = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject); //이미 인스턴스가 존재하면 새로 생성된 오브젝트 파괴
        }
    }

    public void PlaySFX(AudioClip clip, float vol = 1f)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip, Mathf.Clamp01(vol));
    }

    public void PlaySound() => PlaySFX(null); //사운드 재생 테스트
}
