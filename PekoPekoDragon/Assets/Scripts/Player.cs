using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
	public float speed = 1.0f;
    public float rotatespeed = 5.0f;
    float moveX = 0f;
    float moveZ = 0f;

    private CharacterController controller;

    void Start()
    {
        // コンポーネントの取得
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {

        moveX = Input.GetAxis("Horizontal") * speed;
        moveZ = Input.GetAxis("Vertical") * speed;
        Vector3 direction = new Vector3(moveX, 0, moveZ);

        float step = rotatespeed + Time.deltaTime;

        controller.SimpleMove(direction);

        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
        {
            gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, Quaternion.Euler(0.0f, 45.0f, 0.0f), step);
        }
        else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, Quaternion.Euler(0.0f, 135.0f, 0.0f), step);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
        {
            gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, Quaternion.Euler(0.0f, 315.0f, 0.0f), step);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, Quaternion.Euler(0.0f, 225.0f, 0.0f), step);
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.rotation =Quaternion.RotateTowards(gameObject.transform.rotation, Quaternion.Euler(0.0f, 90.0f, 0.0f), step);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, Quaternion.Euler(0.0f, 0.0f, 0.0f), step);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, Quaternion.Euler(0.0f, 270.0f, 0.0f), step);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, Quaternion.Euler(0.0f, 180.0f, 0.0f), step);
        }
    }

}

//        if (Input.GetKey(KeyCode.RightArrow))
