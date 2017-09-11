using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DragonBehaviour : MonoBehaviour
{

    [SerializeField]
    private GameObject[] waypoints;
    [SerializeField]
    private int num;
    [SerializeField]
    private float minDist;
    [SerializeField]
    private float _minDistWithPlayer = 5;
    [SerializeField]
    private GameObject[] dragonAttacks;

    [SerializeField]
    private GameObject _targetObject;
    [SerializeField]
    private AudioClip[] _fireSounds;

    private NavMeshAgent navMeshAgent;

    private float time;
    public float attackDelay = 2;



    // ご機嫌度
    int moodValue;
    // 最大ご機嫌度
    public int MAX_MOOD_VALUE = 100;

    // Use this for initialization
    void Start()
    {
        navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
        time = 0;

    }

    // Update is called once per frame
    void Update()
    {

        float dist = Vector3.Distance(gameObject.transform.position, waypoints[num].transform.position);
        _targetObject = gameObject.transform.GetChild(0).GetComponent<DragonDetector>().targetObject;

        if (dist > minDist)
        {
            Move();
        }
        else
        {
            num = Random.Range(0, waypoints.Length);
        }
    }


    void Move()
    {
        if (!_targetObject)
        {
            navMeshAgent.SetDestination(waypoints[num].transform.position);
        }
        else
        {

            float dist = Vector3.Distance(gameObject.transform.position, _targetObject.transform.position);

            if (dist < _minDistWithPlayer)
            {

                navMeshAgent.isStopped = true;
                Attack();
                gameObject.transform.LookAt(_targetObject.transform.position);

            }
            else
            {
                navMeshAgent.isStopped = false;

                navMeshAgent.SetDestination(_targetObject.transform.position);

                // ドラゴンがプレイヤーについて行っている時の処理

                // プレイヤーにシールドを張る
                //GiveShield();
            }

        }

    }

    void Attack()
    {
        time++;
        int attackNum = Random.Range(0, dragonAttacks.Length);
        if (time > attackDelay * 60.0f)
        {
            time = 0;
            GameObject par = Instantiate(dragonAttacks[attackNum], gameObject.transform.GetChild(attackNum + 1).transform.position, gameObject.transform.GetChild(attackNum + 1).transform.rotation);
            SoundManager.instance.PlaySFX(_fireSounds[attackNum]);
        }
    }

    void GiveShield()
    {
        // プレイヤーにシールドを張る

        //// シールド   publicで指定
        //GameObject prefab;
        //// 生成するオブジェクト     publicで指定
        //GameObject obj;

        //obj = (GameObject)Instantiate(prefab);
    }
    public int MoodValue
    {
        get { return moodValue; }
        set { moodValue = value; }
    }
}
