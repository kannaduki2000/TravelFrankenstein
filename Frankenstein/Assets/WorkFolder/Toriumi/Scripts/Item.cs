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

    //α­°ι
    public void Low()
    {
        //x²ϋό
        float I_speed  = 70f;
        float I_degree = 60f; // 60

        //y²ϋό
        float I_Speed  = 60f;
        float I_Degree = 45f; // 45

        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

        Vector3 vel = Vector3.zero;
       

        //memo wMathfxOpΦΜθΖ\bhπρ
        //memo2wPIx   Ξπwθ
        //memo3 I_degreeΞ/180 = 1/x = x
        //memo4 x²ϋόΜvZ v0cosΖ * t
        //memo5 y²ϋόΜvZ v0sinΖ - gt

        vel.x = I_speed * Mathf.Cos(I_degree * Mathf.PI / 180f);
        vel.y = I_Speed * Mathf.Sin(I_Degree * Mathf.PI / 180f);
        rb.velocity = vel;
    }

    //­°ι
    public void Hight()
    {
        float I_speed  = 55f;
        float I_degree = 60f;@// 60
        float I_Speed  = 70f;
        float I_Degree = 60f;@// 60

        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

        Vector3 vel = Vector3.zero;
        

        vel.x = I_speed * Mathf.Cos(I_degree * Mathf.PI / 180f);
        vel.y = I_Speed * Mathf.Sin(I_Degree * Mathf.PI / 180f);
        rb.velocity = vel;
    }
}
