using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoodController : MonoBehaviour
{
    bool isGood = true;
    int frameCount = 0;
    
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		// 通常状態
        if(isGood)
        {

        }
        // 怒っている状態
        else
        {
            // 自然と減らしていく
            frameCount++;
            if (frameCount > 20)
            {

            }
        }
    }
}
