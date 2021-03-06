﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    //アニメーター
    Animator animator;

    //コントローラ取得
    GamepadInput.GamePad.Index playerNo;
    GamepadInput.GamepadState keyState;

    //トリガー処理
    bool trigger = true;

    //プレイヤーナンバー
    int PlayerID = 1;

    // Bullet prefab
    public GameObject Bullet;

    public GameObject PowerBullet;

    GameObject attacks;

    // 発射点
    public Transform muzzle;

    // 射出速度
    public float speed = 1000;

    //クールタイム
    public float cooltime = 60;

    float CT = 0;

    //パワーアップ
    public bool powerUp = false;

    public float delayCount = 10;

    //ディレイ
    int delay = 0;
    bool delayTrigger = false;

    // Use this for initialization
    void Start()
    {
    //アニメーター取得
    animator = this.GetComponent<Player>().animator;

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
                playerNo = GamepadInput.GamePad.Index.Any;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CT > 0) CT--;
        //キー情報取得
        keyState = GamepadInput.GamePad.GetState(playerNo, false);

        //キーが押された時
        //if (Input.GetKeyDown(KeyCode.Z))
        if ((keyState.X || keyState.RightTrigger > 0.7f) && trigger == false && CT < 1
            && this.gameObject.GetComponent<FoodThrow>().foodhave == false)
        {

            animator.SetTrigger("AttackTrigger");

            trigger = true;
            CT = cooltime;

            delayTrigger = true;

        }
        else if (!keyState.X && keyState.RightTrigger < 0.7f && trigger == true)
        {
            trigger = false;
        }

        if (delay > delayCount)
        {
            delay = 0;
            delayTrigger = false;

            // 弾の複製

            //パワーアップ
            if (powerUp)
            {
                attacks = GameObject.Instantiate(PowerBullet) as GameObject;
                attacks.GetComponent<Bullet>().powerUp = true;
                powerUp = false;
            }
            else
            {
                attacks = GameObject.Instantiate(Bullet) as GameObject;
            }

            //色々あたり判定を無視する
            Physics.IgnoreCollision(attacks.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
            Physics.IgnoreCollision(attacks.GetComponent<Collider>(), gameObject.GetComponent<CharacterController>());

            //弾に自分のタグをつける
            attacks.GetComponent<Bullet>().PlayerID = this.PlayerID;

            Vector3 force;
            force = this.gameObject.transform.forward * speed;
            // Rigidbodyに力を加えて発射
            attacks.GetComponent<Rigidbody>().AddForce(force);
            //位置を調整
            attacks.transform.position = muzzle.position;
            // 向きを調整
            attacks.transform.rotation = muzzle.rotation;
        }

        if (powerUp)
        {
            gameObject.transform.GetChild(3).gameObject.SetActive(true);
        }
        else
        {
            gameObject.transform.GetChild(3).gameObject.SetActive(false);
        }

        if (delayTrigger)
        {
            delay++;
        }

    }
}

