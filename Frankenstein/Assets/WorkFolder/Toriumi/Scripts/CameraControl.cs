using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraControl : MonoBehaviour
{
    private float speed = 5f;   // カメラが追従する速度
    private bool fall = false;  // Pleyerが画面外にいった判定

    [SerializeField] GameObject player; // Player
    [SerializeField] Camera c_target;   // Camera

    private Rect rect = new Rect(0, 0, 1, 1);   // ViewportがRect内にいるか

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {

        Vector2 playerPos = player.transform.position;

        var viewport = c_target.WorldToViewportPoint(playerPos);
        
        if (fall)
        {
            StartCoroutine("Timer");
        }

        if (!rect.Contains(viewport))   //画面外にPleyerがいったら
        {
            fall = true;
        }
    }


    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1.0f);

        Transform camera = this.transform;

        Vector3 pos = camera.position;

        pos.y = Mathf.MoveTowards(pos.y, -5.5f, Time.deltaTime * speed);
        transform.position = new Vector3(pos.x, pos.y, pos.z);

        yield break;
    }
}

/*
    スクリプト内容
・　Pleyerが画面外に行ったらカメラのy座標が下がるようになる。
・　コルーチン内で処理を書いている。
　
    ・1秒の遅延
    ・カメラを下げる(下げる範囲は自分で設定)。
 
 */


