using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Charger : MonoBehaviour
{
    LineRenderer line;
    EdgeCollider2D edge;


    Vector2 startPos;              // 初期位置

    private int count;             // 頂点の数
    private bool Chager = false;
    private Vector2[] points = new Vector2[2];

    // 充電ケーブルの距離制限

    // Range1: 設定された位置
    // Range2: 初期位置でない場合
    //        Triggerで動ける範囲を決定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Range2")
        {
            Debug.Log("!!");
            this.gameObject.transform.parent = null;
            Chager = true;
        }

        if (collision.gameObject.tag == "Range1")
        {
            Debug.Log("Event");
            this.gameObject.transform.parent = null;
            Chager = false;
        }
    }

    //　親子関係にする
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Dog")
        {
            if (Input.GetKey(KeyCode.W))
            {
                this.gameObject.transform.parent = GameObject.Find("Dog").transform;

            }
        }
        Chager = true;

    }

    // colliderつけたいけど無理
    void AddCollider()
    {

    }

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        edge = gameObject.AddComponent<EdgeCollider2D>();
        startPos = transform.position;      // 初期位置を格納
    }




    // Wキーを離したらケーブルが初期位置に戻る。
    private void Update()
    {
        if (Chager)
        {
            if (Input.GetKeyUp(KeyCode.W))
            {
                Debug.Log("ok");
                this.gameObject.transform.parent = null;
                transform.position = startPos;              //初期位置
                count = 0;                                  //頂点の数をなくすため線の描画が消える。
            }
        }

    }

    // count を増やすことで頂点の数を増やす。
    // 線を引く
    private void FixedUpdate()
    {
        count += 1;
        line.positionCount = count;
        
        line.SetPosition(count-1,transform.position);
    }

   

    /*
     　＊スクリプト内容＊

      ・LineRendererを使って線を作成。
    　　ケーブルの軌跡が作られる。

      ・ケーブルにWを押すと犬？の子供にしてるため動きは同じ。
    　　離すと解除される。

    　・但しケーブルが一定の位置(ここでは1以上)ではないとき、
    　　初期位置に戻りケーブルの後も消える。


    　・線はTriggerで距離を制限。
    　　Range1に当たりWキーを離してもその場に残る。
        Range2に当たりWキーを離すと初期位置に戻る。
    　
    　・線自体に当たり判定はない。
        どうやってつける？？？？？？？？？

     */

}
