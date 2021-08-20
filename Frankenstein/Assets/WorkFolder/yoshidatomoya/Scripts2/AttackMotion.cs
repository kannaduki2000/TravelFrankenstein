using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMotion : MonoBehaviour
{

    public float speed = 1; // スピードX
    public float time = 0;

    Rigidbody2D rb;

    Transform Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 direction = (Player.position - transform.position).normalized;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
 
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // プレイヤータグがついているやつが近づいてきたら
        if (collision.gameObject.tag == "Player")
        {
            //float speed = 0;
            time += Time.deltaTime;
            if (time > 2)
            {
                //   Vector3 direction = (Player.position - transform.position).normalized;
                //  speed = 3.5f;
                //transform.Translate(direction * speed * Time.deltaTime);

                if (transform.position.x < Player.position.x)
                {
                    //右
                    rb.velocity = new Vector2(speed, 0);
                    transform.localScale = new Vector2(1, 1);
                }
                else if (transform.position.x > Player.position.x)
                {
                    //左

                    rb.velocity = new Vector2(-speed, 0);
                    transform.localScale = new Vector2(-1, 1);

                }

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }
}

// エネミーの挙動
// 攻撃処理（突進）
