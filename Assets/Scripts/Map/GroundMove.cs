using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundMove : MonoBehaviour
{
    private Transform groundTran;
    private float speedUp;

    void Start()
    {
        groundTran = transform;
    }

    void Update()
    {
        groundTran.position -= new Vector3(GameManager.instance.groundSpeed * Time.deltaTime, 0, 0);
        speedUp += Time.deltaTime;

        if (speedUp >= 10f)
        {
            GameManager.instance.SpeedUp(0.2f);
            speedUp = 0;
        }
        if(transform.position.x <= -120f)
        {
            gameObject.SetActive(false);
        }
    }
}
