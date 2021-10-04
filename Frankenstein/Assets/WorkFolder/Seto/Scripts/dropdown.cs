using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropdown : MonoBehaviour
{
    public Transform downpos;
    public float speed;
    public bool iselevatordown;

    public bool foll = false;

    public AudioClip FollCrash;
    public bool onTrigger;

    private System.Action callBack = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(foll == true)
        {
            if(transform.position.y <= downpos.position.y)
            {
                iselevatordown = false;

                if (onTrigger)
                {
                    SEConveyer.instance.PlaySE(FollCrash);
                }
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

    public IEnumerator DropDown(System.Action _callBack = null)
    {
        callBack = _callBack;
        while (transform.position.y - 0.1f >= downpos.position.y)
        {
            transform.position = Vector2.MoveTowards(transform.position, downpos.position, speed * Time.deltaTime);
            yield return null;
        }

        SEConveyer.instance.PlaySE(FollCrash);

        if (callBack != null) { callBack(); }
        yield break;
    }
    
}
