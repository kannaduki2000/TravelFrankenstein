using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Vector2 Speed = new Vector2(1, 1);   //���x
    private int presskeyFrames = 0;             //�������t���[����
    private int PressLong = 300;                 //�����������臒l
    private int PressShort = 100;                //�y�������������臒l
    Rigidbody2D rb;
    Item item;
    



    void Start()
    {
        //Animetor�R���|�l�[�V�������擾����
        //anim = GetComponent<Animator>();

    }

    // �A�b�v�f�[�g�̓t���[�����Ƃ�1��Ăяo�����
    void Update()
    {
        //var pos = GetComponent<RectTransform>().localPosition;
        ////�����L�[�������Ă���
        //if (Input.GetKey(KeyCode.A))
        //{
        //    pos.x -= 1;
        //}
        ////�E���L�[�������Ă���
        //else if (Input.GetKey(KeyCode.D))
        //{
        //    pos.x += 1;
        //}
        //GetComponent<RectTransform>().localPosition = pos;
        Vector2 Position = transform.position;
        if(Input.GetKey(KeyCode.A))
        {
            Position.x -= Speed.x;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            Position.x += Speed.x;
        }
        transform.position=Position;

        float x= Input.GetAxisRaw("Horizontal");
        if(x !=0)
        {
            Vector2 Iscale = gameObject.transform.localScale;
            if((Iscale.x > 0 && x < 0) || (Iscale.x < 0 && x > 0))
            {
                Iscale.x *= -1;
                gameObject.transform.localScale = Iscale;
            }
        }

        //GameObject obj = GameObject.Find("Player");

        //Vector3 scale = obj.transform.localScale;

        //if(x > 0)
        //{
        //    scale.x = 1;
        //}
        //else if(x < 0)
        //{
        //    scale.x = -1;
        //}

        //obj.transform.localScale = scale;



        if (aa)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                //�X�y�[�X�̔���
                presskeyFrames += (Input.GetKey(KeyCode.Space)) ? 1 : 0;
                Debug.Log(presskeyFrames);
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                //�����X�y�[�X�����������ꂽ��
                if (PressLong <= presskeyFrames)

                //���߂ɓ�����
                {
                    item.Hight();
                    //this.gameObject.transform.DetachChildren();
                    Debug.Log("����");
                    this.gameObject.transform.DetachChildren();
                }

                //�����X�y�[�X�������ꂽ��
                else if (PressShort <= presskeyFrames)

                //��߂ɓ�����
                {
                    item.Low();
                    //this.gameObject.transform.DetachChildren();
                    Debug.Log("�Z��");
                    this.gameObject.transform.DetachChildren();
                }
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                this.gameObject.transform.DetachChildren();
            }
        }
    }

    private void FixedUpdate()
    {
        
    }
    private bool aa = false;
    //�A�C�e���ɓ���������
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            //�A�j���[�V�������Đ������
            Debug.Log("�A�j���[�V�����Đ�");


            
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            aa = false;
            Debug.Log("exit");
        }
    }


    //�A�C�e���ɓ����葱������
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag =="Item")
        {
            Debug.Log("stay");

            //item = collision.gameObject.GetComponent<Item>();
            //W�������Ă�����
            if (Input.GetKey(KeyCode.W))
            {
                aa = true;
                //�A�C�e���N���X�̎擾
                item = collision.gameObject.GetComponent<Item>();

                //�A�C�e����Y�����オ��
                // �����ł��̃I�u�W�F�N�g���v���C���[�̎q���ɂ���

                item.gameObject.transform.parent = this.transform;


                //item.Move();


                //if (Input.GetKey(KeyCode.Space))
                //{
                //    //�X�y�[�X�̔���
                //    presskeyFrames += (Input.GetKey(KeyCode.Space)) ? 1 : 0;
                //    Debug.Log(presskeyFrames);
                //}

                //if (Input.GetKeyUp(KeyCode.Space))
                //{
                //    //�����X�y�[�X�����������ꂽ��
                //    if (PressLong <= presskeyFrames)

                //    //���߂ɓ�����
                //    {
                //        item.Hight();

                //        Debug.Log("����");

                //    }

                //    //�����X�y�[�X�������ꂽ��
                //    else if (PressShort <= presskeyFrames)

                //    //��߂ɓ�����
                //    {
                //        item.Low();

                //        Debug.Log("�Z��");

                //    }
                //}

            }

            

        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            this.gameObject.transform.DetachChildren();
        }
    }
    //�L���������]�������A�C�e�������u���Ă��ڂ�     �Z
    //�A�j���[�V�����̍Đ��A�A�j���[�V�����̓����    �Z
    //�������̔�����Z������̂�����              �Z
    //�q���猳�ɖ߂�Ȃ�


}
