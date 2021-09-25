using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryControl : MonoBehaviour
{
     private Animator anim;
    void Start()
    {
        //アニメーター取得
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        AnimationControl();
    }
    private void AnimationControl()
    {
        //sキー押したときの処理
        if(Input.GetKeyDown("s"))
        {
            //Battery Animationプレイ
             anim.Play("Battery Animation");
        }
         if(Input.GetKeyDown("s"))
        {
            anim.Play("Battery2 Animation");
        }
    }
    
    
}