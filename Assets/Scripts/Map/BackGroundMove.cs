using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        offsetX += GameManager.instance.groundSpeed*0.0001f;
        rend.material.mainTextureOffset = new Vector2(offsetX * Time.deltaTime, 0);
    }
}
