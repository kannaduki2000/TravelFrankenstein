using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collapse : MonoBehaviour
{
    private bool Fall = false;
    private float speed = 16f;//半田：スピード値の変更

    GameObject Ground;

    void Start()
    {
        Ground = GameObject.Find("CrashGround");//半田：オブジェクト名の変更
    }

    void Update()
    {
        if (Fall)
        {
            Transform ground = Ground.transform;                            //Groundの座標を取得
            Vector2 pos = ground.position;
            pos.y = Mathf.MoveTowards(pos.y, -15, Time.deltaTime * speed); //pos.yから-100までTime.deltaTime * speedのスピードで移動
            ground.position = pos;   //半田：-10から-15に変更
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Fall = true;
        }
    }
    
    /* スクリプト内容 */
//・地面が崩落するスクリプト
//・石の外側にTriggerがあり、触れたら地面が落ちる。
//・sppedで落ちる速さが調節可能
//・-10のところで落ちる高さが変更可能
//・アニメーションがないのでただただ地面落下のみ。
//・カメラ移動はスクリプトが分からなくて触ってないです。
}
