using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    //CharacterController Controller;
    //Transform Target;
    //GameObject Player;

    //[SerializeField]
    //float MoveSpeed = 2.0f;
    //int DetecDist = 8;
    //bool InArea = false;

    // �����ƁA��������
    public float speedX = 1; // �X�s�[�hX
    public float speedY = 0; // �X�s�[�hY
    public float speedZ = 0; // �X�s�[�hZ
    public float second = 1; // ������b��
    private float UpdateTimer = 0f;
    private float TimeRimit = 2.0f;
    private bool move = false;

    float time = 0f;

    private void FixedUpdate() // �����ƁA��������
    {
        if(move == true)
        {
            time += Time.deltaTime;
            float s = Mathf.Sin(time * 3.14f / second); // �ړ��ʂ����߂�
            this.transform.Translate(speedX * s / 50, speedY * s / 50, speedZ * s / 50);
        }

        
    }


    // Start is called before the first frame update
    void Start()
    {
        // �v���C���[�^�O�̎擾
       // Player = GameObject.FindWithTag("Player");
        //Target = Player.transform;

        //Controller = GetComponent<CharacterController>();


    }

    // Update is called once per frame
    void Update()
    {
        
        if (move == false)
        {
            UpdateTimer += Time.deltaTime;
        }
        if (UpdateTimer >= TimeRimit)
        {
            move = true;
            UpdateTimer = 0;
        }
        


        /*
        if(InArea)
        {
            Debug.Log("abc");
            // �v���C���[�̂ق�����������
            this.transform.LookAt(Target.transform);

            // �L���[�u�ƃv���C���[�Ԃ̋������v�Z
            Vector3 direction = Target.position - this.transform.position;
            direction = direction.normalized;

            // �v���C���[�����̑��x���쐬
            Vector3 velocity = direction * MoveSpeed;

            // �v���C���[���W�����v�������ɃL���[�u�������Ȃ��悤��y���x��0�ɌŒ肵�Ă���
            velocity.y = 0.0f;

            // �L���[�u�𓮂���
            Debug.Log("nnn");
            Controller.Move(velocity * Time.deltaTime);
        }
        */
        // �v���C���[�ƃL���[�u�Ԃ̋������v�Z
        //Vector3 Apos = this.transform.position;
        //Vector3 Bpos = Target.transform.position;
        //float distance = Vector3.Distance(Apos, Bpos);

        /*
        // ������DetecDist�̐ݒ�l�����̏ꍇ�͌��m�t���O��false�ɂ���
        if(distance > DetecDist)
        {
            Debug.Log("aaa");
            InArea = false;
        }
        */
    }

    /*
    // �v���C���[�����m�G���A�ɂ����猟�m�t���O��true�ɂ���
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("�������I");
        InArea = true;
    }
    */
}

// �G�l�~�[���g�̃e���g���[�Ԃ��s��������Z
// �ҋ@���Ԃ���i2�b�ԁj�Z
// �t�����P�����߂Â��Ă�����ːi����
// �t�����P���̎����@�m�̓G�l�~�[�𒆐S�ɂ���i�G�l�~�[���������王���@�m���ꏏ�ɓ����j�����Z
// �ːi����������S�b�ԑҋ@�@���̂��Ǝ����̃e���g���[�ɖ߂�
// �S�b�ԑҋ@��A�����@�m���Ƀt�����P����������t�����P���ɍĂѓːi
// �ːi�����ꏊ�Ƀt�����P�������Ȃ�������Q�b�ԑҋ@���Ď����̃e���g���[�ɖ߂�
// 

