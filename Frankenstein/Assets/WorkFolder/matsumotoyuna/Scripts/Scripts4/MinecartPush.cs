using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Physics;

//エネミーがトロッコの前でRを押すと、
//トロッコが動く仕組みのスクリプトだよ！
public class MinecartPush : MonoBehaviour
{
    public Rigidbody2D rigid2D;
    private float speed = 2f;
    public bool minecartpush = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floar")
        {
            //特定の床に着くまではZ回転を固定してます
            //その床にFloarタグを設定してくれたまえ
            rigid2D.constraints = RigidbodyConstraints2D.None;
            //int player = LayerMask.NameToLayer("Player");
            //int enemy = LayerMask.NameToLayer("Enemy");
            //Physics.IgnoreLayerCollision(player, enemy);
            //gameObject.layer = LayerName.MineCart;
        }
    }

    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (minecartpush == true)
        {
            MineCartPush();
        }
    }

    public void MineCartPush()
    {
        Transform push = this.transform;
        Vector2 minecartposition = push.position;

        minecartposition.x = Mathf.MoveTowards(minecartposition.x, 3.5f, Time.deltaTime * speed);
        push.position = minecartposition;
    }
}
