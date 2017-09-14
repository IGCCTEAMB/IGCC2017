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

    private int lastNum;




    // ご機嫌度
    public float moodValue;
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

    private Animator anim;

    // Use this for initialization
    void Start()
    {
        navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
        _time = 0;
        _stopTime = 0;
        _moveDelayTime = 0;
        num = 0;
        _moodState = MoodState.NORMAL;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        float dist = Vector3.Distance(gameObject.transform.position, waypoints[num].transform.position);
        _targetObject = gameObject.transform.GetChild(0).GetComponent<DragonDetector>().targetObject;

        if (dist > minDist)
        {
            if (lastNum != num)
            {
                Move();
            }
        }
        else
        {
            if (gameObject.transform.position.x == waypoints[num].transform.position.x &&
                gameObject.transform.position.z == waypoints[num].transform.position.z)
            {
                _moveDelayTime++;

                anim.SetBool("Idle Bool", true);
                anim.SetBool("Walk Bool", false);
            }

            if (_moveDelayTime > 3 * 60.0f)
            {
                _moveDelayTime = 0;
                num = Random.Range(0, waypoints.Length);


            }
        }

        int iconNum;

        moodValue += 0.1f;

        if(_moodState == MoodState.BAD)
        {
            _moodState = MoodState.BAD;
            // ご機嫌度を徐々に減らす
            gameObject.transform.GetChild(0).GetComponent<DragonDetector>().CalcMoodValue();
            iconNum = 2;
        }
        else if(moodValue < 50)
        {
            _moodState = MoodState.NORMAL;
            iconNum = 0;
        }
        else if(moodValue < 80)
        {
            _moodState = MoodState.GREAT;
            iconNum = 0;

            GameManager.Instance.FollowPlayer();
        }
        else
        {
            _moodState = MoodState.HAPPY;
            GiveShield();
            iconNum = 1;

            GameManager.Instance.FollowPlayer();
        }

        GameManager.Instance.ChangeIcon(iconNum);
    }



    void Move()
    {
        if (!_targetObject)
        {
            if (!navMeshAgent.isStopped)
            {
                anim.SetBool("Walk Bool", true);
                anim.SetBool("Idle Bool", false);

                Debug.Log(waypoints[num].transform.position);

                navMeshAgent.SetDestination(waypoints[num].transform.position);
                lastNum = num;
            }
                if (_moodState == MoodState.BAD)
                {
                    Attack();
                }
        }
        else
        {
            navMeshAgent.SetDestination(_targetObject.transform.position);
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
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("Attack basic(rage)") ||
                anim.GetCurrentAnimatorStateInfo(0).IsName("Attack AOE(rage)"))
            {
                _stopTime++;
            }
        }

        if (_time > attackDelay * 60.0f)
        {
            if (attackNum == 0)
            {
                // アニメーションを止める
                anim.SetBool("Walk Bool", false);
                anim.SetBool("Idle Bool", false);

                // 炎を吐くアニメーション再生
                anim.SetInteger("Attack Int", 1);
            }
            else
            {
                // アニメーションを止める
                anim.SetBool("Walk Bool", false);
                anim.SetBool("Idle Bool", false);

                // 炎を吐くアニメーション再生
                anim.SetInteger("Attack Int", 2);
            }

            _time = 0;
            navMeshAgent.isStopped = true;
            GameObject par = Instantiate(dragonAttacks[attackNum], gameObject.transform.GetChild(attackNum + 1).transform.position, gameObject.transform.GetChild(attackNum + 1).transform.rotation);
            SoundManager.instance.PlaySFX(_fireSounds[attackNum]);
        }

        if (_stopTime > 0.5 * 60.0f)
        {
            // 攻撃するアニメーションが再生されていなければ
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack basic(rage)") &&
                !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack AOE(rage)"))
            {

                navMeshAgent.isStopped = false;
                _stopTime = 0;

                anim.SetInteger("Attack Int", 0);
            }
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
