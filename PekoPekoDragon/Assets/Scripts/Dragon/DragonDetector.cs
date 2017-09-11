using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonDetector : MonoBehaviour
{

    private bool _badMood;

    public GameObject targetObject;
    public int reduceValue;
    int increaseValue;

    void OnTriggerEnter(Collider other)
    {
        if(!_badMood)
        {
            if(other.gameObject.tag == "Food")
            {
                targetObject = other.gameObject;
            }
        }
        else
        {
            if (other.gameObject.tag == "Player")
            {
                targetObject = other.gameObject;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Food")
        {
         targetObject = null;
        }
    }

	// Use this for initialization
	void Start ()
    {
        targetObject = null;
        _badMood = true;
        increaseValue = (int)(GetComponentInParent<DragonBehaviour>().MAX_MOOD_VALUE * 0.02f);
    }
	
	// Update is called once per frame
	void Update ()
    {
	}

    public bool GetMood()
    {
        return _badMood;
    }

    public void CalcMoodValue()
    {
        if(_badMood)
        {
            //時間経過で減らしていく
            regulateMoodValue(-reduceValue);

            // 餌を与えられたら怒りゲージを減らす

        }
        else
        {
            // 餌を与えられたら増やす
            regulateMoodValue(increaseValue);
        }
    }

    private void regulateMoodValue(int regulateValue)
    {
        var db = GetComponent<DragonBehaviour>();
        int moodValue = db.MoodValue;
        moodValue += regulateValue;
        db.MoodValue = moodValue;
    }
}
