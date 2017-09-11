using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCursol : MonoBehaviour
{
    // カーソルの上下の移動量
    public float moveCursol = 54.0f;

    // 選択位置
    int cursolNum;

	// Use this for initialization
	void Start ()
    {
        // 初期の選択位置を上側にする
        cursolNum = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // カーソルを移動
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            int nowNum = cursolNum;
            cursolNum = (nowNum + 1) % 2;

            Vector3 pos = gameObject.transform.position;
            pos.y -= (cursolNum - nowNum) * moveCursol;
            gameObject.transform.position = pos;
        }
	}
}
