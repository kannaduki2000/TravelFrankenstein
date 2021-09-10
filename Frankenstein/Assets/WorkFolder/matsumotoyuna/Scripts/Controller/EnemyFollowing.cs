﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Edit→Project Settings→Physics2D下のチェック
//その前にレイヤー分けすると、
//プレイヤーとエネミーですれ違い通信ができるようになるよ！

//とてもとても非効率な書き方してる

public class EnemyFollowing : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public GameObject MineCart;
    public float speed = 3.0f;         //速度
    public float stopDistance;         //止まるときの距離

    private bool isFollowing = true;   //追従するかどうか

    public MoveTest mt;
    public CarPush cp;
    public MinecartPush mcp;
    public PushButton pushb;

    public bool enemyMove = true;                       //エネミーの動き
    private bool Jump = false;                          //ジャンプ用
    private bool Follow = false;                        //二度目の入力でのついてくるか否か
    private bool car = false;                           //車に触れているかどうか
    private bool minecart = false;                      //トロッコに触れているかどうか
    [SerializeField] private bool tukamuFlag = false;   //物を掴んでいるかどうか
    private bool okrpush = false;

    Vector3 enemyScale;

    Rigidbody2D rigid2D;
    float jumpForce = 300.0f;                           //ジャンプ力
    [SerializeField] private float muki = 0;            //向いている方向判定

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Jump = false;
        //↑床に着くまでジャンプさせないマン

        //車に触れているかな～？
        if(collision.gameObject.tag == "Car")
        {
            car = true;
            cp.rigid2D.constraints = RigidbodyConstraints2D.None;
        }

        //坂を降りるやーつだお
        if (collision.gameObject.name == "MineCart")
        {
            minecart = true;
        }

        //トロッコに触れている？既にトロッコはボタンを押した？
        if (collision.gameObject.name == "MineCart" && pushb.rpush == true)
        {
            okrpush = true;
        }
    }

    void Start()
    {
        //Rigid～、エネミーの大きさ取得
        this.rigid2D = GetComponent<Rigidbody2D>();
        enemyScale = this.transform.localScale;
    }

    void Update()
    {
        //箱を用意して、その中にY座標を入れる
        Vector2 targetPos = player.transform.position;
        targetPos.y = transform.position.y;

        //距離
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (isFollowing)
        {
            //if(間の距離が止まるときの距離以上なら?)
            if (distance > stopDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                new Vector2(player.transform.position.x, enemy.transform.position.y),
                speed * Time.deltaTime);
            }

            // 右
            if (player.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-enemyScale.x, enemyScale.y, enemyScale.z);
            }

            // 左
            else if (player.transform.position.x > transform.position.x)
            {
                transform.localScale = enemyScale;
            }

            //ジャンプ
            if (Jump == false && Input.GetKeyDown(KeyCode.Space))
            {
                this.rigid2D.AddForce(transform.up * this.jumpForce);
                Jump = !Jump;
            }
        }

        //エネミーの動き用
        if (enemyMove == false)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.transform.Translate(-0.01f, 0.0f, 0.0f);

                if (tukamuFlag && 0 > muki)
                {
                    transform.localScale = new Vector3(-muki, enemyScale.y, enemyScale.z);
                }

                else
                {
                    transform.localScale = new Vector3(-enemyScale.x, enemyScale.y, enemyScale.z);
                }
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                this.transform.Translate(0.01f, 0.0f, 0.0f);
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                
                if (tukamuFlag && muki > 0)
                {
                    transform.localScale = new Vector3(-muki, enemyScale.y, enemyScale.z);
                }

                else
                {
                    transform.localScale = new Vector3(enemyScale.x, enemyScale.y, enemyScale.z);
                }
            }

            if (Jump == false && Input.GetKeyDown(KeyCode.Space))
            {
                this.rigid2D.AddForce(transform.up * this.jumpForce);
                Jump = !Jump;
            }

            //車押すよ
            if(Input.GetKey(KeyCode.R) && car == true)
            {
                cp.crash = true;
            }

            //トロッコを坂から落とすよ
            if (Input.GetKey(KeyCode.R) && minecart == true)
            {
                mcp.minecartpush = true;
            }

            //トロッコ引っ張るよ
            if(Input.GetKeyDown(KeyCode.R) && pushb.rpush == true && okrpush == true)
            {
                tukamuFlag = true;
                muki = -transform.localScale.x;
                mcp.enemyrpush = true;
                MineCart.transform.parent = this.transform;
                this.transform.SetParent(transform, false);
                MineCart.gameObject.layer = 9;
            }

            //トロッコ手放すよ
            if (Input.GetKeyUp(KeyCode.R) && pushb.rpush == true && okrpush == true)
            {
                tukamuFlag = false;
                mcp.enemyrpush = false;
                MineCart.transform.parent = null;
                MineCart.gameObject.layer = 8;
            }
        }

        // 追従の切り替え処理
        //if (Input.GetKey(KeyCode.A))
        //{
        //     //GetComponent<EnemyFollowing>().enabled = false;
        //     Following();
        //}

        //★操作の切り替え処理
        //1回目の切り替え時の動き
        if (Input.GetKeyDown(KeyCode.Return) && Follow == false)
        {
            //GetComponent<EnemyFollowing>().enabled = false;
            mt.playerMove = !mt.playerMove;
            Following();
            enemyMove = !enemyMove;
            //PlayerChange(); //私にこれを扱うことなんてできませんでした。
            Follow = !Follow;
        }

        //2回目の切り替え時、プレイヤーだけ動いてエネミー不動堂
        //この状態だと何回Enter押してもプレイヤーしか動かんで
        else if(Input.GetKeyDown(KeyCode.Return) && Follow == true)
        {
            //Following();  //こんなの知りません。
            isFollowing = false;
            enemyMove = true;
            mt.playerMove = false;
        }

        //呼ぶボタン(Delete仮置き)を押した時の動き
        //Followを切り替えることでもう一度追従や切り替えができるお
        if (Follow == true && Input.GetKeyDown(KeyCode.Delete))
        {
            //Following();  //あぁ、わからない。
            isFollowing = true;
            Follow = !Follow;
        }
    }

    // かつて追従の切り替えだったもの
    public void Following()
    {
        isFollowing = !isFollowing;
    }

    // かつて操作の切り替えだったもの
    public void PlayerChange()
    {
        // プレイヤーの操作をできなくする
        mt.playerMove = !mt.playerMove;

        // 操作権を敵に移動させる
        Following();
        enemyMove = !enemyMove;

        //この状態で元のボタンを押すと、操作切り替え・追従なし

        // カメラの追従を敵に移す→誰か頑張ってクレメンス他力本願寺
    }
}

/*解決
敵の追従
プレイヤーの座標と重なってはいけない！
プレイヤーとエネミーは、いくらかの間を開けるイメージ
進行方向と反対側につく？(プレイヤーが右に進んでいる時は左側につく)
*/

/*
Lボタンで操作切り替え
そのあとは付いてこない→機能オフ?
*/

/*
Lボタンを押した後にAボタンを押すと呼べる→機能オン
(これはいらなさそう？)→(いるらしいですよあなた)
*/