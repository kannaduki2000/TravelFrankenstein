using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPush : MonoBehaviour
{
    Rigidbody2D rigid2D;
    public bool crash = true;
    public bool garekiCrash = false;
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (crash == true)
        {
            rigid2D.mass = 5;
            Crash();
        }

        else if (crash == false)
        {
            rigid2D.mass = 500;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Gareki")
        {
            garekiCrash = true;
        }
    }

    private void Crash()
    {
         //Rを押したら押せる、瓦礫の破壊
         //rigid2D.mass = 5;
         if (garekiCrash == true)
         {
             GameObject gareki = GameObject.Find("Garekiiiiiiiiiiiiiiiiii");
             Destroy(gareki);
             // GetComponent<ParticleSystem>().Play();みたいなものを書く
             //時差発動「車破壊」
             Invoke("CarCrash", 2.0f);
         }
    }

    private void CarCrash()
    {
        //車破壊
        Destroy(this.gameObject);
    }
}


/*
 エネミーが車を押す
 瓦礫に当たる
 爆散する
 */