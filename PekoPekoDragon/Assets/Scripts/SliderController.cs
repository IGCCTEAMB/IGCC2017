using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider mood;
    int frameCount;

    // Use this for initialization
    void Start ()
    {
        mood.value = 2;
        frameCount = 0;
    }

    // Update is called once per frame
    void Update ()
    {
        // プレイヤー１のゲージを増やす
		if(Input.GetKeyDown(KeyCode.Z))
        {
            ModifySlider(mood, 2, true);
        }
        // プレイヤー２のゲージを増やす
        if (Input.GetKeyDown(KeyCode.X))
        {
            ModifySlider(mood, 2, true);
        }
        // プレイヤー３のゲージを増やす
        if (Input.GetKeyDown(KeyCode.C))
        {
            ModifySlider(mood, 2, true);
        }
        // プレイヤー４のゲージを増やす
        if (Input.GetKeyDown(KeyCode.V))
        {
            ModifySlider(mood, 2, true);
        }

        // 自然と減らしていく
        frameCount++;
        if(frameCount > 20)
        {
            ModifySlider(mood, 0.008f, false);
        }
    }

    private void ModifySlider(Slider slider, float value, bool isAdd)
    {
        // ゲージを増やす
        if(isAdd)
        {
            slider.value += value;
        }
        // ゲージを減らす
        else
        {
            slider.value = slider.value < value ? 0 : slider.value - value;
        }
    }
}
