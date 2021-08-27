using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    private int presskeyFrames = 0;             //長押しフレーム数
    private int PressLong = 300;                //長押し
    private int PressShort = 100;               //軽押し

    private bool Throw = false;                 //投げのフラグ
    private bool Carry = false;                 //アニメーションフラグ

    public Vector2 speed = new Vector2(1, 1);   //速度

    KeyPlessThrow item;
    Animator anim;

    void Start()
    {
        // Animetorコンポネーションを取得する
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Throw)
        {
            if (Input.GetKey(KeyCode.R))//半田：SpaceからRに変更
            {
                //Rの判定
                //memo  『? true:false』
                presskeyFrames += (Input.GetKey(KeyCode.R)) ? 1 : 0;//半田：SpaceからRに変更
                Debug.Log(presskeyFrames);
            }

            else if (Input.GetKeyUp(KeyCode.R))//半田：SpaceからRに変更
            {
                //もしRが長押しされたら
                if (PressLong <= presskeyFrames)

                //高めに投げる
                {
                    item.Hight();
                    Debug.Log("長め");
                }

                //もしRが押されたら
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

        // アニメーションつき移動
        if(Carry)
        {
            //移動
            Vector2 Position = transform.position;
            if (Input.GetKey(KeyCode.A))
            {
                Position.x -= speed.x;
                anim.SetBool("CarryMove", true);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                Position.x += speed.x;
                anim.SetBool("CarryMove", true);
            }
            else
            {
                anim.SetBool("CarryMove", false);
            }
            transform.position = Position;

            //向き反転
            float x = Input.GetAxisRaw("Horizontal");

            if (x != 0)
            {
                Vector2 Iscale = gameObject.transform.localScale;
                if ((Iscale.x > 0 && x < 0) || (Iscale.x < 0 && x > 0))
                {
                    Iscale.x *= -1;
                    gameObject.transform.localScale = Iscale;
                }
            }
        }
    }



    //アイテムから離れたら
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            anim.SetBool("Carry", false);

            Carry = false;
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

            //Wを押していたら
            if (Input.GetKey(KeyCode.W))
            {
                anim.SetBool("Carry", true);

                Debug.Log("on");

                Throw = true;
                Carry = true;

                //アイテムクラスの取得
                item = collision.gameObject.GetComponent<KeyPlessThrow>();

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
/*
*スクリプト内容


・Throwフラグで物を持つ、投げる処理
　・Rを押すことで長押し、短押し判定。
　・物、敵との判定はTriggerで判定。

・Carryフラグで物を持った時のアニメーション
　・アニメーション:boolでwaitとwalkの判定。

＊できていないところ
・持ち上げた時の物、敵のy軸の変化
・アニメーションが終わるまで持ち上げる判定がない
 
 */


