using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyorandplayer : MonoBehaviour
{
    public float speed;
    float movex;
    Rigidbody2D rb;
    public float jumpforce;
    public Transform groundcheck;
    public LayerMask whatisground;
    public float radius;
    bool isGrounded;
    bool istouchingpuller;
    public float pullforce;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movex = Input.GetAxisRaw("Horizontal");

        if(istouchingpuller == true)
        {
            rb.velocity = new Vector2(pullforce,rb.velocity.y);
            if(movex > 0)
            {
                rb.velocity = new Vector2(movex * (speed - 3),rb.velocity.y);
            }
            if(movex < 0)
            {
                rb.velocity = new Vector2(-(pullforce + speed),rb.velocity.y);
            }
        }
        else
        {
            rb.velocity = new Vector2(movex * speed,rb.velocity.y);
        }

        isGrounded = Physics2D.OverlapCircle(groundcheck.position,radius,whatisground);
        if(isGrounded && Input.GetKeyDown("w"))
        {
            rb.velocity = Vector2.up*jumpforce;
        }
        if(movex > 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        }
        if(movex < 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "pull")
        {
            istouchingpuller = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "pull")
        {
            istouchingpuller = false;
        }
    }
}
