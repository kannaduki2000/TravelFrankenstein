﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropdown : MonoBehaviour
{
    public Transform downpos;
    public float speed;
    bool iselevatordown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("e"))
        {
            if(transform.position.y <= downpos.position.y)
            {
                iselevatordown = false;
            }
            else 
            {
                iselevatordown = true;
            }
        }

        
        if(iselevatordown)
        {
        transform.position = Vector2.MoveTowards(transform.position,downpos.position,speed * Time.deltaTime);
        }
      
    }
    
}