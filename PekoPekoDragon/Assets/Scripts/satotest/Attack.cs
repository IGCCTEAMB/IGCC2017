using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    //コントローラ取得
    GamepadInput.GamePad.Index playerNo;
    GamepadInput.GamepadState keyState;

    //トリガー処理
    bool trigger = true;

    // Bullet prefab
    public GameObject Bullet;

    // 発射点
    public Transform muzzle;

    // 射出速度
    public float speed = 1000;

    //クールタイム
    public float cooltime = 60;

    float CT = 0;

    // Use this for initialization
    void Start()
    {
        //コントローラ設定
        playerNo = GamepadInput.GamePad.Index.One;
    }

    // Update is called once per frame
    void Update()
    {
        if (CT > 0) CT--;
        //キー情報取得
        keyState = GamepadInput.GamePad.GetState(playerNo, false);


        //キーが押された時
        //if (Input.GetKeyDown(KeyCode.Z))
        if (keyState.X && trigger == false && CT < 1)
        {
            trigger = true;
            CT = cooltime;

            // 弾の複製
            GameObject attacks = GameObject.Instantiate(Bullet) as GameObject;

            Physics.IgnoreCollision(attacks.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
            Physics.IgnoreCollision(attacks.GetComponent<Collider>(), gameObject.GetComponent<CharacterController>());

            Vector3 force;
            force = this.gameObject.transform.forward * speed;
            // Rigidbodyに力を加えて発射
            attacks.GetComponent<Rigidbody>().AddForce(force);
            //位置を調整
            attacks.transform.position = muzzle.position;
            // 向きを調整
            attacks.transform.rotation = muzzle.rotation;
        }
        else if (!keyState.X && trigger == true)
        {
            trigger = false;
        }
    }
}
