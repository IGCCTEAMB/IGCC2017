using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBullet : MonoBehaviour
{
    //接触時に呼ばれるコールバック
    void OnCollisionEnter(Collision hit)
    {
        //接触対象はPlayerタグですか？
        if (hit.gameObject.tag == "Enemy")
        {
            // このコンポーネントを持つGameObjectを破棄する
            Destroy(this.gameObject);
        }
    }
}
