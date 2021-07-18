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
    public float vx = 0;
    private bool leftFlag = false;
    private bool jumpFlag = false;
    private bool groundCheck = false;//接地判定 
    private bool pushFlag = false;
    public bool player_Move = false;

    //
    [SerializeField] private LayerMask enemyLayer; // モック版熊倉:敵のLayer取得用

    public int maxHP = 100;
    public float HP = 100;
    public bool touchFlag = false;
    public bool enemyTouchFlag = false; // モック版熊倉:フラグ追加
    public bool onElectricity = true;
    public GameObject hpCanvas;
    private float hpCanvasScale_x;

    private int presskeyFrames = 0;             //長押しフレーム数
    private int PressLong = 300;                //長押し
    private int PressShort = 100;               //軽押し
    private bool Throw = false;                 //投げのフラグ
    Rigidbody2D rb;
    KeyPlessThrow item;


    private bool enemyFollowFlg = false;
    // モック版熊倉:GetCompornent重いんで直で取得、ここ敵の数増えるはずなので書き換えること
    [SerializeField] private EnemyController enemyCon;
    [SerializeField] private Image hp;

    public SceneChange sc;
    public FadeControl fadeControl;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        hpCanvasScale_x = hpCanvas.transform.localScale.x;
        sc = FindObjectOfType<SceneChange>();
        fadeControl = FindObjectOfType<FadeControl>();
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
                anim.SetBool("Walking", true);
            }
            else if (Input.GetKey("left"))
            {
                vx = -speed;
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

        //
        float x = Input.GetAxisRaw("Horizontal");
        if(x != 0)
        {
            Vector2 Iscale = gameObject.transform.localScale;
            if ((Iscale.x > 0 && x < 0) || (Iscale.x < 0 && x > 0))
            {
                Iscale.x *= -1;
                gameObject.transform.localScale = Iscale;
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

        /*アイテムを持つ入力処理---------------------------------------------*/
        if (Throw)
        {
            if (Input.GetKey(KeyCode.R))//半田：SpaceからRに変更
            {
                //スペースの判定
                //memo  『? true:false』
                presskeyFrames += (Input.GetKey(KeyCode.R)) ? 1 : 0;//半田：SpaceからRに変更
                Debug.Log(presskeyFrames);
            }

            if (Input.GetKeyUp(KeyCode.R))//半田：SpaceからRに変更
            {
                //もしスペースが長押しされたら
                if (PressLong <= presskeyFrames)

                //高めに投げる
                {
                    item.Hight();
                    Debug.Log("長め");
                }

                //もしスペースが押されたら
                else if (PressShort <= presskeyFrames)

                //低めに投げる
                {
                    item.Low();
                    Debug.Log("短め");
                }
            }

            //if (Input.GetKeyUp(KeyCode.W))
            //{
            //    this.gameObject.transform.DetachChildren();
            //}
        }
        /*-----------------------------------------------------------------*/

    }



    /*無限ジャンプを防ぐ処理------------------------------------------------*/
    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(vx, rb2d.velocity.y);
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
            item.transform.parent = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HomeApp")
        {
            touchFlag = true;
        }

        //判定の場所を通過したら発生
        if (collision.gameObject.tag == "GoTitleLogo")
        {
            fadeControl.Fade("wout", () => fadeControl.sceneChange.SceneSwitching("TitleLogo", true));
        }

        if (collision.gameObject.tag == "GoMap2")
        {
            fadeControl.Fade("out", () => fadeControl.sceneChange.SceneSwitching("TentativeTitle"));
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

        if (collision.gameObject.tag == "Item")
        {
            Throw = false;
            presskeyFrames = 0;
            item.transform.parent = null;
            Debug.Log("exit");
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


