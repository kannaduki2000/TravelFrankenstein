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

        // �e��x�X�P�[�����擾
        // x�X�P�[�����A0�����傫���B���Ȃ킿 + �Ȃ�ΉE������
        // x�X�P�[�����A0�����������B���Ȃ킿 - �Ȃ�΍�������
        // �A���A�e��x�X�P�[�������]����ꍇ�̂ݎg����B
        // �e�̃X�P�[���� +- �ς��Ȃ��Ɣ��]���Ȃ��ł��B

        Vector2 vec = GameObject.Find("Player").transform.localScale;
        float x = vec.x;
        if (x > 0)
        {
            //x������
            float I_speed = 70f;
            float I_degree = 60f; // 60��= �E����

            //y������
            float I_Speed = 60f;
            float I_Degree = 45f; // 45��= �E����

            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

            Vector2 vel = Vector2.zero;


            //memo �wMathf�x�O�p�֐��̒萔�ƃ��\�b�h���
            //memo2�wPI�x   �΂��w��
            //memo3 I_degree��/180 = 1/x = x��
            //memo4 x�������̌v�Z v0cos�� * t
            //memo5 y�������̌v�Z v0sin�� - gt

            vel.x = I_speed * Mathf.Cos(I_degree * Mathf.PI / 180f);
            vel.y = I_Speed * Mathf.Sin(I_Degree * Mathf.PI / 180f);
            rb.velocity = vel;
        }
        if (x < 0)
        {
            //x������
            float I_speed = 70f;
            float I_degree = 120f; // 120��= ������

            //y������
            float I_Speed = 60f;
            float I_Degree = 135f; // 135��= ������

            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

            Vector2 vel = Vector2.zero;


            //memo �wMathf�x�O�p�֐��̒萔�ƃ��\�b�h���
            //memo2�wPI�x   �΂��w��
            //memo3 I_degree��/180 = 1/x = x��
            //memo4 x�������̌v�Z v0cos�� * t
            //memo5 y�������̌v�Z v0sin�� - gt

            vel.x = I_speed * Mathf.Cos(I_degree * Mathf.PI / 180f);
            vel.y = I_Speed * Mathf.Sin(I_Degree * Mathf.PI / 180f);
            rb.velocity = vel;
        }

    }

    //����������
    public void Hight()
    {

        Vector2 vec = GameObject.Find("Player").transform.localScale;
        float x = vec.x;
        if (x > 0)
        {
            float I_speed = 55f;
            float I_degree = 60f; // 60��
            float I_Speed = 70f;
            float I_Degree = 60f; // 60��

            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

            Vector2 vel = Vector2.zero;


            vel.x = I_speed * Mathf.Cos(I_degree * Mathf.PI / 180f);
            vel.y = I_Speed * Mathf.Sin(I_Degree * Mathf.PI / 180f);
            rb.velocity = vel;
        }
        else if (x < 0)
        {
            float I_speed = 55f;
            float I_degree = 120f; // 60��
            float I_Speed = 70f;
            float I_Degree = 120f; // 60��

            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

            Vector2 vel = Vector2.zero;


            vel.x = I_speed * Mathf.Cos(I_degree * Mathf.PI / 180f);
            vel.y = I_Speed * Mathf.Sin(I_Degree * Mathf.PI / 180f);
            rb.velocity = vel;
        }
    }
}
