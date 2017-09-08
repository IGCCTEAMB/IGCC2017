using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionItem : MonoBehaviour {

    private void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), go.GetComponent<CharacterController>());
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

            }

            // このコンポーネントを持つGameObjectを破棄する
            Destroy(this.gameObject);
        }
    }
}
