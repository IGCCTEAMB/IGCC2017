using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    //消えるまでの時間
    public float deletetime = 60;


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //時間減らす
        deletetime--;

        //消す
        if (deletetime == 0)
        {
            Destroy(this.gameObject);
        }

    }
}
