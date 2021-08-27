using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    //CharacterController Controller;
    //Transform Target;

    [SerializeField]
    private GameObject objGet;
    GameObject Ground;

    //[SerializeField]
    //float MoveSpeed = 2.0f;
    //int DetecDist = 8;
    //bool InArea = false;

    // ずっと、往復する
    public float speedX = 1; // スピードX
    public float speedY = 0; // スピードY
    public float speedZ = 0; // スピードZ
    public float second = 1; // かかる秒数
    private bool move = false;
    public float Stop = 2;

    public Transform targetPos; // 行きたい場所
    public Vector3 startPos;  // 元の場所

    public bool isSearch = false;
    public bool isloop = false;
    private bool move2 = false;

    private float thisXScale;

    public float time = 0;
    public float time2 = 0;
    public float time3 = 0;
    public float time4 = 0;

    Transform Player;
    Transform Bone;

    Rigidbody2D rb;
    public float speed = 1; // スピードX

    int bone = 2;
   


    private void Update()
    {
        if (isloop) { return; }
        // 右
        if (isSearch)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos.position, 1f * Time.deltaTime);
            if (targetPos.position.x - transform.position.x <= Mathf.Abs(0.1f))
            {
                // 2秒停止
                time += Time.deltaTime;
                if (time >= 2)
                {
                    time = 0;
                    Vector3 left = new Vector3(-thisXScale, transform.localScale.y, transform.localScale.z);
                    transform.localScale = left;
                    isSearch = false;
                }
            }
        }
        // 左
        else
        {            
            transform.position = Vector3.MoveTowards(transform.position, startPos, 1f * Time.deltaTime);
            if (transform.position.x - startPos.x <= Mathf.Abs(0.1f)) 
            {
                time += Time.deltaTime;
                // 2秒停止
                if (time >= 2)
                {
                    time = 0;
                    Vector3 right = new Vector3(thisXScale, transform.localScale.y, transform.localScale.z);
                    transform.localScale = right;
                    isSearch = true;
                }
               
            }
        }
        Vector3 direction = (Player.position - transform.position).normalized;

        
    }

    private void Start()
    {
        startPos = this.transform.position;
        Debug.Log(startPos);

        // プレイヤーの位置取得
        Player = GameObject.Find("Player").transform;
        Bone = GameObject.Find("Bone").transform;

        thisXScale = transform.localScale.x;

        rb = GetComponent<Rigidbody2D>();
    }

    // 石に当たったら動きを止める処理
    private void OnCollisionEnter2D(Collision2D collision)
    {
       // Stoneのタグが付いているものに当たったら
       if (collision.gameObject.tag == "Stone")
       {
            isloop = true;
            speedX = 0;
            // 右向きの状態で当たったら
           if (this.transform.localScale.x == 1)
           {
               transform.localScale = new Vector3(1, 1, 1);
           }
           // 左向きの状態で当たったら
           else
           {
               transform.localScale = new Vector3(-1, 1, 1);
           }
       }
    }

    // 骨を取得
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bone")
        {
            bone = 0;
        }
    }



    // 突進
    private void OnTriggerStay2D(Collider2D collision)
    {
        // プレイヤータグがついているやつが近づいてきたら
        if (collision.gameObject.tag == "Player")
        {
            if(bone < 1)
            {
                //float speed = 0;
                time2 += Time.deltaTime;
                if (time2 > 2)
                {
                    isloop = true;

                    if (transform.position.x < Player.position.x)
                    {
                        //右
                        rb.velocity = new Vector2(speed, 0);
                        transform.localScale = new Vector2(1, 1);
                        isloop = false;
                    }
                    else if (transform.position.x > Player.position.x)
                    {
                        //左
                        rb.velocity = new Vector2(-speed, 0);
                        transform.localScale = new Vector2(-1, 1);
                        isloop = false;
                    }
                }
            }
        }     
    }


    // 元の位置に戻る
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            time2 = 0;
            time3 += Time.deltaTime;
            if (transform.position.x < Player.position.x)
            {
                //右
                rb.velocity = new Vector2(speed, 0);
                transform.localScale = new Vector2(-1, 1);
            }
            else if (transform.position.x > Player.position.x)
            {
                //左
                rb.velocity = new Vector2(-speed, 0);
                transform.localScale = new Vector2(1, 1);
            }
        }
        if (collision.gameObject.tag == "Bone")
        {
            bone = 2;
        }
    }

}

// エネミー自身のテリトリー間を行き来する〇
//  待機時間あり（2秒間）〇
// フランケンが近づいてきたら突進する 〇
//  フランケンの視線察知はエネミーを中心にする（エネミーが動いたら視線察知も一緒に動く）〇
// 突進成功したら４秒間待機　そのあと自分のテリトリーに戻る
//  ４秒間待機後、視線察知内にフランケンがいたらフランケンに再び突進
// 突進した場所にフランケンがいなかったら２秒間待機して自分のテリトリーに戻る △

    // 骨を持っている間は襲ってこない処理

    // 石に当たったら動きを完全に止める〇

    // プレイヤータグの取得
    // Player = GameObject.FindWithTag("Player");
    //Target = Player.transform;

    //Controller = GetComponent<CharacterController>();

    /*
           if(InArea)
           {
               Debug.Log("abc");
               // プレイヤーのほうを向かせる
               this.transform.LookAt(Target.transform);

               // キューブとプレイヤー間の距離を計算
               Vector3 direction = Target.position - this.transform.position;
               direction = direction.normalized;

               // プレイヤー方向の速度を作成
               Vector3 velocity = direction * MoveSpeed;

               // プレイヤーがジャンプした時にキューブが浮かないようにy速度を0に固定しておく
               velocity.y = 0.0f;

               // キューブを動かす
               Debug.Log("nnn");
               Controller.Move(velocity * Time.deltaTime);
           }
    */

    // プレイヤーとキューブ間の距離を計算
    //Vector3 Apos = this.transform.position;
    //Vector3 Bpos = Target.transform.position;
    //float distance = Vector3.Distance(Apos, Bpos);


    /*
    // 距離がDetecDistの設定値未満の場合は検知フラグをfalseにする
    if(distance > DetecDist)
    {
        Debug.Log("aaa");
        InArea = false;
    }
    */

    /*
    // プレイヤーが検知エリアにいたら検知フラグをtrueにする
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("動いた！");
        InArea = true;
    }
    */

    // 突進の処理 没
    //   Vector3 direction = (Player.position - transform.position).normalized;
    //  speed = 3.5f;
    //transform.Translate(direction * speed * Time.deltaTime);

    // ずっと、往復する

    /*
    void FixedUpdate()
    {
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    Debug.Log("E");
        //    StartCoroutine(Move());
        //}
        //if (move == true)
        //{

        //    float s = Mathf.Sin(Time.time); // 移動量を求める
        //    this.transform.Translate(speedX * s / 50, speedY * s / 50, speedZ * s / 50);
        //    // デフォルトが右向きの場合
        //    // スケール値取り出し
        //    Vector3 scale = transform.localScale;

        //    if (s >= 0)
        //    {

        //        // 右方向に移動中
        //        scale.x = 1; // そのまま（右向き）
        //    }
        //    else
        //    {
        //        // 左方向に移動中
        //        scale.x = -1; // 反転する（左向き）
        //    }
        //    // 代入し直す
        //    transform.localScale = scale;
        //}

    }  
*/

// 没
/*
    // Playerのタグが付いているものに当たったら
else if (collision.gameObject.tag == "Player")
{
    isloop = true;
    speed = 0;
    TimeCount3();
    if (time4 > 4)
    {
        isloop = false;
    }
}
*/
