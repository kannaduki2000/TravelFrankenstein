﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsAnim : MonoBehaviour
{
    Animator anim;
    GameObject wall;

    public StairsGimmick stairs;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Stairs()
    {
        //アニメーションが再生
        //ループを切っているので一回のみ再生。
        //判定はある。
        anim.SetBool("Stairs", true);
    }

    //アニメーションイベントにて設定。
    //再生後、透明な壁が非表示になる。
    private void Wall()
    {
        wall = GameObject.Find("TransparentWall");
        wall.SetActive(false);
        // Playerを動かせるように
        stairs.AnimationEnd();
        Debug.Log("壁、非表示");
    }

    
}
