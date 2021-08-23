using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropdown : MonoBehaviour
{
    public Transform downpos;
    public float speed;
    bool iselevatordown;
        void Start()
    {
        
    }
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
