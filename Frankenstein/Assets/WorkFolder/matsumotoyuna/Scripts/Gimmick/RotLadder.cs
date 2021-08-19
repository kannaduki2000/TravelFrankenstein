using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�͂����̒[�ɋ�̃I�u�W�F�N�g���
//�������e�A�͂������q�ɂ���
//��̃I�u�W�F�N�g�ɂ����t���ĂˁI

public class RotLadder : MonoBehaviour
{
    //��]�X�s�[�h
    float speed = 40f;
    //�X�C�b�`�������ꂽ��true
    public bool pushtorotation = false;

    void Update()
    {
        //�X�C�b�`�����ꂽ��
        if(pushtorotation)
        {
            float step = speed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards
           (transform.rotation, Quaternion.Euler(0, 0, -20.237f), step);
        }

        //�X�C�b�`��������ĂȂ����
        //(�������ʒu�ɖ߂�����)
        else if(!pushtorotation)
        {
            float step = speed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards
           (transform.rotation, Quaternion.Euler(0, 0, 20.237f), step);
        }
    }
}
