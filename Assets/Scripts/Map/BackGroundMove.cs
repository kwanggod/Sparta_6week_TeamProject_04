using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackGroundMove : MonoBehaviour
{
    private Renderer rend;
    float offsetX = 0f;
    float timer = 0f;
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (GameManager.instance.groundStop == false)
        {
            offsetX += GameManager.instance.groundSpeed * 0.01f * Time.deltaTime;
            rend.material.mainTextureOffset = new Vector2(offsetX, 0);
        }
        else
        {
            Time.timeScale = 0.2f;
            timer += Time.deltaTime;
            if (timer > 0.5f)
                SceneManager.LoadScene("ResultScene");
        }

    }
}
