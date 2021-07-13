using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb2d;
    Animator anim;
    public GameObject Player;
    public GameObject enemy;

    public float speed;//速度
    public float jumpPower;//ジャンプ
    private float vx = 0;
    private bool leftFlag = false;
    private bool jumpFlag = false;
    private bool groundCheck = false;//接地判定 
    private bool pushFlag = false;
    public bool player_Move = false;

    //
    [SerializeField] private LayerMask enemyLayer; // モック版熊倉:敵のLayer取得用

    [SerializeField] int maxHP = 100;
    [SerializeField] float HP = 100;
    [SerializeField] private bool touchFlag = false;
    [SerializeField] private bool enemyTouchFlag = false; // モック版熊倉:フラグ追加
    private bool onElectricity = true;
    public GameObject hpCanvas;
    private float hpCanvasScale_x;


    private bool enemyFollowFlg = false;
    // モック版熊倉:GetCompornent重いんで直で取得、ここ敵の数増えるはずなので書き換えること
    [SerializeField] private EnemyController enemyCon;
    [SerializeField] private Image hp;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        hpCanvasScale_x = hpCanvas.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        anim = gameObject.GetComponent<Animator>();

        // モック版熊倉:LayerでやってたっぽいのでLinecastで取得
        if (GetEnemyLayer())
        {
            if (enemyCon.isCharging)
            {
                enemyTouchFlag = true;
            }
            hpCanvas.SetActive(true);
        }
        else
        {
            enemyTouchFlag = false;
            hpCanvas.SetActive(false);
        }


        /*プレイヤーの移動入力処理--------------------------------------------*/
        if(player_Move == false)
        {
            vx = 0;
            if (Input.GetKey("right"))
            {
                vx = speed;
                leftFlag = false;
                anim.SetBool("Walking", true);
            }
            else if (Input.GetKey("left"))
            {
                vx = -speed;
                leftFlag = true;
                anim.SetBool("Walking", true);
            }
            else
            {
                anim.SetBool("Walking", false);
            }

            if (Input.GetKey("space") && groundCheck)
            {
                if (pushFlag == false)
                {
                    jumpFlag = true;
                    pushFlag = true;
                }
                else
                {
                    pushFlag = false;
                }
            }

        }
        /*--------------------------------------------------------------------------*/

        /*体力の減増処理-----------------------------------------------------------------*/
        if (touchFlag || enemyTouchFlag || enemyFollowFlg)
        {
            // 表示
            hpCanvas.SetActive(true);

            // 電気を流す
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if(onElectricity == true)
                {
                    HP -= 20;// HPを減らす
                             // モック版熊倉:HPバー
                    hp.fillAmount = HP / maxHP;
                    Debug.Log(HP);
                    onElectricity = false;

                }

                // モック版熊倉:追加しますた
                // 触れている物がEnemyの場合
                if (enemyTouchFlag)
                {
                    // 追従開始
                    enemyCon.isFollowing = true;
                    // 充電したのでこれ以上充電出来ないように
                    enemyCon.isCharging = false;
                    hpCanvas.SetActive(false);
                }
            }
            // 電気を充電
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                if(onElectricity == false)
                {
                    HP += 20;// HPを増やす
                    hp.fillAmount = HP / maxHP;
                    Debug.Log(HP);
                    // ここに処理を加える
                    onElectricity = true;
                }

                if (enemyFollowFlg)
                {
                    enemyCon.isFollowing = false;
                }
            }
        }
        // モック版熊倉:HP表示するObjectから離れたら強制的にHPバーを非表示にします
        else
        {
            hpCanvas.SetActive(false);
        }
        /*-----------------------------------------------------------------*/
    }

    /*無限ジャンプを防ぐ処理------------------------------------------------*/
    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(vx, rb2d.velocity.y);
        this.GetComponent<SpriteRenderer>().flipX = leftFlag;
        if (jumpFlag)
        {
            jumpFlag = false;
            rb2d.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
        }
    }
    /*-------------------------------------------------------------------*/

    /// <summary>
    /// 敵のレイヤーがあるかどうかを取得する関数
    /// </summary>
    /// <returns></returns>
    private bool GetEnemyLayer()
    {
        Vector3 left = transform.position + Vector3.up * 0.5f - Vector3.right * 3.5f;
        Vector3 right = transform.position + Vector3.up * 0.5f + Vector3.right * 3.5f;
        // ここのコメント消せばデバッグ用の線が見えます
        Debug.DrawLine(left, right);
        return Physics2D.Linecast(left, right, enemyLayer);
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            groundCheck = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HomeApp")
        {
            touchFlag = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HomeApp")
        {
            touchFlag = false;
            hpCanvas.SetActive(false);
        }
        else
        {
            groundCheck = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            touchFlag = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            touchFlag = false;
        }
    }
}


