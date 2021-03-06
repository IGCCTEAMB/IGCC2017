﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour {

    [SerializeField]
    private float destroyIn;

	// Use this for initialization
	void Start () {

        //GameObject go = GameObject.FindGameObjectWithTag("Player");
        //Physics.IgnoreCollision(go.GetComponent<CharacterController>(), gameObject.GetComponent<Collider>());
        Destroy(gameObject, destroyIn);
  }

    // Update is called once per frame
    void Update () {

        transform.Rotate(new Vector3(0.0f,45.0f,0.0f) * Time.deltaTime);

    }

    //接触時に呼ばれるコールバック
    void OnCollisionEnter(Collision hit)
    {

        //接触対象はPlayerタグですか？
        if (hit.gameObject.tag == "Player")
        {

            //これは食べ物ですか
            if (gameObject.tag == "FoodItem")
            {
                //食べ物Get
                if (!hit.gameObject.GetComponent<FoodThrow>().foodhave)
                    hit.gameObject.GetComponent<FoodThrow>().foodhave = true;
            }
            else if (gameObject.tag == "WeaponItem")
            {
                //武器Get
                if (!hit.gameObject.GetComponent<Attack>().powerUp)
                    hit.gameObject.GetComponent<Attack>().powerUp = true;
            }

            // このコンポーネントを持つGameObjectを破棄する
            Destroy(this.gameObject);
        }
        else
        {
            Physics.IgnoreCollision(hit.gameObject.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
        }
    }

}
