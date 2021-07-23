using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualShockInput;

public class EnemyController : ElectricItem
{
    Rigidbody2D rb2d;
    public PlayerController mt;
    public GameObject Player;
    public GameObject enemy;
    public float stopDistance;  //�~�܂�Ƃ��̋���
    public float inputSpeed;    //�ړ����x
    public float jumpingPower;  //�W�����v

    // ���b�N�ŌF�q:�[�d�\���ǂ����𔻕ʂ���t���O
    public bool isCharging = true; // HP��0�ɂȂ�����true�ɂ���悤�ɂ��Ă�������
    public bool isFollowing = true;   //�Ǐ]���邩�ǂ���
    public bool enemyMove = true;      //�G�l�~�[�̓���
    private bool enemyJump = false;         //�W�����v�p
    public bool Follow = false;       //��x�ڂ̓��͂ł̂��Ă��邩�ۂ�

    public Camera camera;

    // �����ƁA��������
    public float speedX = 1; // �X�s�[�hX
    public float speedY = 0; // �X�s�[�hY
    public float speedZ = 0; // �X�s�[�hZ
    public float second = 1; // ������b��
    public bool isWandering = true;//�p�j���邩�ǂ���
    float time = 0f;

    Vector3 enemyScale;

    bool cableFlag = false;
    public ElectricCableController ECon;

    CableData cableData;
    [SerializeField] private float moveSpeed;
    private float vx;
    private bool carFlag = false;
    [SerializeField] private CarPush car;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        enemyJump = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ElectricCable")
        {
            cableFlag = true;
            cableData = collision.gameObject.GetComponent<CableData>(); // �d���̏��擾
            // �d����`���\��
        }

        if (collision.gameObject.tag == "Car")
        {
            carFlag = true;
            //Follow = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ElectricCable")
        {
            cableFlag = false;
            cableData = null;
        }

        if (collision.gameObject.tag == "Car")
        {
            carFlag = false;
            //Follow = true;
        }
    }

    //�����ɒ����܂ŃW�����v�����Ȃ��}��

    // Start is called before the first frame update
    void Start()
    {
        this.rb2d = GetComponent<Rigidbody2D>();
        enemyScale = transform.localScale;
        IsThrow = true;
    }

    // Update is called once per frame
    void Update()
    {
        // �ړ�
        rb2d.velocity = new Vector2(vx, rb2d.velocity.y);


        if (cableFlag && enemyMove == false)
        {
            if (Input.GetKeyDown(KeyCode.P) || DSInput.PushDown(DSButton.Square))
            {
                if (cableData.point == CablePoint.start) ECon.CablePointMove(gameObject, cableData.CableNum);
                else ECon.CablePointMove(gameObject, cableData.CableNum, false);
            }
        }

        if (carFlag)
        {
            // �摜�̕\��
            if (Input.GetKeyDown(KeyCode.R) || DSInput.PushDown(DSButton.R1))
            {
                EnemyNotMove();
                car.crash = true;
                carFlag = false;
            }
        }

        //����p�ӂ��āA���̒���Y���W������
        Vector2 targetPos = Player.transform.position;
        targetPos.y = transform.position.y;

        //����
        float distance = Vector2.Distance(transform.position, Player.transform.position);

        // �Ǐ]�p
        if (isFollowing)
        {
            //if(�Ԃ̋������~�܂�Ƃ��̋����ȏ�Ȃ�?)
            if (distance > stopDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                new Vector2(Player.transform.position.x, enemy.transform.position.y),
                inputSpeed * Time.deltaTime);
            }
            //enemy��player

            // �E
            if (Player.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-enemyScale.x, enemyScale.y, enemyScale.z);
            }

            // ��
            else if (Player.transform.position.x > transform.position.x)
            {
                transform.localScale = enemyScale;
            }

