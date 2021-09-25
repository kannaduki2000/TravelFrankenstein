using DualShockInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DebugLogUtility;

public class GoofCon : ElectricItem
{
    [SerializeField] private PlayerController playerCon;

    /*グーフ操作用-----------------------------------------------------------------------*/
    [SerializeField] private bool dogMove = false;  //ｲｯﾇの操作切り替え
    [SerializeField] private bool cantMove = false; //ｲｯﾇの動き制御
    [SerializeField] private bool take = false;     //ｲｯﾇがプレイヤーを持ち運べるかどうか
    [SerializeField] private bool grab = false;     //ｲｯﾇがケーブルを持ち運べるかどうか
    [SerializeField] private bool Jump = false;
    public float jumpForce = 300.0f;

    [SerializeField] private bool tukamuFlag = false;
    [SerializeField] private bool noTossin = false;
    [SerializeField] private bool migi = false;
    [SerializeField] private bool hidari = false;
    [SerializeField] private float muki = 0;

    Vector3 dogScale;             　　　　　　　　　　//ｲｯﾇの大きさ
    Vector3 dashareaScale;        　　　　　　　　　　//ｲｯﾇのダッシュ範囲
    Vector3 playerScale;          　　　　　　　　　　//プレイヤーの大きさ

    //[SerializeField] private GameObject DashArea;   //ダッシュ範囲
    [SerializeField] private GameObject Wall;       //壊す壁
    [SerializeField] private GameObject Player;     //プレイヤー
    [SerializeField] private GameObject Cable;      //ケーブル

    [SerializeField] private Rigidbody2D rigid2D;
    [SerializeField] private Animator anim;
    /*-----------------------------------------------------------------------------------*/

    /*グーフ敵対用-----------------------------------------------------------------------*/
    public Vector2 startPos;
    Transform target;
    Transform returnPos;
    Transform endPos;
    [SerializeField]private GameObject returm;
    [SerializeField]private GameObject stop;

    [SerializeField] private float speed = 3f;     //ｲｯﾇのスピード
    public bool searchPlayer = false;   //プレイヤーの索敵
    public bool Hostile = true;         //敵対flag
    public float chaseSpeed = 1;        //追いかけるスピード
    public float time2 = 0;    // 突進させる前に2秒停止させるためのタイマー



    /*-----------------------------------------------------------------------------------*/

    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.position;
        target = Player.transform; // Playerの位置取得
        returnPos = returm.transform;
        endPos = stop.transform;


        //親子関係 + 向き
        //DashArea.transform.parent = this.transform;
        dogScale = this.transform.localScale;
        //dashareaScale = DashArea.transform.localScale;
        playerScale = Player.transform.localScale;
        rigid2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Hostile == true)
        {
            //if (searchPlayer == true)
            //{
            //    DLUtility.DebugLog("突っ込むよ");
            //    time2 += Time.deltaTime;
            //    if (time2 > 2)
            //    {
            //        Debug.Log("add_player");
            //        anim.SetBool("DogWalk", false);
            //        if (transform.position.x < target.position.x)
            //        {
            //            //右
            //            rigid2D.velocity = new Vector2(speed, 0);
            //            transform.localScale = new Vector2(-muki, dogScale.y);
                        
            //        }
            //        else if (transform.position.x > target.position.x)
            //        {
            //            //左
            //            rigid2D.velocity = new Vector2(-speed, 0);
            //            transform.localScale = new Vector2(-dogScale.x, dogScale.y);
            //        }
            //    }
            //}
            //else if(searchPlayer == true && transform.position.x == endPos.position.x)
            //{
                
            //}
            //else
            //{
               
            //    anim.SetBool("DogWalk", false);
            //}
        }
        else if(playerCon.haveBone == true)
        {
            Hostile = false;
        }

        /*グーフ操作-------------------------------------------------------------------*/
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
                anim.SetBool("DogWalk", true);

                if (tukamuFlag && muki < 0)
                {
                    transform.localScale = new Vector3(muki, dogScale.y, dogScale.z);
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

            if(Jump == true && DSInput.PushDown(DSButton.Cross))
            {
                this.rigid2D.AddForce(transform.up * this.jumpForce);
                Jump = !Jump;
            }

            //プレイヤーを持つ部分
            if (Input.GetKey(KeyCode.R) && !cantMove && (take || grab))
            {
                noTossin = true;
                tukamuFlag = true;
                muki = -transform.localScale.x;

                //プレイヤーなら
                if (take)
                {
                    Player.transform.parent = this.transform;
                    this.transform.SetParent(transform, false);
                }

                //ケーブルなら
                if (grab)
                {
                    Cable.transform.parent = this.transform;
                    this.transform.SetParent(transform, false);
                }
            }

            //持ったものを離す部分
            if (Input.GetKeyUp(KeyCode.R) && !cantMove)
            {
                noTossin = false;
                Player.transform.parent = null;
                Cable.transform.parent = null;
                tukamuFlag = false;
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
            }

            if (Input.GetKeyUp(KeyCode.T))
            {
                migi = false;
                hidari = false;
            }
        }
        /*---------------------------------------------------------------------------*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Wall")
        {
            //壁壊してもろて
            Wall.SetActive(false);
        }

        //プレイヤーを運べるようにするお
        if (collision.gameObject.name == "Player")
        {
            take = true;
        }

        //ケーブルを持てるようにするお
        if (collision.gameObject.name == "Cable")
        {
            grab = true;
        }

        if(collision.gameObject.tag == "Ground")
        {
            Jump = true;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            searchPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            searchPlayer = false;
        }
    }
}
