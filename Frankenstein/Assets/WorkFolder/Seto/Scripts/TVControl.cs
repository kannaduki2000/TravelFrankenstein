using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVControl : MonoBehaviour
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
            //TV Animationプレイ
             anim.Play("TV Animation");
        }
         if(Input.GetKeyDown("s"))
        {
            anim.Play("TV2 Animation");
        }
    }
}
