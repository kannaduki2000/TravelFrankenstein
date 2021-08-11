using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyorandplayer : MonoBehaviour
{
    public float speed;              //movement speed
    float movex;                     //float for move 
    Rigidbody2D rb;
    public float jumpforce;          //float for jump
    public Transform groundcheck;    // player groundcheck
    public LayerMask whatisground;   //layer for ground
    public float radius;             //layer radius
    bool isGrounded;                 // bool for ground 
    bool istouchingpuller;           // bool for puller 
    public float pullforce;          // force for puller 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //////////////////////////////////////////////////////////
        //for player movement
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
        //////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////////
        //for Player Jump
        isGrounded = Physics2D.OverlapCircle(groundcheck.position,radius,whatisground);
        if(isGrounded && Input.GetKeyDown("w"))
        {
            rb.velocity = Vector2.up*jumpforce;
        }
        //////////////////////////////////////////////////////////////////////

        /////////////////////////////////////////////////////////////////////
        // player left and right rotation
        if(movex > 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        }
        if(movex < 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
        }
        ////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////
        //puller is stop or continue 
        if(Input.GetKeyDown("space") && isGrounded)
        {
            istouchingpuller = true;
        }
        if(Input.GetKeyDown("e") && isGrounded)
        {
            istouchingpuller = false;
        }
        ////////////////////////////////////////////////////////////
    }
    //////////////////////////////////////////////////////////////
    //gameobjcet and to pull from pullingplatform
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
    ///////////////////////////////////////////////////////////////////////
}
