using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using DualShockInput;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public Animator anim;
    public GameObject Player;
    public GameObject enemy;

    public float speed;                         //速度
    public float input;
    public float jumpPower;                     //ジャンプの強さ
    public float vx = 0;
    private bool jumpFlag = false;              //ジャンプをしたかどうか
    private bool groundCheck = false;　         //接地判定 
    private bool pushFlag = false;              //ジャンプボタンを押したかどうか
    public bool player_Move = false;            //プレイヤーが動けるかどうか

    [SerializeField] private LayerMask enemyLayer; // モック版熊倉:敵のLayer取得用

    [SerializeField] private float inputRange = 0.5f;
    public int maxHP = 100;                     //最大体力
    public float HP = 100;                      //現在の体力
    public bool touchFlag = false;              //電気を流せる物に触れているか
    public bool enemyTouchFlag = false;         // モック版熊倉:フラグ追加
    public bool enemyOnElect = false;
    public bool onElectricity = true;           //電気を流しているかどうか
    public GameObject hpCanvas;                 //HPバーのオブジェクト
    public GameObject canvasParent;             
    private float canvasParentScale_x;          //HPバーの反転防止

    private int presskeyFrames = 0;             //長押しフレーム数
    private int PressLong = 300;                //長押し
    private int PressShort = 100;               //軽押し
    private bool Throw = false;                 //投げのフラグ
    private bool Getitem = false;               //半田：itemを持っているflag
    [SerializeField] KeyPlessThrow item;

    [SerializeField] private ImageData imageData;
    public Image AnnounceImage;
    public Canvas TitleLogo;

    private bool enemyFollowFlg = false;
    [SerializeField] private EnemyController enemyCon;
    [SerializeField] private Image hp;

    public SceneChange sc;
    public FadeControl fadeControl;

    [SerializeField] private GoofCon goofCon;
    public bool haveBone = false;

    // シーン遷移が多重で呼ばれないようにする
    private bool titleLogoflag = false;
    private bool map2Flag = false;

    public TextController textCon;

    public ElectricItem electricItem;

    private bool getUpTrigger = true;
    private EventBandController eventBandCon;
    private bool endFlagTrigger = false;
    [SerializeField] private EveCon eve;

    public bool istouchingpuller = false;
    public float pullforce;

    public EnemyController[] FollEnemy;

    [SerializeField] crane cra;
    private bool craneFlag = false;

    public GameObject hpbar;
    public bool on_damage = false;       //ダメージフラグ
    private SpriteRenderer renderer;

    [SerializeField] public AudioClip kidou;
    [SerializeField] public AudioClip jumpHigh;
    [SerializeField] public AudioClip jumpRow;
    [SerializeField] public AudioClip aruku;
    [SerializeField] public AudioClip taipSE;
    [SerializeField] public AudioClip sceneChange;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        canvasParentScale_x = canvasParent.transform.localScale.x;
        sc = FindObjectOfType<SceneChange>();
        fadeControl = FindObjectOfType<FadeControl>();
        imageData = FindObjectOfType<ImageData>();
        eventBandCon = FindObjectOfType<EventBandController>();

        //点滅処理の為に呼び出しておく
        renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // フランケンがまだ起き上がっていなければ
        if (EventFlagManager.Instance.GetFlagState(EventFlagName.frankensteinGetUp) == false)
        {
            return;
        }
        else
        {
            // 一度でも起き上がったことがあれば起きるアニメーションのスキップ
            if (EventFlagManager.Instance.GetFlagState(EventFlagName.getupFlag))
            {
                anim.SetBool("GetUpFlag", true);
                getUpTrigger = false;
                
            }

            if (getUpTrigger)
            {
                player_Move = true;
                PlayerSetAnnounceImage(AnnounceName.T_CircleButton_StartUp);
                // 〇ボタン
                if (Input.GetKeyDown(KeyCode.Return) || DSInput.PushDown(DSButton.Circle))
                {
                    ViewAnnounceImage(false);
                    getUpTrigger = false;
                    if (eventBandCon != null) { eventBandCon.EventStart(() =>
                    {
                        EventFlagManager.Instance.SetFlagState(EventFlagName.getupFlag, true);
                        anim.SetTrigger("isGetUp");
                        SEConveyer.instance.PlaySE(kidou);
                    }); }
                }
            }
        }
        if (EventFlagManager.Instance.GetFlagState(EventFlagName.isFade))
        {
            PlayerNotMove();
            return;
        }

        // モック版熊倉:LayerでやってたっぽいのでLinecastで取得
        if (GetEnemyLayer())
        {
            // 電気の吸収イベントが終了してからでないとHPバーすら表示しない
            if (EventFlagManager.Instance.GetFlagState(EventFlagName.electricAabsorption))
            {
                electricItem = enemy.GetComponent<ElectricItem>();
                if (electricItem.IsChargeEvent == false && EventFlagManager.Instance.GetFlagState(EventFlagName.enemyCharge) == false)
                {
                    PlayerSetAnnounceImage(AnnounceName.T_Put_Electric_Enemy);
                }
                enemyTouchFlag = true;
                hpCanvas.SetActive(true);
            }
        }
        else
        {
            if (enemyTouchFlag)
            {
                electricItem = null;
                ViewAnnounceImage(false);
            }
            //@item = null;
            //@Throw = false;
            enemyTouchFlag = false;
            hpCanvas.SetActive(false);
        }


        /*プレイヤーの移動入力処理--------------------------------------------*/
        if(player_Move == false)
        {

            vx = 0;
            input = Input.GetAxis("J_Horizontal");
            if (Input.GetKey("right") || inputRange < input)
            {
                SystemTextEndPlayerMove();
                vx = speed;
                anim.SetBool("Walking", true);
                // HPバーの向き
                canvasParent.transform.localScale = new Vector3(canvasParentScale_x, canvasParent.transform.localScale.y, canvasParent.transform.localScale.x);

                
            }
            else if (Input.GetKey("left") || input < -inputRange)
            {
                SystemTextEndPlayerMove();
                vx = -speed;
                anim.SetBool("Walking", true);
                canvasParent.transform.localScale = new Vector3(-canvasParentScale_x, canvasParent.transform.localScale.y, canvasParent.transform.localScale.x);

                
            }
            else
            {
                anim.SetBool("Walking", false);
            }

            if(istouchingpuller == true)
            {
                if (vx > 0)
                {
                    rb2d.velocity = new Vector2(vx * (speed - 3), rb2d.velocity.y);
                }
                if (vx < 0)
                {
                    rb2d.velocity = new Vector2(-(pullforce + speed), rb2d.velocity.y);
                }
            }
           

            if ((Input.GetKey("space") || DSInput.Push(DSButton.Cross)) && groundCheck)
            {
                SystemTextEndPlayerMove();
                if (pushFlag == false)
                {
                    jumpFlag = true;
                    pushFlag = true;

                    if(!jumpFlag)
                    {
                        SEConveyer.instance.PlaySE(jumpHigh);
                    }
                }
                else
                {
                    pushFlag = false;
                }
            }

            //
            float x = Input.GetAxisRaw("Horizontal");
            if(input != 0)
            {
                //SystemTextEndPlayerMove();
                Vector2 Iscale = gameObject.transform.localScale;
                if ((Iscale.x < 0 && inputRange < input) || (Iscale.x > 0 && input < -inputRange))
                {
                    Iscale.x *= -1;
                    gameObject.transform.localScale = Iscale;
                }
            }
            input = 0;

            /*体力の減増処理-----------------------------------------------------------------*/
            if (touchFlag || enemyTouchFlag || enemyFollowFlg || electricItem != null)
            {
                if (enemyCon.isFollowing || textCon.textFlag) { return; }

                // 表示
                hpCanvas.SetActive(true);


                if (Input.GetKeyDown(KeyCode.Backspace) || DSInput.PushDown(DSButton.Square))
                {
                    ViewAnnounceImage(false);

                    // 電気を流す
                    if (onElectricity == true || electricItem.ChargeFlag == false)
                    {
                        electricItem.ChargeFlag = true;
                        HP -= electricItem.Power;
                        hp.fillAmount = HP / maxHP;
                        onElectricity = false; // これいるんかな
                                               // 入れたObject毎のイベントの実行
                        electricItem.Event();
                        electricItem.IsChargeEvent = true;

                    }

                    // 充電する
                    else if ((onElectricity == false || electricItem.ChargeFlag) && electricItem.IsCharge)
                    {
                        HP += electricItem.Power;
                        //HP += 20;// HPを増やす
                        hp.fillAmount = HP / maxHP;
                        // ここに処理を加える
                        onElectricity = true;
                        electricItem.ChargeEvent();
                        electricItem.ChargeFlag = false;
                    }

                    // モック版熊倉:追加しますた
                    // 触れている物がEnemyの場合
                    if (enemyTouchFlag && EventFlagManager.Instance.GetFlagState(EventFlagName.electricAabsorption)) // チュートリアルの吸収フラグがないと追従しないように
                    {
                        EventFlagManager.Instance.SetFlagState(EventFlagName.enemyCharge, true);
                        // 追従開始
                        enemyCon.isFollowing = true;
                        // 充電したのでこれ以上充電出来ないように
                        enemyCon.isCharging = false;
                        hpCanvas.SetActive(false);
                        enemyOnElect = true;

                        
                    }
                }
            }

            if (craneFlag)
            {
                if (DSInput.PushDown(DSButton.Square))
                {
                    craneFlag = false;
                    cra.craneMove = true;
                }
            }
            /*--------------------------------------------------------------------------*/

            // ダメージフラグがtrueで有れば点滅させる
            if (on_damage)
            {
                float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
                renderer.color = new Color(1f, 1f, 1f, level);
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
            if (Input.GetKey(KeyCode.R) || DSInput.PushDown(DSButton.Circle))//半田：SpaceからRに変更
            {
                //スペースの判定
                //memo  『? true:false』
                presskeyFrames += (Input.GetKey(KeyCode.R) || DSInput.PushDown(DSButton.Circle)) ? 1 : 0;//半田：SpaceからRに変更
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

                    Getitem = false;
                    item.gameObject.transform.parent = null;
                }

                //もしスペースが押されたら
                else if (PressShort <= presskeyFrames)

                //低めに投げる
                {
                    item.Low();
                    Debug.Log("短め");

                    Getitem = false;
                    item.gameObject.transform.parent = null;
                }
            }

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

    /// <summary>
    /// アナウンス画像を入れてから表示
    /// </summary>
    /// <param name="name"></param>
    public void PlayerSetAnnounceImage(AnnounceName name)
    {
        AnnounceImage.sprite = imageData.GetAnnounceImage(name);
        ViewAnnounceImage(true);

    }

    /// <summary>
    /// アナウンス画像の表示/非表示
    /// </summary>
    /// <param name="isView"></param>
    public void ViewAnnounceImage(bool isView)
    {
        AnnounceImage.enabled = isView;
    }

    /// <summary>
    /// テキストの表示後にプレイヤーが動いたらアナウンス画像を非表示にする　名前が適当すぎる
    /// </summary>
    private void SystemTextEndPlayerMove()
    {
        if (EventFlagManager.Instance.GetFlagState(EventFlagName.textSystem) && 
            EventFlagManager.Instance.GetFlagState(EventFlagName.textSystemEnd) == false)
        {
            EventFlagManager.Instance.SetFlagState(EventFlagName.textSystemEnd, true);
            ViewAnnounceImage(false);
        }
    }

    /// <summary>
    /// テキストの表示(アニメーション用)
    /// </summary>
    public void TextAnim()
    {
        textCon.SetTextActive(true);
        SEConveyer.instance.PlaySE(taipSE);
    }

    /// <summary>
    /// アニメーション用
    /// </summary>
    public void FadeOut()
    {
        if (endFlagTrigger) return;
        endFlagTrigger = true;
        fadeControl.Fade("out", ()=>
        {
            eventBandCon.InitEventBand();
            fadeControl.sceneChange.SceneSwitching("TrueEndScene");
        });
    }

    /// <summary>
    /// 移動可能
    /// </summary>
    public void PlayerMove()
    {
        player_Move = false;
    }

    /// <summary>
    /// 移動不可能
    /// </summary>
    public void PlayerNotMove()
    {
        input = 0;
        player_Move = true;
        vx = 0;
        rb2d.velocity = Vector2.zero;
        anim.SetBool("Walking", false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //地面判定
        if(collision.gameObject.tag == "Ground" || collision.gameObject.tag == "BackGround" || collision.gameObject.tag == "pull")
        {
            groundCheck = true;
        }

        if (collision.gameObject.tag == "Item")
        {
            Debug.Log("stay");

            //Wを押していたら
            if (Input.GetKey(KeyCode.W) || DSInput.Push(DSButton.R1))
            {
                Throw = true;
                //アイテムクラスの取得
                item = collision.gameObject.GetComponent<KeyPlessThrow>();

                //アイテムのY軸が上がる
                // ここでこのオブジェクトをプレイヤーの子供にする
                item.gameObject.transform.parent = this.transform;

                //itemを持ったらtrue
                Getitem = true;
            }

            if (Getitem == true)
            {
                if (Input.GetKeyUp(KeyCode.W) || DSInput.PushUp(DSButton.R1))
                {
                    item.gameObject.transform.parent = null;

                    //tiemを放したらfalse
                    Getitem = false;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ElectricItem>() != null)
        {
            electricItem = collision.gameObject.GetComponent<ElectricItem>();
        }

        if (collision.gameObject.tag == "HomeApp")
        {
            touchFlag = true;
        }

        //判定の場所を通過したら発生
        if (collision.gameObject.tag == "GoTitleLogo" && titleLogoflag == false && EventFlagManager.Instance.GetFlagState(EventFlagName.enemyCharge))
        {
            // 複数判定を防ぐ為のフラグ
            titleLogoflag = true;
            fadeControl.Fade("wout", () => sc.SceneSwitching("TitleLogo", true));
            SEConveyer.instance.PlaySE(sceneChange);
        }

        if (collision.gameObject.tag == "GoTitle")
        {
            fadeControl.Fade("out", () => sc.SceneSwitching("MainTitle"));
        }

        if (collision.gameObject.tag == "GoMap2")
        {
            map2Flag = true;
            fadeControl.Fade("out", () => sc.SceneSwitching("Main_Stag2"));
        }

        // ケーブルカーに乗車or降車
        if (collision.gameObject.tag == "CableCarEventCollider")
        {
            collision.gameObject.GetComponent<BusEventCollider>().BusEvent(gameObject);
        }

        if(collision.gameObject.tag == "WarpIN")
        {
            Player.gameObject.layer = 12;
        }

        if(collision.gameObject.tag == "WarpOut")
        {
            Player.gameObject.layer = 6;
        }

        if (collision.gameObject.tag == "pull")
        {
            istouchingpuller = true;
        }

        if (collision.gameObject.tag == "Crane")
        {
            craneFlag = true;
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ElectricItem>() != null)
        {
            electricItem = null;
        }

        if (collision.gameObject.tag == "HomeApp")
        {
            touchFlag = false;
            hpCanvas.SetActive(false);
        }
        else
        {
            groundCheck = false;
        }

        if(collision.gameObject.tag == "pull")
        {
            istouchingpuller = false;
        }

        if (collision.gameObject.tag == "Crane")
        {
            craneFlag = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            touchFlag = true;
        }

        if(collision.gameObject.tag == "Bone")
        {
            haveBone = true;
        }

        //  敵とぶつかったかつダメージフラグがfalse
        if (!on_damage && collision.gameObject.tag == "Dog")
        {
            OnDamageEffect();

            HP -= 20;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            touchFlag = false;
        }
    }

    void OnDamageEffect()
    {
        // ダメージフラグON
        on_damage = true;

        //// プレイヤーの位置を後ろに飛ばす
        //float s = 100f * Time.deltaTime;
        //transform.Translate(Vector3.up * s);

        //// プレイヤーのlocalScaleでどちらを向いているのかを判定
        //if (transform.localScale.x >= 0)
        //{
        //    transform.Translate(Vector3.left * s);
        //}
        //else
        //{
        //    transform.Translate(Vector3.right * s);
        //}

        // コルーチン開始
        StartCoroutine("WaitForIt");
    }

    IEnumerator WaitForIt()
    {
        // 1秒間処理を止める
        yield return new WaitForSeconds(1);

        // １秒後ダメージフラグをfalseにして点滅を戻す
        on_damage = false;
        renderer.color = new Color(1f, 1f, 1f, 1f);
    }
}


