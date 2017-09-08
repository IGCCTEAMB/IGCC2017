using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider mood;
    public Slider love1;
    public Slider love2;
    public Slider love3;
    public Slider love4;
    int frameCount;

    // Use this for initialization
    void Start ()
    {
        mood.value = 2;
        love1.value = 0;
        love2.value = 0;
        love3.value = 0;
        love4.value = 0;
        frameCount = 0;
    }

    // Update is called once per frame
    void Update ()
    {
        // プレイヤー１のゲージを増やす
		if(Input.GetKeyDown(KeyCode.Z))
        {
            ModifySlider(mood, 2, true);
            ModifySlider(love1, 10, true);
            ModifySlider(love2, 2, false);
            ModifySlider(love3, 2, false);
            ModifySlider(love4, 2, false);
        }
        // プレイヤー２のゲージを増やす
        if (Input.GetKeyDown(KeyCode.X))
        {
            ModifySlider(mood, 2, true);
            ModifySlider(love1, 2, false);
            ModifySlider(love2, 10, true);
            ModifySlider(love3, 2, false);
            ModifySlider(love4, 2, false);
        }
        // プレイヤー３のゲージを増やす
        if (Input.GetKeyDown(KeyCode.C))
        {
            ModifySlider(mood, 2, true);
            ModifySlider(love1, 2, false);
            ModifySlider(love2, 2, false);
            ModifySlider(love3, 10, true);
            ModifySlider(love4, 2, false);
        }
        // プレイヤー４のゲージを増やす
        if (Input.GetKeyDown(KeyCode.V))
        {
            ModifySlider(mood, 2, true);
            ModifySlider(love1, 2, false);
            ModifySlider(love2, 2, false);
            ModifySlider(love3, 2, false);
            ModifySlider(love4, 10, true);
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
