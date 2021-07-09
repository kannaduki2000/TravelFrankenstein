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
        //�d���ύX����
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
        //���ꂫ�ɓ���������
        if (collision.gameObject.tag == "Gareki")
        {
            //garekiCrash = true;
            //Destroy(gareki);
            gareki.SetActive(false);
            //���������u�Ԕj��v
            Invoke("CarCrash", 2.0f);
        }
    }

    private void Crash()
    {
         //R���������牟����A���I�̔j��
         //rigid2D.mass = 5;
         if (garekiCrash == true)
         {
            Destroy(gareki);


            //���������u�Ԕj��v
            Invoke("CarCrash", 2.0f);
         }
    }

    private void CarCrash()
    {
        //�Ԕj��
        //Destroy(this.gameObject);
        gameObject.SetActive(false);
    }
}


/*
 �G�l�~�[���Ԃ�����
 ���I�ɓ�����
 ���U����
 */