using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    //CharacterController Controller;
    //Transform Target;

    [SerializeField]
    private GameObject objGet;
    GameObject Ground;

    //[SerializeField]
    //float MoveSpeed = 2.0f;
    //int DetecDist = 8;
    //bool InArea = false;

    // �����ƁA��������
    public float speedX = 1; // �X�s�[�hX
    public float speedY = 0; // �X�s�[�hY
    public float speedZ = 0; // �X�s�[�hZ
    public float second = 1; // ������b��
    private bool move = false;
    public float Stop = 2;

    public Transform targetPos; // �s�������ꏊ
    public Vector3 startPos;  // ���̏ꏊ

    public bool isSearch = false;
    public bool isloop = false;
    private bool move2 = false;

    private float thisXScale;

    public float time = 0;
    public float time2 = 0;
    public float time3 = 0;
    public float time4 = 0;

    Transform Player;
    Transform Bone;

    Rigidbody2D rb;
    public float speed = 1; // �X�s�[�hX

    int bone = 2;
   


    private void Update()
    {
        if (isloop) { return; }
        // �E
        if (isSearch)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos.position, 1f * Time.deltaTime);
            if (targetPos.position.x - transform.position.x <= Mathf.Abs(0.1f))
            {
                // 2�b��~
                time += Time.deltaTime;
                if (time >= 2)
                {
                    time = 0;
                    Vector3 left = new Vector3(-thisXScale, transform.localScale.y, transform.localScale.z);
                    transform.localScale = left;
                    isSearch = false;
                }
            }
        }
        // ��
        else
        {            
            transform.position = Vector3.MoveTowards(transform.position, startPos, 1f * Time.deltaTime);
            if (transform.position.x - startPos.x <= Mathf.Abs(0.1f)) 
            {
                time += Time.deltaTime;
                // 2�b��~
                if (time >= 2)
                {
                    time = 0;
                    Vector3 right = new Vector3(thisXScale, transform.localScale.y, transform.localScale.z);
                    transform.localScale = right;
                    isSearch = true;
                }
               
            }
        }
        Vector3 direction = (Player.position - transform.position).normalized;

        
    }

    private void Start()
    {
        startPos = this.transform.position;
        Debug.Log(startPos);

        // �v���C���[�̈ʒu�擾
        Player = GameObject.Find("Player").transform;
        Bone = GameObject.Find("Bone").transform;

        thisXScale = transform.localScale.x;

        rb = GetComponent<Rigidbody2D>();
    }

    // �΂ɓ��������瓮�����~�߂鏈��
    private void OnCollisionEnter2D(Collision2D collision)
    {
       // Stone�̃^�O���t���Ă�����̂ɓ���������
       if (collision.gameObject.tag == "Stone")
       {
            isloop = true;
            speedX = 0;
            // �E�����̏�Ԃœ���������
           if (this.transform.localScale.x == 1)
           {
               transform.localScale = new Vector3(1, 1, 1);
           }
           // �������̏�Ԃœ���������
           else
           {
               transform.localScale = new Vector3(-1, 1, 1);
           }
       }
    }

    // �����擾
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bone")
        {
            bone = 0;
        }
    }



    // �ːi
    private void OnTriggerStay2D(Collider2D collision)
    {
        // �v���C���[�^�O�����Ă������߂Â��Ă�����
        if (collision.gameObject.tag == "Player")
        {
            if(bone < 1)
            {
                //float speed = 0;
                time2 += Time.deltaTime;
                if (time2 > 2)
                {
                    isloop = true;

                    if (transform.position.x < Player.position.x)
                    {
                        //�E
                        rb.velocity = new Vector2(speed, 0);
                        transform.localScale = new Vector2(1, 1);
                        isloop = false;
                    }
                    else if (transform.position.x > Player.position.x)
                    {
                        //��
                        rb.velocity = new Vector2(-speed, 0);
                        transform.localScale = new Vector2(-1, 1);
                        isloop = false;
                    }
                }
            }
        }     
    }


    // ���̈ʒu�ɖ߂�
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            time2 = 0;
            time3 += Time.deltaTime;
            if (transform.position.x < Player.position.x)
            {
                //�E
                rb.velocity = new Vector2(speed, 0);
                transform.localScale = new Vector2(-1, 1);
            }
            else if (transform.position.x > Player.position.x)
            {
                //��
                rb.velocity = new Vector2(-speed, 0);
                transform.localScale = new Vector2(1, 1);
            }
        }
        if (collision.gameObject.tag == "Bone")
        {
            bone = 2;
        }
    }

}

// �G�l�~�[���g�̃e���g���[�Ԃ��s��������Z
//  �ҋ@���Ԃ���i2�b�ԁj�Z
// �t�����P�����߂Â��Ă�����ːi���� �Z
//  �t�����P���̎����@�m�̓G�l�~�[�𒆐S�ɂ���i�G�l�~�[���������王���@�m���ꏏ�ɓ����j�Z
// �ːi����������S�b�ԑҋ@�@���̂��Ǝ����̃e���g���[�ɖ߂�
//  �S�b�ԑҋ@��A�����@�m���Ƀt�����P����������t�����P���ɍĂѓːi
// �ːi�����ꏊ�Ƀt�����P�������Ȃ�������Q�b�ԑҋ@���Ď����̃e���g���[�ɖ߂� ��

    // ���������Ă���Ԃ͏P���Ă��Ȃ�����

    // �΂ɓ��������瓮�������S�Ɏ~�߂�Z

    // �v���C���[�^�O�̎擾
    // Player = GameObject.FindWithTag("Player");
    //Target = Player.transform;

    //Controller = GetComponent<CharacterController>();

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

    /*
    // �v���C���[�����m�G���A�ɂ����猟�m�t���O��true�ɂ���
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("�������I");
        InArea = true;
    }
    */

    // �ːi�̏��� �v
    //   Vector3 direction = (Player.position - transform.position).normalized;
    //  speed = 3.5f;
    //transform.Translate(direction * speed * Time.deltaTime);

    // �����ƁA��������

    /*
    void FixedUpdate()
    {
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    Debug.Log("E");
        //    StartCoroutine(Move());
        //}
        //if (move == true)
        //{

        //    float s = Mathf.Sin(Time.time); // �ړ��ʂ����߂�
        //    this.transform.Translate(speedX * s / 50, speedY * s / 50, speedZ * s / 50);
        //    // �f�t�H���g���E�����̏ꍇ
        //    // �X�P�[���l���o��
        //    Vector3 scale = transform.localScale;

        //    if (s >= 0)
        //    {

        //        // �E�����Ɉړ���
        //        scale.x = 1; // ���̂܂܁i�E�����j
        //    }
        //    else
        //    {
        //        // �������Ɉړ���
        //        scale.x = -1; // ���]����i�������j
        //    }
        //    // ���������
        //    transform.localScale = scale;
        //}

    }  
*/

// �v
/*
    // Player�̃^�O���t���Ă�����̂ɓ���������
else if (collision.gameObject.tag == "Player")
{
    isloop = true;
    speed = 0;
    TimeCount3();
    if (time4 > 4)
    {
        isloop = false;
    }
}
*/
