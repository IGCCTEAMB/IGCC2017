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
    private float _moveDelayTime;
    public float atkMoveDelay = 1;
    public float attackDelay = 2;




    // ご機嫌度
    float moodValue;
    // 最大ご機嫌度
    public float MAX_MOOD_VALUE = 100;

    public enum MoodState
    {
        BAD,
        NORMAL,
        GREAT,
        HAPPY
    }

    private MoodState _moodState;

    // Use this for initialization
    void Start()
    {
        navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
        _time = 0;
        _stopTime = 0;
        _moveDelayTime = 0;
        _moodState = MoodState.NORMAL;
    }

    // Update is called once per frame
    void Update()
    {
        int num;
        
        if(_moodState == MoodState.BAD)
        {
            _moodState = MoodState.BAD;
            // ご機嫌度を徐々に減らす
            gameObject.transform.GetChild(0).GetComponent<DragonDetector>().CalcMoodValue();
            num = 2;
        }
        else if(moodValue < 30)
        {
            _moodState = MoodState.NORMAL;
            num = 0;
        }
        else if(moodValue < 60)
        {
            _moodState = MoodState.GREAT;
            num = 1;
        }
        else
        {
            _moodState = MoodState.HAPPY;
            GiveShield();
            num = 1;
        }

        GameManager.Instance.ChangeIcon(num);

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
            _moveDelayTime++;
            if (_moveDelayTime > 5 * 60.0f)
            {
                _moveDelayTime = 0;

                navMeshAgent.SetDestination(waypoints[num].transform.position);
            }

            if (_moodState == MoodState.BAD)
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

    public void GiveShield()
    {
        // プレイヤーにシールドを張る

        //// シールド   publicで指定
        //GameObject prefab;
        //// 生成するオブジェクト     publicで指定
        //GameObject obj;

        //obj = (GameObject)Instantiate(prefab);
    }
    public float MoodValue
    {
        get { return moodValue; }
        set { moodValue = value; }
    }

    public MoodState DragonMoodState
    {
        get { return _moodState; }
        set { _moodState = value; }
    }

}
