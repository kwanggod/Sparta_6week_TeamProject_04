using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyBGMPlayer : MonoBehaviour
{
    [Header("로비 BGM 설정")]
    [SerializeField] private AudioClip lobbyBGM;

    [Range(0f, 1f)][SerializeField] private float volume = 0.3f;
    [Range(0.1f, 3f)][SerializeField] private float pitch = 1f;
    void Start()
    {
        if (BGMPlayer.instance != null)
        {
            BGMPlayer.instance.PlayBGM(lobbyBGM);
            BGMPlayer.instance.SetVolume(volume);
            BGMPlayer.instance.SetPitch(pitch);
        }
    }
}
