using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    //CharacterController Controller;
    //Transform Target;
    //GameObject Player;

    //[SerializeField]
    //float MoveSpeed = 2.0f;
    //int DetecDist = 8;
    //bool InArea = false;

    // ずっと、往復する
    public float speedX = 1; // スピードX
    public float speedY = 0; // スピードY
    public float speedZ = 0; // スピードZ
    public float second = 1; // かかる秒数
    private float UpdateTimer = 0f;
    private float TimeRimit = 2.0f;
    private bool move = false;

    float time = 0f;

    private void FixedUpdate() // ずっと、往復する
    {
        if(move == true)
        {
            time += Time.deltaTime;
            float s = Mathf.Sin(time * 3.14f / second); // 移動量を求める
            this.transform.Translate(speedX * s / 50, speedY * s / 50, speedZ * s / 50);
        }

        
    }


    // Start is called before the first frame update
    void Start()
    {
        // プレイヤータグの取得
       // Player = GameObject.FindWithTag("Player");
        //Target = Player.transform;

        //Controller = GetComponent<CharacterController>();


    }

    // Update is called once per frame
    void Update()
    {
        
        if (move == false)
        {
            UpdateTimer += Time.deltaTime;
        }
        if (UpdateTimer >= TimeRimit)
        {
            move = true;
            UpdateTimer = 0;
        }
        


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
    }

    /*
    // プレイヤーが検知エリアにいたら検知フラグをtrueにする
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("動いた！");
        InArea = true;
    }
    */
}

// エネミー自身のテリトリー間を行き来する〇
// 待機時間あり（2秒間）〇
// フランケンが近づいてきたら突進する
// フランケンの視線察知はエネミーを中心にする（エネミーが動いたら視線察知も一緒に動く）多分〇
// 突進成功したら４秒間待機　そのあと自分のテリトリーに戻る
// ４秒間待機後、視線察知内にフランケンがいたらフランケンに再び突進
// 突進した場所にフランケンがいなかったら２秒間待機して自分のテリトリーに戻る
// 

