using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{
    public Vector2 warpPoint;   //移動先の座標
    private Vector3 target;     //移動元の座標
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            target = collision.gameObject.transform.position; //移動元の座標を取得
            target.x = warpPoint.x;                           //移動元X ⇒ 移動先X
            target.y = warpPoint.y;                           //移動元Y ⇒ 移動先Y
            collision.gameObject.transform.position = target; //移動先の座標に変更
        }
    }

    /*移動する先の注意*/
    //＊この方法はTileMapで使う方法である
    //行きと帰りで移動元と移動先の座標をずらす
    //(＊移動先が重なると無限にワープをするため,1～２マス程離す)
    //当たり判定やレイヤーの順番を変えるのなら、行きと帰りにTagをつけて移動対象にその処理をつける
}
