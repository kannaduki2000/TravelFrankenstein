using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Low()
    {
        ////�͂����������
        //Vector3 forceDirection = new Vector3(3, 3, 0);

        ////��̌����ɉ����͂̑傫��
        //float forceMagnitude = 10.0f;

        ////�����Ƒ傫������item�ɉ����͂��v�Z
        //Vector3 force = forceMagnitude * forceDirection;

        ////item�I�u�W�F�N�g��rigidbody�R���|�[�l���g�ւ̎Q��
        //Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

        ////�͂������郁�\�b�h
        //rb.AddForce(force, ForceMode2D.Impulse);

        float I_speed = 60f;
        float I_degree = 60f;

        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

        Vector3 vel = Vector3.zero;
        vel.y = I_speed;

        vel.x = I_speed * Mathf.Cos(I_degree * Mathf.PI / 180f);
        vel.y = I_speed * Mathf.Sin(I_degree * Mathf.PI / 180f);
        rb.velocity = vel;
        this.transform.parent = null;

    }

    public void Hight()
    {
        ////�͂����������
        //Vector3 forceDirection = new Vector3(6, 6, 0);

        ////��̌����ɉ����͂̑傫��
        //float forceMagnitude = 10.0f;

        ////�����Ƒ傫������item�ɉ����͂��v�Z
        //Vector3 force = forceMagnitude * forceDirection;

        ////item�I�u�W�F�N�g��rigidbody�R���|�[�l���g�ւ̎Q��
        //Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

        ////�͂������郁�\�b�h
        //rb.AddForce(force, ForceMode2D.Impulse);

        float I_speed = 80f;
        float I_degree = 80f;

        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

        Vector3 vel = Vector3.zero;
        vel.y = I_speed;

        vel.x = I_speed * Mathf.Cos(I_degree * Mathf.PI / 192.5f);
        vel.y = I_speed * Mathf.Sin(I_degree * Mathf.PI / 192.5f);
        rb.velocity = vel;
        this.transform.parent = null;
    }

    //public void Move()
    //{
    //    Vector3 pos = transform.position;

    //    //y��
    //    pos.y = 50;

    //    //x��
    //    PlayerControl player = gameObject.GetComponent<PlayerControl>();

    //    // z
    //    pos.z = 0;
        
        
    //    if (Input.GetKey(KeyCode.A))
    //    {
    //        pos.x -= 2;
    //    }
    //    //�E���L�[�������Ă���
    //    else if (Input.GetKey(KeyCode.D))
    //    {
    //        pos.x += 2;
    //    }
    //    float x = Input.GetAxisRaw("Horizontal");

    //    GameObject obj = GameObject.Find("Player");

    //    Vector3 scale = obj.transform.localScale;

    //    if (x > 0)
    //    {
    //        scale.x = 1;

    //    }
    //    else if (x < 0)
    //    {
    //        scale.x = -1;
    //    }

    //    obj.transform.localScale = scale;
    //    transform.position = pos;
    //    //Debug.Log("�Z");

    //}
}