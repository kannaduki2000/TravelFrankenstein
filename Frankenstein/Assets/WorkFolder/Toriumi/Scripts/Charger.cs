using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Charger : MonoBehaviour
{
    GameObject Ra1;                //イベント発生範囲 
    GameObject Ra2;                //壁


    Vector2 startPos;              // 初期位置

    private bool Remove = false;   // 親子解除、初期位置に戻る、ケーブルのスケールを0にするフラグ

    private void Start()
    {
        startPos = transform.position;      // 初期位置を格納

        Remove = true;                      

        this.Ra1 = GameObject.Find("Range1");
        this.Ra2 = GameObject.Find("Range2");

        Ra1.SetActive(false);   //  イベント範囲非表示
        Ra2.SetActive(false);   //　壁非表示
    }

    private void Update()
    {
        if (Remove)
        {
            if (Input.GetKeyUp(KeyCode.W))
            {
                Debug.Log("ok");
                this.gameObject.transform.parent = null;                                    //親子関係の解除
                transform.position = startPos;                                              //初期位置

                Vector3 vec = GameObject.Find("Cabels").transform.localScale;
                GameObject.Find("Cabels").transform.localScale = new Vector3(0, 0, 0);      //Cablesのスケールを0(非表示にする)

                Ra1.SetActive(false);   //  イベント範囲非表示
                Ra2.SetActive(false);   //　壁非表示
            }
        }
    }

    //　親子関係にする
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Dog")
        {
         
            // Wキーを押していると親子関係になる
            if (Input.GetKey(KeyCode.W))
            {
                this.gameObject.transform.parent = GameObject.Find("Dog").transform;    //Dog を親にする

                Vector3 vec = GameObject.Find("Cabels").transform.localScale;
                GameObject.Find("Cabels").transform.localScale = new Vector3(1, 1, 1);  //Cablesのスケールを1(表示する)

                Ra1.SetActive(true);    //　イベント範囲表示
                Ra2.SetActive(true);    //　壁表示

            }
        }
    }

    // 充電ケーブルの距離制限
    // Triggerでイベント範囲を決定
    // Collisionで壁を生成。

    // Range1: 設定された位置
    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.gameObject.tag == "Range1")
            {
                Remove = false;

                Debug.Log("ok?");

                this.gameObject.transform.parent = null;

                Debug.Log("親子解除されない…");
            }
    }


    /*
     　＊スクリプト内容＊

    ・ Hinge Joint を使って四角をつなげてケーブルを作成。
    ・ Dogのスクリプト内容がわからないため、仮の物を入れています。
    ・ tagを3種類追加しました。
        Dog、changer、Range1
    
       *距離制限*
       ・Range1 でイベント判定を作っています。
       ・Range2 で壁を作っています。
       ・どちらも始めは非表示ですが、Dogとケーブルが親子になったら
         表示されるようになっています。
     
       *ケーブルを伸ばす*
       ・Hinge Jointを使ってつなげています。
       ・SetActiveを使うとバグが発生するので、
         スケールを0(非表示)と1(表示)で表示、非表示を設定しています。
       ・Rigidbodyの関係とケーブルの位置でケーブルが荒ぶる可能性があります。
         また、Rigidbodyの関係でケーブルが床をすり抜ける恐れが多いにあります。
         ケーブルを増やせばすり抜けが減りますが、
         代わりにケーブルがとんでもなく重なるので滅茶苦茶荒ぶります。

    　*初期位置に戻る*
    　 ・範囲外や途中で離した場合、初期位置に戻ります。
       ・初期位置はtransform.positionで設定しています。
       
        以上です、よろしくお願いします。
     */

}
