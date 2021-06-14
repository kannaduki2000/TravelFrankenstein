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
        //x軸方向
        float I_speed  = 70f;
        float I_degree = 60f; // 60°

        //y軸方向
        float I_Speed  = 60f;
        float I_Degree = 45f; // 45°

        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

        Vector3 vel = Vector3.zero;
       

        //memo 『Mathf』三角関数の定数とメソッドを提供
        //memo2『PI』   πを指定
        //memo3 I_degreeπ/180 = 1/x = x°
        //memo4 x軸方向の計算 v0cosθ * t
        //memo5 y軸方向の計算 v0sinθ - gt

        vel.x = I_speed * Mathf.Cos(I_degree * Mathf.PI / 180f);
        vel.y = I_Speed * Mathf.Sin(I_Degree * Mathf.PI / 180f);
        rb.velocity = vel;
    }

    //高く投げる
    public void Hight()
    {
        float I_speed  = 55f;
        float I_degree = 60f;　// 60°
        float I_Speed  = 70f;
        float I_Degree = 60f;　// 60°

        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

        Vector3 vel = Vector3.zero;
        

        vel.x = I_speed * Mathf.Cos(I_degree * Mathf.PI / 180f);
        vel.y = I_Speed * Mathf.Sin(I_Degree * Mathf.PI / 180f);
        rb.velocity = vel;
    }
}
