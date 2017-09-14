using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    //プレイヤーナンバー
    public int PlayerID = 0;

    //消えるまでの時間
    public float deletetime = 60;

    //public GameObject explodePrefab;

    //パワーアップ
    public bool powerUp = false;

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

    //接触時に呼ばれるコールバック
    void OnCollisionEnter(Collision hit)
    {
        //接触対象はPlayerタグですか？
        if (hit.gameObject.tag == "Player")
        {
            if (this.PlayerID != hit.gameObject.GetComponent<Player>().PlayerID)
            {
                if (powerUp)
                {
                    hit.gameObject.GetComponent<Player>().HP = 0;
                }
                else
                {
                    hit.gameObject.GetComponent<Player>().HP--;
                    //爆発を呼ぶ
                    //GameObject go = Instantiate(explodePrefab, gameObject.transform.position, Quaternion.identity);
                    // このコンポーネントを持つGameObjectを破棄する
                    Destroy(this.gameObject);
                }
            }
        }
        else if (hit.gameObject.tag == "Dragon")
        {

            if(hit.gameObject.GetComponent<DragonBehaviour>().DragonMoodState == DragonBehaviour.MoodState.BAD)
            {
                hit.gameObject.transform.GetChild(0).GetComponent<DragonDetector>().regulateMoodValue(2);
            }
            else
            {
                hit.gameObject.transform.GetChild(0).GetComponent<DragonDetector>().regulateMoodValue(-2);
            }
            // このコンポーネントを持つGameObjectを破棄する
            Destroy(this.gameObject);
        }
        // このコンポーネントを持つGameObjectを破棄する
        Destroy(this.gameObject);
    }

}
