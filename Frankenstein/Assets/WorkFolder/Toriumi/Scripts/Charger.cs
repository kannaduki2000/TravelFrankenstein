using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Charger : MonoBehaviour
{
    LineRenderer line;
    EdgeCollider2D edge;


    Vector2 startPos;              // �����ʒu

    private int count;             // ���_�̐�
    private bool Chager = false;
    private Vector2[] points = new Vector2[2];

    // �[�d�P�[�u���̋�������

    // Range1: �ݒ肳�ꂽ�ʒu
    // Range2: �����ʒu�łȂ��ꍇ
    //        Trigger�œ�����͈͂�����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Range2")
        {
            Debug.Log("!!");
            this.gameObject.transform.parent = null;
            Chager = true;
        }

        if (collision.gameObject.tag == "Range1")
        {
            Debug.Log("Event");
            this.gameObject.transform.parent = null;
            Chager = false;
        }
    }

    //�@�e�q�֌W�ɂ���
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Dog")
        {
            if (Input.GetKey(KeyCode.W))
            {
                this.gameObject.transform.parent = GameObject.Find("Dog").transform;

            }
        }
        Chager = true;

    }

    // collider���������ǖ���
    void AddCollider()
    {

    }

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        edge = gameObject.AddComponent<EdgeCollider2D>();
        startPos = transform.position;      // �����ʒu���i�[
    }




    // W�L�[�𗣂�����P�[�u���������ʒu�ɖ߂�B
    private void Update()
    {
        if (Chager)
        {
            if (Input.GetKeyUp(KeyCode.W))
            {
                Debug.Log("ok");
                this.gameObject.transform.parent = null;
                transform.position = startPos;              //�����ʒu
                count = 0;                                  //���_�̐����Ȃ������ߐ��̕`�悪������B
            }
        }

    }

    // count �𑝂₷���ƂŒ��_�̐��𑝂₷�B
    // ��������
    private void FixedUpdate()
    {
        count += 1;
        line.positionCount = count;
        
        line.SetPosition(count-1,transform.position);
    }

   

    /*
     �@���X�N���v�g���e��

      �ELineRenderer���g���Đ����쐬�B
    �@�@�P�[�u���̋O�Ղ������B

      �E�P�[�u����W�������ƌ��H�̎q���ɂ��Ă邽�ߓ����͓����B
    �@�@�����Ɖ��������B

    �@�E�A���P�[�u�������̈ʒu(�����ł�1�ȏ�)�ł͂Ȃ��Ƃ��A
    �@�@�����ʒu�ɖ߂�P�[�u���̌��������B


    �@�E����Trigger�ŋ����𐧌��B
    �@�@Range1�ɓ�����W�L�[�𗣂��Ă����̏�Ɏc��B
        Range2�ɓ�����W�L�[�𗣂��Ə����ʒu�ɖ߂�B
    �@
    �@�E�����̂ɓ����蔻��͂Ȃ��B
        �ǂ�����Ă���H�H�H�H�H�H�H�H�H

     */

}
