using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFXController : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip slideClip;
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private AudioClip dieClip;

    public void PlayJumpSFX()
    {
        if (SFXManager.instance != null && jumpClip != null)
            SFXManager.instance.PlaySFX(jumpClip);
    }

    public void PlaySlideSFX()
    {
        if (SFXManager.instance != null && slideClip != null)
            SFXManager.instance.PlaySFX(slideClip);
    }

    public void PlayHitSFX()
    {
        if (SFXManager.instance != null && hitClip != null)
            SFXManager.instance.PlaySFX(hitClip);
    }

    public void PlayDieSFX()
    {
        if (SFXManager.instance != null && dieClip != null)
            SFXManager.instance.PlaySFX(dieClip);
    }

}
