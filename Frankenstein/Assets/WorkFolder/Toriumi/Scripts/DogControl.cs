using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogControl : MonoBehaviour
{

    // 仮で作ったスクリプト

    public Vector2 Speed = new Vector2(1, 1);   //速度
    public float jumpForce = 300.0f;            //ジャンプ力
    private bool Jump = false;
    Rigidbody2D rigid2D;
    Charger Charger;

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        //移動
        Vector2 Position = transform.position;
        if (Input.GetKey(KeyCode.A))
        {
            Position.x -= Speed.x;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Position.x += Speed.x;
        }
        transform.position = Position;

        if (Jump == false && Input.GetKeyDown(KeyCode.Space))
        {
            this.rigid2D.AddForce(transform.up * this.jumpForce);
            Jump = !Jump;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Jump = false;
    }
}
