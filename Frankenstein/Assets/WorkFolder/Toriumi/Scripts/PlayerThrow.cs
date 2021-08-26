﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    private int presskeyFrames = 0;             //長押しフレーム数
    private int PressLong = 300;                //長押し
    private int PressShort = 100;               //軽押し
    private bool Throw = false;                 //投げのフラグ
    Rigidbody2D rb;
    KeyPlessThrow item;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Throw)
        {
            if (Input.GetKey(KeyCode.R))//半田：SpaceからRに変更
            {
                //スペースの判定
                //memo  『? true:false』
                presskeyFrames += (Input.GetKey(KeyCode.R)) ? 1 : 0;//半田：SpaceからRに変更
                Debug.Log(presskeyFrames);
            }

            else if (Input.GetKeyUp(KeyCode.R))//半田：SpaceからRに変更
            {
                //もしスペースが長押しされたら
                if (PressLong <= presskeyFrames)

                //高めに投げる
                {
                    item.Hight();
                    Debug.Log("長め");
                }

                //もしスペースが押されたら
                else if (presskeyFrames <= PressShort)

                //低めに投げる
                {
                    item.Low();
                    Debug.Log("短め");
                }
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                this.gameObject.transform.DetachChildren();
            }
        }
    }
    private void FixedUpdate()
    {

    }

    //アイテムに当たったら
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    //アイテムから離れたら
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            Throw = false;
            presskeyFrames = 0;
            this.gameObject.transform.DetachChildren();
            Debug.Log("exit");
        }
    }


    //アイテムに当たり続けたら
    private void OnTriggerStay2D(Collider2D collision)
    {
  

        if (collision.gameObject.tag == "Item")
        {
            Debug.Log("stay");

            //item = collision.gameObject.GetComponent<Item>();
            //Wを押していたら
            if (Input.GetKey(KeyCode.W))
            {
                Throw = true;
                //アイテムクラスの取得
                item = collision.gameObject.GetComponent<KeyPlessThrow>();

                //アイテムのY軸が上がる
                // ここでこのオブジェクトをプレイヤーの子供にする
                item.gameObject.transform.parent = this.transform;
            }
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            this.gameObject.transform.DetachChildren();
        }
    }
}


