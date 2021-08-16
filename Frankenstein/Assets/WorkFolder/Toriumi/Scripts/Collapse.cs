using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collapse : MonoBehaviour
{
    private bool Fall = false;
    private float speed = 10f;

    GameObject Ground;

    void Start()
    {
        Ground = GameObject.Find("Ground");
    }

    void Update()
    {
        if (Fall)
        {
            Transform ground = Ground.transform;                            //Ground�̍��W���擾
            Vector2 pos = ground.position;
            pos.y = Mathf.MoveTowards(pos.y, -100, Time.deltaTime * speed); //pos.y����-100�܂�Time.deltaTime * speed�̃X�s�[�h�ňړ�
            ground.position = pos;
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Fall = true;
        }
    }
    
    /* �X�N���v�g���e */
//�E�n�ʂ���������X�N���v�g
//�E�΂̊O����Trigger������A�G�ꂽ��n�ʂ�������B
//�Espped�ŗ����鑬�������߉\
//�E-100�̂Ƃ���ŗ����鍂�����ύX�\
//�E�A�j���[�V�������Ȃ��̂ł��������n�ʗ����̂݁B
//�E�J�����ړ��̓X�N���v�g��������Ȃ��ĐG���ĂȂ��ł��B
}
