using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMove : MonoBehaviour
{
    private float speedUp;
    private int speed;
    Transform groundTran;
    // Start is called before the first frame update
    void Start()
    {
        groundTran = GetComponent<Transform>();
        GameManager.instance.SetGroundSpeed(5);//초기 스피드 지정
    }

    // Update is called once per frame
    void Update()
    {
        groundTran.position -= new Vector3(GameManager.instance.groundSpeed * Time.deltaTime, 0, 0);
        speedUp += Time.deltaTime;
        if(speedUp >= 20f)
        {
            GameManager.instance.SetGroundSpeed(0.5f);
            speedUp = 0;
            Debug.Log("속도증가");
        }
    }
}
