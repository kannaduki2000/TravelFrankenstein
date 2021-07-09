using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pogegegeeeeee : MonoBehaviour
{
    private float speed = 1f;
    Rigidbody2D rigid2D;

    void Start()
    {
        
    }

    void Update()
    {
        if (this.transform.localScale.x == 1)
        {
            Invoke("migiugoki",2.0f);
        }

        else if (this.transform.localScale.x == -1)
        {
            hidariugoki();
        }
    }

    void migiugoki()
    {
        if(this.transform.localScale.x == 1)
        {
            Transform aiueo = this.transform;
            Vector2 ppposition = aiueo.position;
            ppposition.x = Mathf.MoveTowards(ppposition.x, -5.5f, Time.deltaTime * speed);
            aiueo.position = ppposition;

            if(this.transform.position.x == -5.5f)
            {
                Invoke("hidari", 1.0f);
                Invoke("hidariugoki", 2.0f);
            }
        }
    }

    void hidariugoki()
    {
        if(this.transform.localScale.x == -1)
        {
            Transform aiueo = this.transform;
            Vector2 ppposition = aiueo.position;
            ppposition.x = Mathf.MoveTowards(ppposition.x, -8.5f, Time.deltaTime * speed);
            aiueo.position = ppposition;

            if(this.transform.position.x == -8.5f)
            {
                Invoke("migi", 1.0f);
                Invoke("migiugoki", 2.0f);
            }
        }
    }

    void migi()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    void hidari()
    {
        transform.localScale = new Vector3(-1, 1, 1);
    }
}
