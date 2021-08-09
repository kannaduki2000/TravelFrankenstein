using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPlessThrow : MonoBehaviour
{
    public float ThrowX = 25;                   //長押しの投げる力
    public float ThrowY = 65;                   //長押しの投げる力

    public float Throwx = 30;                   //短押しの投げる力
    public float Throwy = 50;                   //短押しの投げる力

    public bool left = false;                   //左右のフラグ

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("right"))//入力名の変更：半田
        {
            left = false;
        }
        if (Input.GetKey("left"))//入力名の変更：半田
        {
            left = true;
        }
    }

    //低く投げる
    public void Low()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("低め");
        if (left)
        {
            rb.AddForce(new Vector2(-Throwx, Throwy), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(new Vector2(Throwx, Throwy), ForceMode2D.Impulse);
        }
    }

    //高く投げる
    public void Hight()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("高め");
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


