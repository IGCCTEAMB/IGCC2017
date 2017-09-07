using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    const int PLAYER_NUM = 4;
    public float CAMERA_DISTANCE = -6.0f;

    GameObject[] player;
    Transform[] target;
    [SerializeField]
    Vector3 offset;
    float screenAspect;
    Camera camera;
    public float scale = 1.5f;
    public float min = 5.0f;
    public float max = 13.0f;

    // Use this for initialization
    void Start ()
    {
        // 4人分作る
        player = new GameObject[PLAYER_NUM];
        for (int i = 0; i < PLAYER_NUM; i++)
        {
            // プレイヤーを読み込む
            string name = "Player" + (i + 1).ToString();
            player[i] = GameObject.FindWithTag(name);
        }
        target = new Transform[2];
        screenAspect = (float)Screen.height / Screen.width;
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update ()
    {
        UpdateCameraPosition();
        UpdateOrthographicSize();
    }

    void UpdateCameraPosition()
    {
        float maxDistance = 0.0f;

        int i = 0;
        int j = 1;
        for (i = 0; i < PLAYER_NUM - 1; i++)
        {
            for (j = i + 1; j < PLAYER_NUM; j++)
            {
                // ２点間の距離の絶対値を求める
                float distance = Vector3.Distance(player[i].transform.position, player[j].transform.position);
                distance = Mathf.Abs(distance);

                // 一番遠い距離を求める
                if (distance > maxDistance)
                {
                    maxDistance = distance;

                    target[0] = player[i].transform;
                    target[1] = player[j].transform;
                }
            }
        }

        // maxDistanceによってズームの大きさを変える
        maxDistance = Mathf.Clamp(maxDistance, min, max);

        // 2点間の中心点からカメラの位置を更新
        Vector3 center = Vector3.Lerp(target[0].position, target[1].position, 0.5f);
        Vector3 nowPos = (center - gameObject.transform.position) * 0.01f;
        nowPos.y = maxDistance * scale;
        transform.position = nowPos + new Vector3(0.0f, 0.0f, CAMERA_DISTANCE);
    }

    void UpdateOrthographicSize()
    {
        // ２点間のベクトルを取得
        Vector3 targetsVector = AbsPositionDiff(target[0], target[1]) + (Vector3)offset;

        // アスペクト比が縦長ならzの半分、横長ならxとアスペクト比でカメラのサイズを更新
        float targetsAspect = targetsVector.z / targetsVector.x;
        float targetOrthographicSize = 0;
        if (screenAspect < targetsAspect)
        {
            targetOrthographicSize = targetsVector.z * 0.5f;
        }
        else
        {
            targetOrthographicSize = targetsVector.x * (1 / camera.aspect) * 0.5f;
        }
        camera.orthographicSize = targetOrthographicSize;
    }

    Vector3 AbsPositionDiff(Transform target1, Transform target2)
    {
        Vector3 targetsDiff = target1.position - target2.position;
        return new Vector3(Mathf.Abs(targetsDiff.x), Mathf.Abs(targetsDiff.y), Mathf.Abs(targetsDiff.z));
    }
}
