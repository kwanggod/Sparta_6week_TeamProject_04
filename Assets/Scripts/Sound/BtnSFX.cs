using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnSFX : MonoBehaviour
{
    [Header("버튼 클릭 사운드 설정")]
    [SerializeField] private AudioClip btnSFX;
    [Range(0f, 1f)][SerializeField] private float volume = 1f;

    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(PlayClickSound);
    }

    private void PlayClickSound()
    {
        if (SFXManager.instance != null)
        {
            SFXManager.instance.PlaySFX(btnSFX, volume);
        }
    }
}
