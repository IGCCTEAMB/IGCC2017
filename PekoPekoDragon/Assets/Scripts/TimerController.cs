using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    Text timer;
    public float time = 180.0f;

	// Use this for initialization
	void Start ()
    {
        timer = GetComponent<Text>();
        timer.text = "00:00";
	}
	
	// Update is called once per frame
	void Update ()
    {
        time -= Time.deltaTime;
        time = time < 0 ? time = 0 : time;
        
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
	}
}
