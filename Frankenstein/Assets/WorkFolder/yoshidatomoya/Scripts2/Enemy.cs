using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Vector3 StartPos; // �J�n�ʒu
    public Vector3 EndPos; �@// �I���ʒu
    public float time;       // ���Ԏw��
    private Vector3 deltaPos;
    private float elapsedTime;
    private bool bStartToEnd = true;
    private float UpdateTimer = 0f;
    private float TimeRimit = 2.0f;
    private bool move = false;
    private float UpdateTimer2 = 0f;
    private float TimeRimit2 = 4.0f;
    private bool move2 = false;
    private float UpdateTimer3 = 0f;
    private float TimeRimit3 = 2.0f;

    private Rigidbody2D rd;
    // Start is called before the first frame update
    void Start()
    {
        

        // StartPos�������ʒu�ɐݒ�
        transform.position = StartPos;
        // �P�b������̈ړ��ʂ��Z�o
        deltaPos = (EndPos - StartPos) / time;
        elapsedTime = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if(move == false)
        {
            UpdateTimer += Time.deltaTime;
        }
        if(UpdateTimer >= TimeRimit)
        {
            move = true;
            UpdateTimer = 0;
        }
        // �P�b������̈ړ��ʂ�Time.deltaTime���|�����1�t���[��������̈ړ��ʂƂȂ�
        if (move == true)
        {
            // Time.deltaTime�͑O��Update���Ă΂�Ă���̌o�ߎ���
            transform.position += deltaPos * Time.deltaTime;
            // ���H���H���]�p�o�ߎ���
            elapsedTime += Time.deltaTime;
        }
        // �ړ��J�n���Ă���̌o�ߎ��Ԃ�time�𒴂���Ɖ��H���H���]
        if(elapsedTime > time)
        {
            if(bStartToEnd)
            {
                
                // StartPos��EndPos�������̂Ŕ��]����EndPos��StartPos�ɂ���
                // ���݂̈ʒu��EndPos�Ȃ̂�StartPos - EndPos��EndPos��StartPos�̈ړ��ʂɂȂ�
                deltaPos = (StartPos - EndPos) / time;
                // �덷������Ƃ����\�������邽�ߔO�̂��߂ɃI�u�W�F�N�g�̈ʒu��EndPos�ɐݒ�
                transform.position = EndPos;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                // ��̋t
                deltaPos = (EndPos - StartPos) / time;
                transform.position = StartPos;
                transform.localScale = new Vector3(1, 1, 1);

            }
            // ���H���H�̃t���O���]
            bStartToEnd = !bStartToEnd;
            // ���H���H���]�p�o�ߎ��ԃN���A
            elapsedTime = 0;
            move = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �t�����P�����G�l�~�[�̎����@�m���ɓ���
        if (collision.gameObject.tag == "Player")
        {
            // �G�l�~�[���t�����P���̂����ꏊ�ɓːi����

            // �ːi����������4�b�ԑҋ@
            if ()
            {
                UpdateTimer2 += Time.deltaTime;
                // 4�b�ԑҋ@�����猳�̃e���g���[�ɖ߂�
                if (UpdateTimer2 >= TimeRimit2)
                {
                    UpdateTimer2 = 0;

                }
            }
            // �ːi���s������2�b�ԑҋ@
            else if ()
            {
                UpdateTimer3 += Time.deltaTime;

                // 2�b�ԑҋ@��A�t�����P���������@�m���ɂ���������P��ːi����

                // 2�b�ԑҋ@�����猳�̃e���g���[�ɖ߂�
                if (UpdateTimer3 >= TimeRimit3)
                {
                    UpdateTimer3 = 0;
                }

            }

        }
    }
}

// �G�l�~�[���g�̃e���g���[�Ԃ��s��������Z
// �ҋ@���Ԃ���i2�b�ԁj�Z
// �t�����P�����߂Â��Ă�����ːi����
// �t�����P���̎����@�m�̓G�l�~�[�𒆐S�ɂ���i�G�l�~�[���������王���@�m���ꏏ�ɓ����j�����Z
// �ːi����������S�b�ԑҋ@�@���̂��Ǝ����̃e���g���[�ɖ߂�
// �S�b�ԑҋ@��A�����@�m���Ƀt�����P����������t�����P���ɍĂѓːi
// �ːi�����ꏊ�Ƀt�����P�������Ȃ�������Q�b�ԑҋ@���Ď����̃e���g���[�ɖ߂�
// 
