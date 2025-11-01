using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSFX : MonoBehaviour
{
    [Header("아이템 획득 사운드 설정")]
    [SerializeField] private AudioClip GetItemSFX;
    [Range(0f, 1f)][SerializeField] private float volume = 1f;

    public void PlayItemSound()
    {
        if (SFXManager.instance != null && GetItemSFX != null)
        {
            SFXManager.instance.PlaySFX(GetItemSFX, volume);
        }
    }
}
