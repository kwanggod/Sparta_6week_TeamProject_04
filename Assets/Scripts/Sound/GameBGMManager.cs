using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBGMManager : MonoBehaviour
{
    [SerializeField] private AudioClip gameBGM;
    void Start()
    {
        BGMManager.instance.PlayBGM(gameBGM);
    }

}
