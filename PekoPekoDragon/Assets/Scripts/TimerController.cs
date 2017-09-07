using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    Text timer;
    float time;

	// Use this for initialization
	void Start ()
    {
        timer = GetComponent<Text>();
        time = 180.0f;
        timer.text = "00:00";
	}
	
	// Update is called once per frame
	void Update ()
    {
        time -= Time.deltaTime;

        if(time / 60 < 10)
        {
            timer.text = "0" + ((int)(time) / 60).ToString();
        }
        else
        {
            timer.text = ((int)(time) / 60).ToString();
        }
        timer.text += ":";
        if(time % 60 < 10)
        {
            timer.text += "0" + ((int)(time) % 60).ToString();
        }
        else
        {
            timer.text += ((int)(time) % 60).ToString();
        }

        //timer.text = ((int)(time) / 60).ToString() + ":" + ((int)(time) % 60).ToString();
	}
}
