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
         //R‚ğ‰Ÿ‚µ‚½‚ç‰Ÿ‚¹‚éAŠ¢âI‚Ì”j‰ó
         //rigid2D.mass = 5;
         if (garekiCrash == true)
         {
             GameObject gareki = GameObject.Find("Garekiiiiiiiiiiiiiiiiii");
             Destroy(gareki);
             // GetComponent<ParticleSystem>().Play();‚İ‚½‚¢‚È‚à‚Ì‚ğ‘‚­
             //·”­“®uÔ”j‰óv
             Invoke("CarCrash", 2.0f);
         }
    }

    private void CarCrash()
    {
        //Ô”j‰ó
        Destroy(this.gameObject);
    }
}


/*
 ƒGƒlƒ~[‚ªÔ‚ğ‰Ÿ‚·
 Š¢âI‚É“–‚½‚é
 ”šU‚·‚é
 */