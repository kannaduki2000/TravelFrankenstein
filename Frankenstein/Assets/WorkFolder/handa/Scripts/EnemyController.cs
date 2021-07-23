using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualShockInput;

public class EnemyController : ElectricItem
{
    Rigidbody2D rb2d;
    public PlayerController mt;
    public GameObject Player;
    public GameObject enemy;
    public float stopDistance;  //止まるときの距離
    public float inputSpeed;    //移動速度
    public float jumpingPower;  //ジャンプ

    // モック版熊倉:充電可能かどうかを判別するフラグ
    public bool isCharging = true; // HPが0になったらtrueにするようにしてください
    public bool isFollowing = true;   //追従するかどうか
    public bool enemyMove = true;      //エネミーの動き
    private bool enemyJump = false;         //ジャンプ用
    public bool Follow = false;       //二度目の入力でのついてくるか否か

    public Camera camera;

    // ずっと、往復する
    public float speedX = 1; // スピードX
    public float speedY = 0; // スピードY
    public float speedZ = 0; // スピードZ
    public float second = 1; // かかる秒数
    public bool isWandering = true;//徘徊するかどうか
    float time = 0f;

    Vector3 enemyScale;

    bool cableFlag = false;
    public ElectricCableController ECon;

    CableData cableData;
    [SerializeField] private float moveSpeed;
    private float vx;
    private bool carFlag = false;
    [SerializeField] private CarPush car;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        enemyJump = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ElectricCable")
        {
            cableFlag = true;
            cableData = collision.gameObject.GetComponent<CableData>(); // 電線の情報取得
            // 電線を伝う表示
        }

        if (collision.gameObject.tag == "Car")
        {
            carFlag = true;
            //Follow = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ElectricCable")
        {
            cableFlag = false;
            cableData = null;
        }

        if (collision.gameObject.tag == "Car")
        {
            carFlag = false;
            //Follow = true;
        }
    }

    //↑床に着くまでジャンプさせないマン

    // Start is called before the first frame update
    void Start()
    {
        this.rb2d = GetComponent<Rigidbody2D>();
        enemyScale = transform.localScale;
        IsThrow = true;
    }

    // Update is called once per frame
    void Update()
    {
        // 移動
        rb2d.velocity = new Vector2(vx, rb2d.velocity.y);


        if (cableFlag && enemyMove == false)
        {
            if (Input.GetKeyDown(KeyCode.P) || DSInput.PushDown(DSButton.Square))
            {
                if (cableData.point == CablePoint.start) ECon.CablePointMove(gameObject, cableData.CableNum);
                else ECon.CablePointMove(gameObject, cableData.CableNum, false);
            }
        }

        if (carFlag)
        {
            // 画像の表示
            if (Input.GetKeyDown(KeyCode.R) || DSInput.PushDown(DSButton.R1))
            {
                EnemyNotMove();
                car.crash = true;
                carFlag = false;
            }
        }

        //箱を用意して、その中にY座標を入れる
        Vector2 targetPos = Player.transform.position;
        targetPos.y = transform.position.y;

        //距離
        float distance = Vector2.Distance(transform.position, Player.transform.position);

        // 追従用
        if (isFollowing)
        {
            //if(間の距離が止まるときの距離以上なら?)
            if (distance > stopDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                new Vector2(Player.transform.position.x, enemy.transform.position.y),
                inputSpeed * Time.deltaTime);
            }
            //enemy→player

            // 右
            if (Player.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-enemyScale.x, enemyScale.y, enemyScale.z);
            }

            // 左
            else if (Player.transform.position.x > transform.position.x)
            {
                transform.localScale = enemyScale;
            }

            //ジャンプ
            if (enemyJump == false && (Input.GetKeyDown(KeyCode.Space) || DSInput.PushDown(DSButton.Cross)))
            {
                this.rb2d.AddForce(transform.up * this.jumpingPower);
                enemyJump = !enemyJump;
            }
        }

        //エネミーの動き用
        if (enemyMove == false)
        {
            vx = 0;
            var input = Input.GetAxis("J_Horizontal");
            if (Input.GetKey(KeyCode.LeftArrow) || input < -0.5)
            {
                vx = -moveSpeed;
                //this.transform.Translate(-0.01f, 0.0f, 0.0f);
                transform.localScale = new Vector3(-enemyScale.x, enemyScale.y, enemyScale.z);
            }

            if (Input.GetKey(KeyCode.RightArrow) || 0.5 < input)
            {
                vx = moveSpeed;
                //this.transform.Translate(0.01f, 0.0f, 0.0f);
                transform.localScale = enemyScale;
            }

            if (enemyJump == false && (Input.GetKeyDown(KeyCode.Space) || DSInput.PushDown(DSButton.Cross)))
            {
                this.rb2d.AddForce(transform.up * this.jumpingPower);
                enemyJump = !enemyJump;
            }
            input = 0;
        }

        //★操作の切り替え処理
        //1回目の切り替え時の動き
        if(isFollowing)
        {
            if ((Input.GetKeyDown(KeyCode.F) || DSInput.PushDown(DSButton.L1)) && Follow == false)
            {
                // カメラ追従の対象をエネミーに変更
                camera.GetComponent<CameraClamp>().targetToFollow = gameObject.transform;
                mt.player_Move = !mt.player_Move;
                Following();
                enemyMove = !enemyMove;
                Follow = !Follow;
            }
        }
        //2回目の切り替え時、プレイヤーだけ動いてエネミー不動堂
        //この状態だと何回Enter押してもプレイヤーしか動かんで
        else if ((Input.GetKeyDown(KeyCode.F) || DSInput.PushDown(DSButton.L1)) && Follow == true)
        {
            camera.GetComponent<CameraClamp>().targetToFollow = Player.transform;
            isFollowing = false;
            enemyMove = true;
            mt.player_Move = false;
        }

        //呼ぶボタン(Delete仮置き)を押した時の動き
        //Followを切り替えることでもう一度追従や切り替えができるお
        if (Follow == true && (Input.GetKeyDown(KeyCode.Delete ) || DSInput.PushDown(DSButton.R1)) && enemyMove == true && isFollowing)
        {
            isFollowing = true;
            Follow = !Follow;
        }

    }

    private void FixedUpdate() // ずっと、往復する
    {

        if (isWandering == true)
        {
            time += Time.deltaTime;
            float s = Mathf.Sin(Time.time);
            this.transform.Translate(speedX * s / 50, speedY * s / 50, speedZ * s / 50);
            Vector3 scale = transform.localScale;
            if (s >= 0)
            {
                scale.x = enemyScale.x;
            }
            else
            {
                scale.x = -enemyScale.x;
            }
            transform.localScale = scale;
        }
        if (isFollowing == true)
        {
            isWandering = false;
        }


    }

    public void EnemyMove()
    {
        enemyMove = false;
    }

    public void EnemyNotMove()
    {
        enemyMove = true;
        vx = 0;
        rb2d.velocity = Vector2.zero;
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
        mt.player_Move = !mt.player_Move;

        // 操作権を敵に移動させる
        Following();
        enemyMove = !enemyMove;
    }
}
