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

    //�Ⴍ������
    public void Low()
    {
        //x������
        float I_speed  = 70f;
        float I_degree = 60f; // 60��

        //y������
        float I_Speed  = 60f;
        float I_Degree = 45f; // 45��

        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

        Vector3 vel = Vector3.zero;
       

        //memo �wMathf�x�O�p�֐��̒萔�ƃ��\�b�h���
        //memo2�wPI�x   �΂��w��
        //memo3 I_degree��/180 = 1/x = x��
        //memo4 x�������̌v�Z v0cos�� * t
        //memo5 y�������̌v�Z v0sin�� - gt

        vel.x = I_speed * Mathf.Cos(I_degree * Mathf.PI / 180f);
        vel.y = I_Speed * Mathf.Sin(I_Degree * Mathf.PI / 180f);
        rb.velocity = vel;
    }

    //����������
    public void Hight()
    {
        float I_speed  = 55f;
        float I_degree = 60f;�@// 60��
        float I_Speed  = 70f;
        float I_Degree = 60f;�@// 60��

        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

        Vector3 vel = Vector3.zero;
        

        vel.x = I_speed * Mathf.Cos(I_degree * Mathf.PI / 180f);
        vel.y = I_Speed * Mathf.Sin(I_Degree * Mathf.PI / 180f);
        rb.velocity = vel;
    }
}
