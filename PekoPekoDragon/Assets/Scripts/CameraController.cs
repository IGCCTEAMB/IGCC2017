using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float baseDistance = 5f;       // 停止時のカメラ―プレイヤー間の距離[m]
    public float baseCameraHeight = 2f;   // 停止時のカメラの高さ[m]
    public float chaseDamper = 3f;        // カメラの追跡スピード（追跡時のカメラ―プレイヤー間の距離がきまる）
    public GameObject dragon;
    private Camera cam;

    public float offset;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        // カメラの位置を設定
        var desiredPos = dragon.transform.position - dragon.transform.forward * baseDistance + Vector3.up * baseCameraHeight;
        cam.transform.position = Vector3.Lerp(cam.transform.position, desiredPos, Time.deltaTime * chaseDamper);

        cam.transform.position = dragon.transform.position - new Vector3(0, 0, baseDistance) + new Vector3(0, baseCameraHeight, 0);

        CalcScreenOff();  
    }

    void CalcScreenOff()
    {
        float width = Screen.width / 2.0f;
        float height = Screen.height / 2.0f;

        for (int i = 0; i < 4; i++)
        {
            Vector3 distance = dragon.transform.position - GameManager.Instance.players[i].transform.position;
            if(Mathf.Abs(distance.x) > width + offset)
            {
                // プレイヤーを殺す

            }
            else if(Mathf.Abs(distance.z) > height + offset)
            {
                // プレイヤーを殺す

            }
        }
    }
}