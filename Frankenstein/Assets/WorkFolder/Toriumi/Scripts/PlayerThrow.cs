using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    private int presskeyFrames = 0;             //�������t���[����
    private int PressLong = 300;                //������
    private int PressShort = 100;               //�y����
    private bool Throw = false;                 //�����̃t���O
    Rigidbody2D rb;
    KeyPlessThrow item;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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

    }

    //�A�C�e�����痣�ꂽ��
    private void OnTriggerExit2D(Collider2D collision)
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
    private void OnTriggerStay2D(Collider2D collision)
    {
  

        if (collision.gameObject.tag == "Item")
        {
            Debug.Log("stay");

            //item = collision.gameObject.GetComponent<Item>();
            //W�������Ă�����
            if (Input.GetKey(KeyCode.W))
            {
                Throw = true;
                //�A�C�e���N���X�̎擾
                item = collision.gameObject.GetComponent<KeyPlessThrow>();

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


