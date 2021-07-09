using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPush : MonoBehaviour
{
    public Rigidbody2D rigid2D;
    private float speed = 2f;

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

        if (crash == true)
        {
            Crash();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //がれきに当たったら
        if (collision.gameObject.tag == "Gareki")
        {
            //transform.Rotate(0f, 0f, -120.0f * Time.deltaTime);
            //this.transform.rotation = new Quaternion(10f * Time.deltaTime, 0f, 0f, 0f);
            if(transform.rotation.x == -30)
            {
                //this.transform.rotation = new Quaternion(-1f * Time.deltaTime, 0f, 0f, 0f);
            }

            //rigid2D.velocity = Vector2.zero;
            //garekiCrash = true;
            //Destroy(gareki);
            gareki.SetActive(false);
            //時差発動「車破壊」
            Invoke("CarCrash", 2.0f);
        }
    }

    private void Crash()
    {
        rigid2D.bodyType = RigidbodyType2D.Dynamic;
        Transform go = this.transform;
        Vector2 carposition = go.position;

        carposition.x = Mathf.MoveTowards(carposition.x, 5.5f, Time.deltaTime * speed);
        go.position = carposition;
    }

    private void CarCrash()
    {
        //車破壊
        //Destroy(this.gameObject);
        gameObject.SetActive(false);
    }
}