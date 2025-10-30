using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static BGMManager instance { get; private set; } //현재 클래스를 싱글톤
    private AudioSource bgmSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); //씬 전환시 파괴되지 않도록 설정
            bgmSource = gameObject.AddComponent<AudioSource>();
            bgmSource.loop = true;
        }
        else
        {
            Destroy(gameObject); //이미 인스턴스가 존재하면 새로 생성된 오브젝트 파괴
        }
    }

    public void PlayBGM(AudioClip clip)
    {
        if (bgmSource.clip == clip)
        {
            return;
        } //이미 재생중인 BGM이면 무시

        bgmSource.clip = clip;
        bgmSource.Play();

    }

    public void StopBGM()
    {
        bgmSource.Stop();
        bgmSource.clip = null;
    }

    public void SetVolume(float vol)
    {
        bgmSource.volume = vol;
    }

    public void SetPitch(float pitch)
    {
        bgmSource.pitch = Mathf.Clamp(pitch, 0.1f, 3f);
    }
}
