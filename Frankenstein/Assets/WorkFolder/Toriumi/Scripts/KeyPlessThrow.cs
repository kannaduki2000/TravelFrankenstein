using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPlessThrow : MonoBehaviour
{
    public float ThrowX = 25;                   //’·‰Ÿ‚µ‚Ì“Š‚°‚é—Í
    public float ThrowY = 65;                   //’·‰Ÿ‚µ‚Ì“Š‚°‚é—Í

    public float Throwx = 30;                    //’Z‰Ÿ‚µ‚Ì“Š‚°‚é—Í
    public float Throwy = 50;                    //’Z‰Ÿ‚µ‚Ì“Š‚°‚é—Í

    private bool left = false;                    //¶‰E‚Ìƒtƒ‰ƒO

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

    //’á‚­“Š‚°‚é
    public void Low()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("’Z‚ß");
        if (left)
        {
            rb.AddForce(new Vector2(-Throwx, Throwy), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(new Vector2(Throwx, Throwy), ForceMode2D.Impulse);
        }
    }

    //‚‚­“Š‚°‚é
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


