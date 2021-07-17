using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationEnemy : MonoBehaviour
{
    public Rigidbody2D rigid2D;
    private float speed = 0;
    private bool kasokuuuuuu = false;
    private bool kasokudekiru = false;
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && kasokudekiru == false)
        {
            this.speed = 0.001f;
            kasokuuuuuu = true;
            kasokudekiru = true;
        }

        if(kasokuuuuuu == true)
        {
            transform.Rotate(0, 0, this.speed);
        }

        if (speed <= 0.1)
        {
            this.speed += 0.002f;
        }
    }

    //‰ñ“]” = ‘å‚«‚³ / 2
}