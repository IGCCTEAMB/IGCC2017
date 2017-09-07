using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodThrow : MonoBehaviour {

    // food prefab
    public GameObject food;

    // 発射点
    public Transform muzzle;

    // 射出速度
    public float speed = 500;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // z キーが押された時
        if (Input.GetKeyDown(KeyCode.Z))
        {
            // 食べ物の複製
            GameObject foods = GameObject.Instantiate(food) as GameObject;

            Physics.IgnoreCollision(foods.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
            Physics.IgnoreCollision(foods.GetComponent<Collider>(), gameObject.GetComponent<CharacterController>());

            Vector3 force;
            force = this.gameObject.transform.forward * speed;
            // Rigidbodyに力を加えて発射
            foods.GetComponent<Rigidbody>().AddForce(force);
            // 位置を調整
            foods.transform.position = muzzle.position;
            // 向きを調整
            foods.transform.rotation = muzzle.rotation;
        }
    }
}
