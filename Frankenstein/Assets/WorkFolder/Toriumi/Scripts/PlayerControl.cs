using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Vector2 Speed = new Vector2(1, 1);   //���x
    private int presskeyFrames = 0;             //�������t���[����
    private int PressLong = 300;                //�����������臒l
    private int PressShort = 100;               //�y�������������臒l
    private bool Throw = false;                 //�����̃t���O
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
        //�ړ�
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

        //�������]
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

        if (Throw)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                //�X�y�[�X�̔���
                //memo  �w? true:false�x
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
                    Debug.Log("����");
                }

                //�����X�y�[�X�������ꂽ��
                else if (PressShort <= presskeyFrames)

                //��߂ɓ�����
                {
                    item.Low();
                    Debug.Log("�Z��");
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
            Throw = false;
            presskeyFrames = 0;
            this.gameObject.transform.DetachChildren();
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
                Throw = true;
                //�A�C�e���N���X�̎擾
                item = collision.gameObject.GetComponent<Item>();

                //�A�C�e����Y�����オ��
                // �����ł��̃I�u�W�F�N�g���v���C���[�̎q���ɂ���
                item.gameObject.transform.parent = this.transform;
            }
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            this.gameObject.transform.DetachChildren();
        }
    }


}