            //�W�����v
            if (enemyJump == false && (Input.GetKeyDown(KeyCode.Space) || DSInput.PushDown(DSButton.Cross)))
            {
                this.rb2d.AddForce(transform.up * this.jumpingPower);
                enemyJump = !enemyJump;
            }
        }

        //�G�l�~�[�̓����p
        if (enemyMove == false)
        {
            vx = 0;
            var input = Input.GetAxis("J_Horizontal");
            if (Input.GetKey(KeyCode.LeftArrow) || input < -0.5)
            {
                vx = -moveSpeed;
                //this.transform.Translate(-0.01f, 0.0f, 0.0f);
                transform.localScale = new Vector3(-enemyScale.x, enemyScale.y, enemyScale.z);
            }

            if (Input.GetKey(KeyCode.RightArrow) || 0.5 < input)
            {
                vx = moveSpeed;
                //this.transform.Translate(0.01f, 0.0f, 0.0f);
                transform.localScale = enemyScale;
            }

            if (enemyJump == false && (Input.GetKeyDown(KeyCode.Space) || DSInput.PushDown(DSButton.Cross)))
            {
                this.rb2d.AddForce(transform.up * this.jumpingPower);
                enemyJump = !enemyJump;
            }
            input = 0;
        }

        //������̐؂�ւ�����
        //1��ڂ̐؂�ւ����̓���
        if(isFollowing)
        {
            if ((Input.GetKeyDown(KeyCode.F) || DSInput.PushDown(DSButton.L1)) && Follow == false)
            {
                // �J�����Ǐ]�̑Ώۂ��G�l�~�[�ɕύX
                camera.GetComponent<CameraClamp>().targetToFollow = gameObject.transform;
                mt.player_Move = !mt.player_Move;
                Following();
                enemyMove = !enemyMove;
                Follow = !Follow;
            }
        }
        //2��ڂ̐؂�ւ����A�v���C���[���������ăG�l�~�[�s����
        //���̏�Ԃ��Ɖ���Enter�����Ă��v���C���[�����������
        else if ((Input.GetKeyDown(KeyCode.F) || DSInput.PushDown(DSButton.L1)) && Follow == true)
        {
            camera.GetComponent<CameraClamp>().targetToFollow = Player.transform;
            isFollowing = false;
            enemyMove = true;
            mt.player_Move = false;
        }

        //�Ăԃ{�^��(Delete���u��)�����������̓���
        //Follow��؂�ւ��邱�Ƃł�����x�Ǐ]��؂�ւ����ł��邨
        if (Follow == true && (Input.GetKeyDown(KeyCode.Delete ) || DSInput.PushDown(DSButton.R1)) && enemyMove == true && isFollowing)
        {
            isFollowing = true;
            Follow = !Follow;
        }

    }

    private void FixedUpdate() // �����ƁA��������
    {

        if (isWandering == true)
        {
            time += Time.deltaTime;
            float s = Mathf.Sin(Time.time);
            this.transform.Translate(speedX * s / 50, speedY * s / 50, speedZ * s / 50);
            Vector3 scale = transform.localScale;
            if (s >= 0)
            {
                scale.x = enemyScale.x;
            }
            else
            {
                scale.x = -enemyScale.x;
            }
            transform.localScale = scale;
        }
        if (isFollowing == true)
        {
            isWandering = false;
        }


    }

    public void EnemyMove()
    {
        enemyMove = false;
    }

    public void EnemyNotMove()
    {
        enemyMove = true;
        vx = 0;
        rb2d.velocity = Vector2.zero;
    }

    // ���ĒǏ]�̐؂�ւ�����������
    public void Following()
    {
        isFollowing = !isFollowing;
    }

    // ���đ���̐؂�ւ�����������
    public void PlayerChange()
    {
        // �v���C���[�̑�����ł��Ȃ�����
        mt.player_Move = !mt.player_Move;

        // ���쌠��G�Ɉړ�������
        Following();
        enemyMove = !enemyMove;
    }
}
