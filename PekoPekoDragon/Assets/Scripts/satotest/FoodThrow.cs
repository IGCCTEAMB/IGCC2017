﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodThrow : MonoBehaviour {

    // food prefab
    public GameObject food;

    //コントローラ取得
    GamepadInput.GamePad.Index playerNo;
    GamepadInput.GamepadState keyState;

    //トリガー処理
    bool trigger = true;

    //プレイヤーナンバー
    int PlayerID = 1;

    //フード所持
    public bool foodhave = false;

    // 発射点
    public Transform muzzle;

    // 射出速度
    public float speed = 500;

    // Use this for initialization
    void Start()
    {
        //コントローラ設定

        PlayerID = this.gameObject.GetComponent<Player>().PlayerID;
        switch (PlayerID)
        {
            case 1:
                playerNo = GamepadInput.GamePad.Index.One;
                break;
            case 2:
                playerNo = GamepadInput.GamePad.Index.Two;
                break;
            case 3:
                playerNo = GamepadInput.GamePad.Index.Three;
                break;
            case 4:
                playerNo = GamepadInput.GamePad.Index.Four;
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

        //キー情報取得
        keyState = GamepadInput.GamePad.GetState(playerNo, false);


        //キーが押された時
        //if (Input.GetKeyDown(KeyCode.Z))
        if ((keyState.Y || keyState.LeftTrigger > 0.7f) && trigger == false && foodhave == true)
        {
            trigger = true;
            foodhave = false;

            // 食べ物の複製
            GameObject foods = GameObject.Instantiate(food) as GameObject;

            //いろいろあたり判定を無視する
            Physics.IgnoreCollision(foods.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
            Physics.IgnoreCollision(foods.GetComponent<Collider>(), gameObject.GetComponent<CharacterController>());

            //エサに自分のタグをつける
            foods.GetComponent<Food>().PlayerID = this.PlayerID;

            Vector3 force;
            force = this.gameObject.transform.forward * speed;
            // Rigidbodyに力を加えて発射
            foods.GetComponent<Rigidbody>().AddForce(force);
            // 位置を調整
            foods.transform.position = muzzle.position;
            // 向きを調整
            foods.transform.rotation = muzzle.rotation;
        }
        else if (!keyState.Y && keyState.LeftTrigger < 0.7f && trigger == true)
        {
            trigger = false;
        }
    }
}
