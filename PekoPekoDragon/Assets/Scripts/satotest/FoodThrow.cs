using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodThrow : MonoBehaviour {

    //アニメーター
    Animator animator;

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
    bool foodTrigger = false;
    GameObject foods;

    // 発射点
    public Transform muzzle;

    // 射出速度
    public float speed = 500;

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

        //キー情報取得
        keyState = GamepadInput.GamePad.GetState(playerNo, false);

        //アニメーション
        if (foodhave)
        {
           if (!foodTrigger)
            {
                // 食べ物の複製
                foods = GameObject.Instantiate(food) as GameObject;

                //いろいろあたり判定を無視する
                Physics.IgnoreCollision(foods.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
                Physics.IgnoreCollision(foods.GetComponent<Collider>(), gameObject.GetComponent<CharacterController>());
                foodTrigger = true;
            }
            animator.SetBool("Hold", true);

            //持ち上げるやーつ
            // 位置を調整
            foods.transform.position = muzzle.position;
            // 向きを調整
            foods.transform.rotation = muzzle.rotation;
            foods.GetComponent<Food>().deletetime++;

            //スンッってならないやつ
            foods.gameObject.GetComponent<Rigidbody>().useGravity = false;

            //キーが押された時
            //if (Input.GetKeyDown(KeyCode.Z))
            if ((keyState.A || keyState.LeftTrigger > 0.7f) && trigger == false)
            {
                
                //アニメーション
                animator.SetBool("Hold", false);

                trigger = true;
                foodhave = false;

                // Rigidbodyに力を加えて発射
                //スンッってならないやつ
                foods.gameObject.GetComponent<Rigidbody>().useGravity = true;
                Vector3 force;
                force = this.gameObject.transform.forward * speed;
                foods.GetComponent<Rigidbody>().AddForce(force);

                //エサに自分のタグをつける
                foods.GetComponent<Food>().PlayerID = this.PlayerID;
                foodTrigger = false;
            }
        }
        if (!keyState.A && keyState.LeftTrigger < 0.7f && trigger == true)
        {
            trigger = false;
        }
    }
}
