using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Physics;

//エネミーがトロッコの前でRを押すと、
//トロッコが動く仕組みのスクリプトだよ！
public class MinecartPush : MonoBehaviour
{
    public Rigidbody2D rigid2D;
    public PhysicsMaterial2D physics2D;
    public GameObject MineCart;
    private float speed = 2f;
    public bool minecartpush = false;  //坂からトロッコ落とす用
    public bool enemytouch = false;    //エネミーとすれ違う用、坂から落ちている最中はすれ違う
    public bool enemyrpush = false;    //エネミーがトロッコ引っ張れる用
    public bool movestop = false;      //落ちている最中用、止まったらtrue

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floar")
        {
            //特定の床に着くまではZ回転を固定してます
            //その床にFloarタグを設定してくれたまえ
            rigid2D.constraints = RigidbodyConstraints2D.None;
            enemytouch = true;
        }

        //ボタン押した後になんかつるつる滑りやがるため、X固定しちゃったお
        else if (collision.gameObject.name == "Button")
        {
            rigid2D.constraints = RigidbodyConstraints2D.FreezePositionX;
            Invoke("MoveStop", 0.5f);
        }

        //トロッコとプレイヤーのすれ違い対策
        //レイヤーの設定してね！
        //(project setting? のところのやつ)

        else if (collision.gameObject.name == "Enemy" && enemytouch == true)
        {
            enemytouch = false;
        }
    }

    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //エネミーがトロッコに触れた & R押したらここ
        if (minecartpush == true)
        {
            MineCartPush();
        }

        //エネミーが手でトロッコを押せる用に重さ変更
        if (enemyrpush == true)
        {
            rigid2D.mass = 1;
        }

        else if(enemyrpush == false)
        {
            rigid2D.mass = 500;
        }
    }

    public void MineCartPush()
    {
        //坂下っていく部分
        if (movestop == false)
        {
            MineCart.gameObject.layer = 9;
            Transform push = this.transform;
            Vector2 minecartposition = push.position;

            minecartposition.x = Mathf.MoveTowards(minecartposition.x, 3.5f, Time.deltaTime * speed);
            push.position = minecartposition;

            Invoke("MoveStop", 2.0f);
        }
    }

    public void MoveStop()
    {
        //坂を降り終わったらここ
        movestop = true;
        rigid2D.constraints = RigidbodyConstraints2D.None;
        MineCart.gameObject.layer = 8;
    }
}
