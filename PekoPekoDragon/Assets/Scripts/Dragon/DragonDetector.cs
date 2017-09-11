using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonDetector : MonoBehaviour
{

    private bool _badMood;

    public GameObject targetObject;

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

    }
	
	// Update is called once per frame
	void Update ()
    {
		if(!_badMood)
        {

        }
	}
}
