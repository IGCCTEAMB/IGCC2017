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

    private float _time;
    private float _stopTime;
    public float atkMoveDelay = 1;
    public float attackDelay = 2;
    private bool _badMood;



    // ご機嫌度
    int moodValue;
    // 最大ご機嫌度
    public int MAX_MOOD_VALUE = 100;

    // Use this for initialization
    void Start()
    {
        navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
        _badMood = gameObject.transform.GetChild(0).GetComponent<DragonDetector>().GetMood();
        _time = 0;
        _stopTime = 0;

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

            if (_badMood)
            {
                Attack();
            }
        }
        else
        {
            navMeshAgent.SetDestination(_targetObject.transform.position);
                // ドラゴンがプレイヤーについて行っている時の処理

                // プレイヤーにシールドを張る
                //GiveShield();
            
            //float dist = Vector3.Distance(gameObject.transform.position, _targetObject.transform.position);
            //
            //if (dist < _minDistWithPlayer)
            //{
            //
            //    navMeshAgent.isStopped = true;
            //    Attack();
            //    gameObject.transform.LookAt(_targetObject.transform.position);
            //
            //}
            //else
            //{
            //    navMeshAgent.isStopped = false;
            //
            //    navMeshAgent.SetDestination(_targetObject.transform.position);
            //
            //    // ドラゴンがプレイヤーについて行っている時の処理
            //
            //    // プレイヤーにシールドを張る
            //    //GiveShield();
            //}

        }

    }

    void Attack()
    {
        int attackNum = Random.Range(0, dragonAttacks.Length);

        if (!navMeshAgent.isStopped)
        {
            _time++;
        }
        else
        {
            _stopTime++;
        }
        
        if (_time > attackDelay * 60.0f)
        {
            _time = 0;           
            navMeshAgent.isStopped = true;
            GameObject par = Instantiate(dragonAttacks[attackNum], gameObject.transform.GetChild(attackNum + 1).transform.position, gameObject.transform.GetChild(attackNum + 1).transform.rotation);
            SoundManager.instance.PlaySFX(_fireSounds[attackNum]);      
        }

        if (_stopTime > 0.5 * 60.0f)
        {
            navMeshAgent.isStopped = false;
            _stopTime = 0;
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
