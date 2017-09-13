using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAnimationClip : MonoBehaviour
{
    [SerializeField]
    AnimationClip _clip;

    string overrideClipName = "Walk loop";   // 上書きするAnimationClip対象

    private AnimatorOverrideController overrideController;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        overrideController = new AnimatorOverrideController();
        overrideController.runtimeAnimatorController = anim.runtimeAnimatorController;
        anim.runtimeAnimatorController = overrideController;
    }

    public void ChangeClip(AnimationClip clip)
    {
        // ステートをキャッシュ
        AnimatorStateInfo[] layerInfo = new AnimatorStateInfo[anim.layerCount];
        for (int i = 0; i < anim.layerCount; i++)
        {
            layerInfo[i] = anim.GetCurrentAnimatorStateInfo(i);
        }

        // AnimationClipを差し替えて、強制的にアップデート
        // ステートがリセットされる
        overrideController[overrideClipName] = clip;
        anim.Update(0.0f);

        // ステートを戻す
        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.Play(layerInfo[i].nameHash, i, layerInfo[i].normalizedTime);
        }
    }
}