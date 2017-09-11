using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{

    //コントローラ取得
    GamepadInput.GamePad.Index playerNo;

    //キャラクターコントローラー
    private CharacterController controller;

    //プレイヤーナンバー
    public int PlayerID = 1;

    //速さ
    public float speed = 1.0f;

    //回転速度
    public float rotatespeed = 25.0f;

    //移動量
    float moveX = 0f;
    float moveZ = 0f;
    Vector2 axis;

    //なつき度
    float loveRate = 0;

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
                break;
        }
    }

    void Update()
    {

        //キー情報取得
        //keyState = GamepadInput.GamePad.GetState(playerNo, false);
        axis = GamepadInput.GamePad.GetAxis(GamepadInput.GamePad.Axis.LeftStick, playerNo, false);

        //moveX = Input.GetAxis("Horizontal") * speed;
        //moveZ = Input.GetAxis("Vertical") * speed;
        moveX = axis.x * speed;
        moveZ = axis.y * speed;
        Vector3 direction = new Vector3(moveX, 0, moveZ);

        float step = rotatespeed + Time.deltaTime;

        controller.SimpleMove(direction);

        if (axis.x * axis.x > 0.2f || axis.y * axis.y > 0.2f)
        {
            gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, Quaternion.LookRotation(direction), step);
        }


    }
}

//


//        if (Input.GetKey(KeyCode.RightArrow))
