﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{
    [SerializeField] private bool dogMove = false;    //ｲｯﾇの操作切り替え
    [SerializeField] private bool cantMove = false;   //ｲｯﾇの動き制御
     public bool take = false;                        //ｲｯﾇがプレイヤーを持ち運べるかどうか
    [SerializeField] private bool grab = false;       //ｲｯﾇがケーブルを持ち運べるかどうか
    [SerializeField] private bool jump = false;       //ｲｯﾇがジャンプできるかどうか
    [SerializeField] private float speed = 20f;       //ｲｯﾇのスピード

    public bool notParent = false;                    //親子関係を一つだけにできるよう固定するフラグ
    public bool notParent2 = false;                    //親子関係を一つだけにできるよう固定するフラグ
    [SerializeField] private bool tukamuFlag = false; //今、物を掴んでいるかどうかのフラグ
    [SerializeField] private bool noTossin = false;   //物を持っている時は突進できないようにするフラグ
    [SerializeField] private bool migi = false;       //右を向いているか
    [SerializeField] private bool hidari = false;     //左を向いているか
    [SerializeField] private bool kirikae = false;    //切り替え
    [SerializeField] private float muki = 0;          //どっちを向いているか、物を持っている時は向き固定
    [SerializeField] private float jumpForce = 300.0f;//ジャンプ力

    Vector3 dogScale;             　　　　　　　　　　  //ｲｯﾇの大きさ
    Vector3 dashareaScale;        　　　　　　　　　　  //ｲｯﾇのダッシュ範囲
    Vector3 playerScale;          　　　　　　　　　　  //プレイヤーの大きさ

    [SerializeField] private GameObject DashArea;     //ダッシュ範囲
    [SerializeField] private GameObject Wall;         //壊す壁
    [SerializeField] private GameObject Player;       //プレイヤー
    [SerializeField] private GameObject Cable;        //ケーブル

    [SerializeField] private Rigidbody2D rigid2D;
    [SerializeField] private Animator anim;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Floar")
        {
            jump = false;
            anim.SetBool("DogJump", false);
        }

        if (collision.gameObject.tag != "Floar")
        {
            anim.SetBool("DogJump", false);
        }

        if (collision.gameObject.name == "Wall")
        {
            //壁壊してもろて
            Wall.SetActive(false);
        }

        //プレイヤーを運べるようにするお
        if(collision.gameObject.name == "Player" && !notParent)
        {
            take = true;
        }

        //ケーブルを持てるようにするお
        if(collision.gameObject.name == "Cable" && !notParent)
        {
            grab = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player" || collision.gameObject.name == "Cable")
        {
            //離れたときの動き用
            take = false;
            grab = false;
        }
    }

    void Start()
    {
        //親子関係 + 向き
        //DashArea.transform.parent = this.transform;
        dogScale = this.transform.localScale;
        //dashareaScale = DashArea.transform.localScale;
        playerScale = Player.transform.localScale;
        rigid2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        //動かせるとき
        //操作・アニメーション
        if (dogMove)
        {
            if (Input.GetKey(KeyCode.LeftArrow) && !cantMove && !migi)
            {
                this.transform.Translate(-0.01f, 0.0f, 0.0f);
                anim.SetBool("DogWalk", true);

                if (tukamuFlag && 0 < muki)
                {
                    transform.localScale = new Vector3(-muki, dogScale.y, dogScale.z);
                }

                else
                {
                    transform.localScale = new Vector3(-dogScale.x, dogScale.y, dogScale.z);
                }
            }

            else if (Input.GetKey(KeyCode.RightArrow) && !cantMove && !hidari)
            {
                this.transform.Translate(0.01f, 0.0f, 0.0f);

                if(jump == false)
                {
                    anim.SetBool("DogWalk", true);
                }

                if (tukamuFlag && muki < 0)
                {
                    transform.localScale = new Vector3(-muki, dogScale.y, dogScale.z);
                }

                else
                {
                    transform.localScale = new Vector3(dogScale.x, dogScale.y, dogScale.z);
                }

            }

            else
            {
                anim.SetBool("DogWalk", false);
            }

            //ジャンプ
            if (jump == false && Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetBool("DogJump",true);
                this.rigid2D.AddForce(transform.up * this.jumpForce);
                jump = true;
            }

            //プレイヤーを持つ部分
            if(Input.GetKey(KeyCode.R) && !cantMove && (take || grab))
            {
                noTossin = true;
                tukamuFlag = true;
                muki = -transform.localScale.x;
                anim.SetBool("DogCatch", true);

                //プレイヤーなら
                if(take)
                {
                    notParent = true;
                    Player.transform.parent = this.transform;
                    this.transform.SetParent(transform, false);
                }

                //ケーブルなら
                if (grab)
                {
                    notParent2 = true;
                    Cable.transform.parent = this.transform;
                    this.transform.SetParent(transform, false);
                }
            }

            //持ったものを離す部分
            if (Input.GetKeyUp(KeyCode.R) && !cantMove)
            {
                notParent = false;
                notParent2 = false;
                noTossin = false;
                Player.transform.parent = null;
                Cable.transform.parent = null;
                tukamuFlag = false;
                anim.SetBool("DogCatch", false);
            }

            //突進
            if (Input.GetKey(KeyCode.T) && !cantMove && !noTossin)
            {
                //左向き
                if (transform.localScale.x == -dogScale.x)
                {
                    //移動速度を増やす
                    this.transform.Translate(-0.02f, 0.0f, 0.0f);
                    hidari = true;
                    anim.SetBool("DogWalk", true);
                }

                if (transform.localScale.x == dogScale.x)
                {
                    this.transform.Translate(0.02f, 0.0f, 0.0f);
                    migi = true;
                    anim.SetBool("DogWalk", true);
                }
                //Invoke("DashStart", 0.1f);
                kirikae = true;
            }

            if(Input.GetKeyUp(KeyCode.T) && kirikae == true)
            {
                migi = false;
                hidari = false;
                kirikae = false;
                anim.SetBool("DogWalk", false);
            }
        }
    }

    /*public void DashStart()
    {
        DashArea.transform.parent = null;
        //cantMove = true;

        Transform dash = this.transform;
        Vector2 dogposition = dash.position;

        //ダッシュ範囲中心目がけて進みますお
        dogposition.x = Mathf.MoveTowards(dogposition.x, DashArea.transform.position.x, Time.deltaTime * speed);
        dash.position = dogposition;

        Invoke("DashStop", 1.0f);
    }*/

    /*public void DashStop()
    {
        cantMove = false;
        DashArea.transform.parent = this.transform;

        //子の向きと位置を調整
        if(transform.localScale.x == -dogScale.x)
        {
            DashArea.transform.position = new Vector3(this.transform.position.x - 3f,
            this.transform.position.y, this.transform.position.z);
            DashArea.transform.localScale = new Vector3(-dashareaScale.x, dashareaScale.y, dashareaScale.z);

        }

        else if (transform.localScale.x == dogScale.x)
        {
            DashArea.transform.position = new Vector3(this.transform.position.x + 3,
            this.transform.position.y, this.transform.position.z);
            DashArea.transform.localScale = new Vector3(dashareaScale.x, dashareaScale.y, dashareaScale.z);
        }
    }*/
}