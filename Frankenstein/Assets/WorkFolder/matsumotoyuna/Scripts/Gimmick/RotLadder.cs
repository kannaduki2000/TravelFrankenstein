using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//はしごの端に空のオブジェクト作る
//そいつが親、はしごを子にする
//空のオブジェクトにこれを付けてね！

public class RotLadder : MonoBehaviour
{
    //回転スピード
    float speed = 40f;
    //スイッチを押されたらtrue
    public bool pushtorotation = false;

    void Update()
    {
        //スイッチ押されたら
        if(pushtorotation)
        {
            float step = speed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards
           (transform.rotation, Quaternion.Euler(0, 0, -20.237f), step);
        }

        //スイッチを押されてなければ
        //(↑初期位置に戻す処理)
        else if(!pushtorotation)
        {
            float step = speed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards
           (transform.rotation, Quaternion.Euler(0, 0, 20.237f), step);
        }
    }
}
