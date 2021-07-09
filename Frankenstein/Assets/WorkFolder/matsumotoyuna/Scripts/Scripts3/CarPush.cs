using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPush : MonoBehaviour
{
    public Rigidbody2D rigid2D;

    [SerializeField] GameObject gareki;

    public bool crash = true;
    public bool garekiCrash = false;
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //重さ変更だよ
        //if (crash == true)
        //{
        //    rigid2D.mass = 5;
        //    Crash();
        //}

        //else if (crash == false)
        //{
        //    rigid2D.mass = 500;
        //    Crash();
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //がれきに当たったら
        if (collision.gameObject.tag == "Gareki")
        {
            //garekiCrash = true;
            //Destroy(gareki);
            gareki.SetActive(false);
            //時差発動「車破壊」
            Invoke("CarCrash", 2.0f);
        }
    }

    private void Crash()
    {
         //Rを押したら押せる、瓦礫の破壊
         //rigid2D.mass = 5;
         if (garekiCrash == true)
         {
            Destroy(gareki);


            //時差発動「車破壊」
            Invoke("CarCrash", 2.0f);
         }
    }

    private void CarCrash()
    {
        //車破壊
        //Destroy(this.gameObject);
        gameObject.SetActive(false);
    }
}


/*
 エネミーが車を押す
 瓦礫に当たる
 爆散する
 */