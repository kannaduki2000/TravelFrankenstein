﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontrol : MonoBehaviour
{
    float dirX,dirY;  //player move
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ///////////////////////////////////////////
        //player movement
        dirX = Input.GetAxis ("Horizontal");
        dirY = Input.GetAxis ("Vertical");
        
        rb.velocity = new Vector2 ( dirX * 5, dirY * 10);
        ///////////////////////////////////////////////////////////////////////////
        //if player is stop animation is not working code
        if(dirX > 0)
        {
            gamecontrol.disabled = false;
        }
        else{
            gamecontrol.disabled = true;
        }
        ///////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////
    }

    /////////////////////////////////////////////
    //object appear and disappear
    void OnTriggerEnter2D (Collider2D col)
    {
        switch (col.gameObject.name)
        {
            case "LP": //LP = Left Position
                gamecontrol.disabled = false;
            break;
            case "RP": // RP = Right Position
                gamecontrol.disabled = true;
            break;
        }
    }
    //////////////////////////////////////////////
}
