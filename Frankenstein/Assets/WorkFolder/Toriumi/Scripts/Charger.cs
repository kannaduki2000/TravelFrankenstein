using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Charger : MonoBehaviour
{
    LineRenderer line;
    private 
    int count;
    private void Start()
    {
        line = GetComponent<LineRenderer>();

    }
    private void Update()
    {
        if (this.transform.position.x >= 0)
        {
            if (Input.GetKeyUp(KeyCode.W))
            {
                Debug.Log("ok");
                this.gameObject.transform.parent = null;
            }
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.W))
            {
                Debug.Log("ok");
                this.gameObject.transform.parent = null;
                Transform charger = this.transform;
                Vector2 pos = charger.position;
                pos.x = -5.5f;
                pos.y = -1.9f;
                charger.position = pos;
            }
            
        }
    }

    private void FixedUpdate()
    {
        count += 1;
        line.positionCount = count;
        line.SetPosition(count-1,transform.position);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Dog")
        {
            if(Input.GetKey(KeyCode.W))
            {
                this.gameObject.transform.parent = GameObject.Find("Dog").transform;
            }
        }
    }

    /*
     　＊スクリプト内容＊

      ・LineRendererを使って線を作成。
    　　ケーブルの後の軌跡が作られる。
      ・線自体に当たり判定がない。
      ・ケーブルにWを押すと犬？の子供にしてるため動きは同じ。
    　　離すと解除される。
    　・但しケーブルが一定の位置(ここでは1以上)ではないとき、
    　　初期値(ここではそのままの初期値)に戻る。
    　・そうでない場合はケーブルはその場に取り残される。
    　
    　＊修正点＊

    　・線はいくらでも自由自在に描ける。
    　・線がその場に残ったまま書き続けられる。
    　

     */

}
