using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using DualShockInput;




public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public Animator anim;
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

    [SerializeField] private float inputRange = 0.5f;
    public int maxHP = 100;
    public float HP = 100;
    public bool touchFlag = false;
    public bool enemyTouchFlag = false; // モック版熊倉:フラグ追加
    public bool onElectricity = true;
    public GameObject hpCanvas;
    public GameObject canvasParent;
    private float canvasParentScale_x;

    private int presskeyFrames = 0;             //長押しフレーム数
    private int PressLong = 300;                //長押し
    private int PressShort = 100;               //軽押し
    private bool Throw = false;                 //投げのフラグ
    private bool Getitem = false;               //半田：itemを持っているflag
    Rigidbody2D rb;
    [SerializeField] KeyPlessThrow item;

    [SerializeField] private ImageData imageData;
    public Image AnnounceImage;
    public Canvas TitleLogo;


    private bool enemyFollowFlg = false;
    // モック版熊倉:GetCompornent重いんで直で取得、ここ敵の数増えるはずなので書き換えること
    [SerializeField] private EnemyController enemyCon;
    [SerializeField] private Image hp;

    public SceneChange sc;
    public FadeControl fadeControl;

    // シーン遷移が多重で呼ばれないようにする
    private bool titleLogoflag = false;
    private bool map2Flag = false;

    public TextController textCon;

    public ElectricItem electricItem;

    private bool getUpTrigger = true;
    private EventBandController eventBandCon;


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
    }

    // Update is called once per frame
    void Update()
    {
        //if (enemyCon.isFollowing) { return; }

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
                    }); }
                }
            }
        }
        if (EventFlagManager.Instance.GetFlagState(EventFlagName.isFade))
        {
            PlayerNotMove();
            return;
        }


        //anim = gameObject.GetComponent<Animator>();
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
                //@item = enemy.GetComponent<KeyPlessThrow>();
                //@if (electricItem.IsThrow) Throw = true;

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
            var input = Input.GetAxis("J_Horizontal");
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
           

            if ((Input.GetKey("space") || DSInput.PushDown(DSButton.Cross)) && groundCheck)
            {
                SystemTextEndPlayerMove();
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
        }

        /*--------------------------------------------------------------------------*/
        
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
                }
            }
            // 電気を充電
            else if (Input.GetKeyDown(KeyCode.Backspace) || DSInput.PushDown(DSButton.Square))
            {
                //if (onElectricity == false || electricItem.ChargeFlag)
                //{
                //    Debug.Log("充電したい");
                //    HP += electricItem.Power;
                //    //HP += 20;// HPを増やす
                //    hp.fillAmount = HP / maxHP;
                //    // ここに処理を加える
                //    onElectricity = true;
                //    electricItem.ChargeFlag = false;
                //}

                // ？？？
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

            //if (Input.GetKeyUp(KeyCode.W))
            //{
            //    this.gameObject.transform.DetachChildren();
            //}
        }
        /*-----------------------------------------------------------------*/


        // @投げる処理
        //if (electricItem != null)
        //{
        //    if (electricItem.IsThrow)
        //    {
        //        if (Input.GetKey(KeyCode.W))
        //        {
        //            Debug.Log("掴んだ");
        //            // ここでこのオブジェクトをプレイヤーの子供にする
        //            item.gameObject.transform.parent = this.transform;
        //        }
        //        if (Input.GetKeyUp(KeyCode.W))
        //        {
        //            Debug.Log("離した");
        //            item.transform.parent = null;
        //        }
        //    }
        //}



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
        player_Move = true;
        vx = 0;
        rb2d.velocity = Vector2.zero;
        anim.SetBool("Walking", false);
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

            // 投げれるObjectなら情報を取得
            //if (electricItem.IsThrow)
            //{
            //    Throw = true;
            //    //アイテムクラスの取得
            //    item = collision.gameObject.GetComponent<KeyPlessThrow>();
            //}
        }

        if (collision.gameObject.tag == "HomeApp")
        {
            //SetAnnounceImage(AnnounceName.T_SquareButton_Input);
            touchFlag = true;
        }

        //判定の場所を通過したら発生
        if (collision.gameObject.tag == "GoTitleLogo" && titleLogoflag == false && EventFlagManager.Instance.GetFlagState(EventFlagName.enemyCharge))
        {
            // 複数判定を防ぐ為のフラグ
            titleLogoflag = true;
            fadeControl.Fade("wout", () => sc.SceneSwitching("TitleLogo", true));
        }

        if (collision.gameObject.tag == "GoTitle")
        {
            fadeControl.Fade("out", () => sc.SceneSwitching("MainTitle"));
        }

        if (collision.gameObject.tag == "GoMap2")
        {
            map2Flag = true;
            fadeControl.Fade("out", () => fadeControl.sceneChange.SceneSwitching("TentativeTitle"));
        }

        // ケーブルカーが来るフラグ
        if (collision.gameObject.tag == "CableCarEvent")
        {
            EventFlagManager.Instance.SetFlagState(EventFlagName.cableCarStart, true);
        }

        // ケーブルカーに乗車or降車
        if (collision.gameObject.tag == "CableCarEventCollider")
        {
            collision.gameObject.GetComponent<BusEventCollider>().BusEvent(gameObject);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ElectricItem>() != null)
        {
            //if (electricItem != null && electricItem.IsThrow)
            //{
            //    Throw = false;
            //    presskeyFrames = 0;
            //    item = null;
            //}
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

        // 一旦消す
        //if (collision.gameObject.tag == "Item")
        //{
        //    Throw = false;
        //    presskeyFrames = 0;
        //    //item.transform.parent = null;
        //    Debug.Log("exit");
        //}
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


