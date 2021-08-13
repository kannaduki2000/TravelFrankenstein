using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Charger : MonoBehaviour
{
    GameObject Ra1;                //�C�x���g�����͈� 
    GameObject Ra2;                //��


    Vector2 startPos;              // �����ʒu

    private bool Remove = false;   // �e�q�����A�����ʒu�ɖ߂�A�P�[�u���̃X�P�[����0�ɂ���t���O

    private void Start()
    {
        startPos = transform.position;      // �����ʒu���i�[

        Remove = true;                      

        this.Ra1 = GameObject.Find("Range1");
        this.Ra2 = GameObject.Find("Range2");

        Ra1.SetActive(false);   //  �C�x���g�͈͔�\��
        Ra2.SetActive(false);   //�@�ǔ�\��
    }

    private void Update()
    {
        if (Remove)
        {
            if (Input.GetKeyUp(KeyCode.W))
            {
                Debug.Log("ok");
                this.gameObject.transform.parent = null;                                    //�e�q�֌W�̉���
                transform.position = startPos;                                              //�����ʒu

                Vector3 vec = GameObject.Find("Cabels").transform.localScale;
                GameObject.Find("Cabels").transform.localScale = new Vector3(0, 0, 0);      //Cables�̃X�P�[����0(��\���ɂ���)

                Ra1.SetActive(false);   //  �C�x���g�͈͔�\��
                Ra2.SetActive(false);   //�@�ǔ�\��
            }
        }
    }

    //�@�e�q�֌W�ɂ���
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Dog")
        {
         
            // W�L�[�������Ă���Ɛe�q�֌W�ɂȂ�
            if (Input.GetKey(KeyCode.W))
            {
                this.gameObject.transform.parent = GameObject.Find("Dog").transform;    //Dog ��e�ɂ���

                Vector3 vec = GameObject.Find("Cabels").transform.localScale;
                GameObject.Find("Cabels").transform.localScale = new Vector3(1, 1, 1);  //Cables�̃X�P�[����1(�\������)

                Ra1.SetActive(true);    //�@�C�x���g�͈͕\��
                Ra2.SetActive(true);    //�@�Ǖ\��

            }
        }
    }

    // �[�d�P�[�u���̋�������
    // Trigger�ŃC�x���g�͈͂�����
    // Collision�ŕǂ𐶐��B

    // Range1: �ݒ肳�ꂽ�ʒu
    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.gameObject.tag == "Range1")
            {
                Remove = false;

                Debug.Log("ok?");

                this.gameObject.transform.parent = null;

                Debug.Log("�e�q��������Ȃ��c");
            }
    }


    /*
     �@���X�N���v�g���e��

    �E Hinge Joint ���g���Ďl�p���Ȃ��ăP�[�u�����쐬�B
    �E Dog�̃X�N���v�g���e���킩��Ȃ����߁A���̕������Ă��܂��B
    �E tag��3��ޒǉ����܂����B
        Dog�Achanger�ARange1
    
       *��������*
       �ERange1 �ŃC�x���g���������Ă��܂��B
       �ERange2 �ŕǂ�����Ă��܂��B
       �E�ǂ�����n�߂͔�\���ł����ADog�ƃP�[�u�����e�q�ɂȂ�����
         �\�������悤�ɂȂ��Ă��܂��B
     
       *�P�[�u����L�΂�*
       �EHinge Joint���g���ĂȂ��Ă��܂��B
       �ESetActive���g���ƃo�O����������̂ŁA
         �X�P�[����0(��\��)��1(�\��)�ŕ\���A��\����ݒ肵�Ă��܂��B
       �ERigidbody�̊֌W�ƃP�[�u���̈ʒu�ŃP�[�u�����r�Ԃ�\��������܂��B
         �܂��ARigidbody�̊֌W�ŃP�[�u�����������蔲���鋰�ꂪ�����ɂ���܂��B
         �P�[�u���𑝂₹�΂��蔲��������܂����A
         ����ɃP�[�u�����Ƃ�ł��Ȃ��d�Ȃ�̂ŖŒ��ꒃ�r�Ԃ�܂��B

    �@*�����ʒu�ɖ߂�*
    �@ �E�͈͊O��r���ŗ������ꍇ�A�����ʒu�ɖ߂�܂��B
       �E�����ʒu��transform.position�Őݒ肵�Ă��܂��B
       
        �ȏ�ł��A��낵�����肢���܂��B
     */

}
