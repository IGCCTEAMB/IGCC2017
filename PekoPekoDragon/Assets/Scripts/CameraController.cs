using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    const int PLAYER_NUM = 4;
    public float CAMERA_DISTANCE = -6.0f;

    GameObject[] player;
    // 最小と最大の座標の位置に空のオブジェクトを生成するための変数
    GameObject emptyObject1;
    GameObject emptyObject2;
    // 最小値の座標、最大値の座標を格納する
    Transform[] targets;
    [SerializeField]
    Vector3 offset = new Vector3(0, 0, 2);
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
            player[i] = GameObject.Find(name);
        }
        emptyObject1 = new GameObject("Min GameObject");
        emptyObject2 = new GameObject("Max GameObject");
        targets = new Transform[2];
        targets[0] = emptyObject1.transform;
        targets[1] = emptyObject2.transform;
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
        // 初期化
        emptyObject1.transform.position = Vector3.zero;
        emptyObject2.transform.position = Vector3.zero;
        targets[0] = emptyObject1.transform;
        targets[1] = emptyObject2.transform;

        int i;
        int j;
        for (i = 0; i < PLAYER_NUM - 1; i++)
        {
            for (j = i + 1; j < PLAYER_NUM; j++)
            {
                // XとZの最小値、最大値を求める
                CalcMinAndMax(player[i].transform, player[j].transform);
            }
        }

        emptyObject1.transform.position = targets[0].transform.position;
        emptyObject2.transform.position = targets[1].transform.position;

        // 2点間の中心点からカメラの位置を更新
        Vector3 center = Vector3.Lerp(targets[0].position, targets[1].position, 0.5f);
        transform.position = center + new Vector3(0.0f, 10.0f, CAMERA_DISTANCE);
    }
    
    void CalcMinAndMax(Transform target1, Transform target2)
    {
        Vector3 minPos = targets[0].position;
        // X座標の最小値を求める
        minPos.x = Mathf.Min(minPos.x, target1.position.x);
        minPos.x = Mathf.Min(minPos.x, target2.position.x);
        // Z座標の最小値を求める
        minPos.z = Mathf.Min(minPos.z, target1.position.z);
        minPos.z = Mathf.Min(minPos.z, target2.position.z);
        targets[0].position = minPos;

        Vector3 maxPos = targets[1].position;
        // X座標の最大値を求める
        maxPos.x = Mathf.Max(maxPos.x, target1.position.x);
        maxPos.x = Mathf.Max(maxPos.x, target2.position.x);
        // Z座標の最大値を求める
        maxPos.z = Mathf.Max(maxPos.z, target1.position.z);
        maxPos.z = Mathf.Max(maxPos.z, target2.position.z);
        targets[1].position = maxPos;
    }

    void UpdateOrthographicSize()
    {
        // ２点間のベクトルを取得
        Vector3 targetsVector = AbsPositionDiff(targets[0], targets[1]) + offset;

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
        // Clampでズーム範囲を制御
        camera.orthographicSize = targetOrthographicSize;
    }

    Vector3 AbsPositionDiff(Transform target1, Transform target2)
    {
        Vector3 targetsDiff = target1.position - target2.position;
        return new Vector3(Mathf.Abs(targetsDiff.x), Mathf.Abs(targetsDiff.y), Mathf.Abs(targetsDiff.z));
    }
}
