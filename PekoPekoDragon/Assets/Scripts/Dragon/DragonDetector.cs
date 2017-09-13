using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonDetector : MonoBehaviour
{

    public GameObject targetObject;
    public int reduceValue;
    int increaseValue;

    DragonBehaviour db;


    void OnTriggerEnter(Collider other)
    {
        if(!(db.DragonMoodState == DragonBehaviour.MoodState.BAD))
        {
            if(other.gameObject.tag == "Food")
            {
                targetObject = other.gameObject;
            }
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Food")
        {
         targetObject = null;
        }
    }

	// Use this for initialization
	void Start ()
    {
        targetObject = null;
        increaseValue = (int)(GetComponentInParent<DragonBehaviour>().MAX_MOOD_VALUE * 0.02f);

        db = GetComponentInParent<DragonBehaviour>();
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void CalcMoodValue()
    {
        if (db.DragonMoodState == DragonBehaviour.MoodState.BAD)
        {
            //時間経過で減らしていく
            regulateMoodValue(-reduceValue);
        }
        else
        {
            // 餌を与えられたら増やす
            regulateMoodValue(increaseValue);
        }
    }

    public void regulateMoodValue(int regulateValue)
    {
        var db = GetComponentInParent<DragonBehaviour>();
        float moodValue = db.MoodValue;
        moodValue += regulateValue;
        db.MoodValue = moodValue;
    }


}
