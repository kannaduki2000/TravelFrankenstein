using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsGimmick : MonoBehaviour
{
    GameObject anim;

    //アニメーションはtransformで設定してあります。
    //電気を入れるの代わりにPを押す

    private void OnCollisionStay2D(Collision2D collision)
    {
        //階段のオブジェクト取得
        anim = GameObject.Find("Stairs");

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("物に当たってる");
            if (Input.GetKey(KeyCode.P))
            {
                //StairsAnimからアニメーション再生スクリプト呼び出し
                
                anim.gameObject.GetComponent<StairsAnim>().Stairs();
                Debug.Log("アニメーション再生");

            }
        }
    }

   




    //アニメーション開始
    //アニメーション終了

}
