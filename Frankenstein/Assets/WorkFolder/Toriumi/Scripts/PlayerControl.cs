using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Vector2 Speed = new Vector2(1, 1);   //速度
    private int presskeyFrames = 0;             //長押しフレーム数
    private int PressLong = 300;                //長押し判定の閾値
    private int PressShort = 100;               //軽く押した判定の閾値
    private bool Throw = false;                 //投げのフラグ
    Rigidbody2D rb;
    Item item;
    



    void Start()
    {
        //Animetorコンポネーションを取得する
        //anim = GetComponent<Animator>();

    }

    // アップデートはフレームごとに1回呼び出される
    void Update()
    {
        //移動
        Vector2 Position = transform.position;
        if(Input.GetKey(KeyCode.A))
        {
            Position.x -= Speed.x;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            Position.x += Speed.x;
        }
        transform.position=Position;

        //向き反転
        float x= Input.GetAxisRaw("Horizontal");
        if(x !=0)
        {
            Vector2 Iscale = gameObject.transform.localScale;
            if((Iscale.x > 0 && x < 0) || (Iscale.x < 0 && x > 0))
            {
                Iscale.x *= -1;
                gameObject.transform.localScale = Iscale;
            }
        }

        if (Throw)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                //スペースの判定
                //memo  『? true:false』
                presskeyFrames += (Input.GetKey(KeyCode.Space)) ? 1 : 0;
                Debug.Log(presskeyFrames);
            }

            if (Input.GetKeyUp(KeyCode.Space))
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

            if (Input.GetKeyUp(KeyCode.W))
            {
                this.gameObject.transform.DetachChildren();
            }
        }
    }

    private void FixedUpdate()
    {
        
    }

    //アイテムに当たったら
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            //アニメーションが再生される
            Debug.Log("アニメーション再生");          
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            Throw = false;
            presskeyFrames = 0;
            this.gameObject.transform.DetachChildren();
            Debug.Log("exit");
        }
    }


    //アイテムに当たり続けたら
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag =="Item")
        {
            Debug.Log("stay");

            //item = collision.gameObject.GetComponent<Item>();
            //Wを押していたら
            if (Input.GetKey(KeyCode.W))
            {
                Throw = true;
                //アイテムクラスの取得
                item = collision.gameObject.GetComponent<Item>();

                //アイテムのY軸が上がる
                // ここでこのオブジェクトをプレイヤーの子供にする
                item.gameObject.transform.parent = this.transform;
            }
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            this.gameObject.transform.DetachChildren();
        }
    }


}
