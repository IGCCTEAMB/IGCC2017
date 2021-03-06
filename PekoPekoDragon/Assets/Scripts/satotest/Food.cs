﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    //プレイヤーナンバー
    public int PlayerID = 0;

    //消えるまでの時間
    public float deletetime = 60;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //時間減らす
        deletetime--;
        //消す
        if (deletetime == 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision hit)
    {
        // 接触対象はDragonタグですか？
        if (hit.gameObject.tag == "Dragon" && PlayerID != 0)
        {
            if(hit.gameObject.GetComponent<DragonBehaviour>().DragonMoodState == DragonBehaviour.MoodState.BAD)
            {
                hit.gameObject.transform.GetChild(0).GetComponent<DragonDetector>().regulateMoodValue(-20);
            }
            else
            {
                hit.gameObject.transform.GetChild(0).GetComponent<DragonDetector>().CalcMoodValue();
            }

            // 全てのプレイヤーを取得
            GameObject[] players = GameManager.Instance.players;
            
            for (int i = 0; i < 4; i++)
            {
                // 餌を投げたプレイヤー
                if(i == PlayerID - 1)
                {
                    // なつき度を増やす
                    players[i].GetComponent<Player>().LoveRate += 15;
                }
                else
                {
                    // なつき度を減らす
                    players[i].GetComponent<Player>().LoveRate -= 2;
                }

                Mathf.Clamp(players[i].GetComponent<Player>().LoveRate, 0, 100);
            }
            // このコンポーネントを持つGameObjectを破棄する
            Destroy(this.gameObject);
        }
    }
}
