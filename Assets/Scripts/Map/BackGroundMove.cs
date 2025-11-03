using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackGroundMove : MonoBehaviour
{
    private Renderer rend;
    float offsetX = 0f;
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        offsetX += GameManager.instance.groundSpeed * 0.01f * Time.deltaTime;
        rend.material.mainTextureOffset = new Vector2(offsetX, 0);
    }
}
