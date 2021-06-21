using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPlessThrow : MonoBehaviour
{
    public float ThrowX = 25;                   //�������̓������
    public float ThrowY = 65;                   //�������̓������

    public float Throwx = 30;                    //�Z�����̓������
    public float Throwy = 50;                    //�Z�����̓������

    private bool left = false;                    //���E�̃t���O

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            left = false;
        }
        if (Input.GetKey(KeyCode.A))
        {
            left = true;
        }
    }

    //�Ⴍ������
    public void Low()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("�Z��");
        if (left)
        {
            rb.AddForce(new Vector2(-Throwx, Throwy), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(new Vector2(Throwx, Throwy), ForceMode2D.Impulse);
        }
    }

    //����������
    public void Hight()
    {
        rb = GetComponent<Rigidbody2D>();
        if (left)
        {
            rb.AddForce(new Vector2(-ThrowX, ThrowY), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(new Vector2(ThrowX, ThrowY), ForceMode2D.Impulse);
        }
    }
}


