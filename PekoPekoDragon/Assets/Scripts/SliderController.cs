using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider mood;
    public Slider love1;
    public Slider love2;

	// Use this for initialization
	void Start ()
    {
        mood = GetComponent<Slider>();
        love1 = GetComponent<Slider>();
        love2 = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Z))
        {
            mood.value += 2;
            love1.value += 2;
            love2.value = love2.value < 2 ? 0 : love2.value - 2;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            mood.value += 2;
            love1.value = love1.value < 2 ? 0 : love1.value - 2;
            love2.value += 2;
        }
    }
}
