using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    //アニメーター
    public Animator animator;

    //コントローラ取得
    GamepadInput.GamePad.Index playerNo;

    //キー情報取得
    GamepadInput.GamepadState keyState;

    //キャラクターコントローラー
    private CharacterController controller;

    //プレイヤーナンバー
    public int PlayerID = 1;

    //速さ
    public float speed = 1.0f;

    //回転速度
    public float rotatespeed = 25.0f;

    //移動量
    public float moveX = 0f;
    public float moveZ = 0f;
    Vector2 axis;

    //生命力
    public int HP = 3;

    //なつき度
    float loveRate = 0;

    //ジャンプ
    

    void Start()
    {
        // コンポーネントの取得
        controller = GetComponent<CharacterController>();

        //コントローラ設定
        switch(PlayerID)
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

    void Update()
    {

        //キー情報取得
        keyState = GamepadInput.GamePad.GetState(playerNo, false);
        axis = GamepadInput.GamePad.GetAxis(GamepadInput.GamePad.Axis.LeftStick, playerNo, false);

        //移動

        //計算
        moveX = axis.x * speed;
        moveZ = axis.y * speed;

        Vector3 direction = new Vector3(moveX, 0.0f, moveZ);

        controller.Move(direction * Time.deltaTime);

        //回転
        float step = rotatespeed + Time.deltaTime;

        if (axis.x * axis.x > 0.0f || axis.y * axis.y > 0.0f)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }

        if (axis.x * axis.x > 0.2f || axis.y * axis.y > 0.2f)
        {
            gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, Quaternion.LookRotation(new Vector3(direction.x,0f, direction.z)), step);
        }

        if (HP < 1)
        {
            HP = 3;
        }



    }
}

//


//        if (Input.GetKey(KeyCode.RightArrow))
//moveX = Input.GetAxis("Horizontal") * speed;
//moveZ = Input.GetAxis("Vertical") * speed;
/*
    public float moveY = 0f;
    public bool jumpflag = false;
    
   if (jumpflag)
   moveY -= 9.8f * Time.deltaTime; //重力
   if (gameObject.transform.position.y < 0.68f) jumpflag = false;
               moveY += 20;
        //ジャンプ
        if (!jumpflag)
        {
            moveY = 0f;
            if (keyState.A)
            {
                jumpflag = true;
                moveY = 5f;
            }
        }

                    hit.gameObject.GetComponent<Player>().moveY += 5;
                hit.gameObject.GetComponent<Player>().jumpflag = true;
     */
