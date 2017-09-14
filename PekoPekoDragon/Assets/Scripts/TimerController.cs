using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    Text timer;
    public float time = 180.0f;

    // Use this for initialization
    void Start()
    {
        timer = GetComponent<Text>();
        DrawTimer();

        // カウントダウンを開始する
        GameManager.Instance.SetCurrentState(GameState.Prepare);
    }

    // Update is called once per frame
    void Update()
    {
        // プレイ中じゃなければ処理しない
        if (GameManager.Instance.GetGameState() != GameState.Playing)
            return;

        // 値がマイナスにならないようにする
        time -= Time.deltaTime;

        if(time < 0)
        {
            time = 0;

            DrawResult();
        }

        DrawTimer();
    }

    void DrawResult()
    {

    }

    void DrawTimer()
    {
        // 時間表示
        if (time / 60 < 10)
        {
            timer.text = "0" + ((int)(time) / 60).ToString();
        }
        else
        {
            timer.text = ((int)(time) / 60).ToString();
        }
        timer.text += " : ";
        if (time % 60 < 10)
        {
            timer.text += "0" + ((int)(time) % 60).ToString();
        }
        else
        {
            timer.text += ((int)(time) % 60).ToString();
        }
    }
}
