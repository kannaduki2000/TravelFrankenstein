using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //低く投げる
    public void Low()
    {

        // 親のxスケールを取得
        // xスケールが、0よりも大きい。すなわち + ならば右方向へ
        // xスケールが、0よりも小さい。すなわち - ならば左方向へ
        // 但し、親のxスケールが反転する場合のみ使える。
        // 親のスケールが +- 変わらないと反転しないです。

        Vector2 vec = GameObject.Find("Player").transform.localScale;
        float x = vec.x;
        if (x > 0)
        {
            //x軸方向
            float I_speed = 70f;
            float I_degree = 60f; // 60°= 右向き

            //y軸方向
            float I_Speed = 60f;
            float I_Degree = 45f; // 45°= 右向き

            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

            Vector2 vel = Vector2.zero;


            //memo 『Mathf』三角関数の定数とメソッドを提供
            //memo2『PI』   πを指定
            //memo3 I_degreeπ/180 = 1/x = x°
            //memo4 x軸方向の計算 v0cosθ * t
            //memo5 y軸方向の計算 v0sinθ - gt

            vel.x = I_speed * Mathf.Cos(I_degree * Mathf.PI / 180f);
            vel.y = I_Speed * Mathf.Sin(I_Degree * Mathf.PI / 180f);
            rb.velocity = vel;
        }
        if (x < 0)
        {
            //x軸方向
            float I_speed = 70f;
            float I_degree = 120f; // 120°= 左向き

            //y軸方向
            float I_Speed = 60f;
            float I_Degree = 135f; // 135°= 左向き

            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

            Vector2 vel = Vector2.zero;


            //memo 『Mathf』三角関数の定数とメソッドを提供
            //memo2『PI』   πを指定
            //memo3 I_degreeπ/180 = 1/x = x°
            //memo4 x軸方向の計算 v0cosθ * t
            //memo5 y軸方向の計算 v0sinθ - gt

            vel.x = I_speed * Mathf.Cos(I_degree * Mathf.PI / 180f);
            vel.y = I_Speed * Mathf.Sin(I_Degree * Mathf.PI / 180f);
            rb.velocity = vel;
        }

    }

    //高く投げる
    public void Hight()
    {

        Vector2 vec = GameObject.Find("Player").transform.localScale;
        float x = vec.x;
        if (x > 0)
        {
            float I_speed = 55f;
            float I_degree = 60f; // 60°
            float I_Speed = 70f;
            float I_Degree = 60f; // 60°

            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

            Vector2 vel = Vector2.zero;


            vel.x = I_speed * Mathf.Cos(I_degree * Mathf.PI / 180f);
            vel.y = I_Speed * Mathf.Sin(I_Degree * Mathf.PI / 180f);
            rb.velocity = vel;
        }
        else if (x < 0)
        {
            float I_speed = 55f;
            float I_degree = 120f; // 60°
            float I_Speed = 70f;
            float I_Degree = 120f; // 60°

            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

            Vector2 vel = Vector2.zero;


            vel.x = I_speed * Mathf.Cos(I_degree * Mathf.PI / 180f);
            vel.y = I_Speed * Mathf.Sin(I_Degree * Mathf.PI / 180f);
            rb.velocity = vel;
        }
    }
}
