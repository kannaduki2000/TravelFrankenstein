using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private bool Follow = false;       //��x�ڂ̓��͂ł̂��Ă��邩�ۂ�

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        enemyJump = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ElectricCable")
        {
            cableFlag = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ElectricCable")
        {
            cableFlag = false;
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
        if (cableFlag && enemyMove == false)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                ECon.CablePointMove(gameObject, 0);
            }
        }


        //����p�ӂ��āA���̒���Y���W������
        Vector2 targetPos = Player.transform.position;
        targetPos.y = transform.position.y;

        //����
        float distance = Vector2.Distance(transform.position, Player.transform.position);


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
            if (enemyJump == false && Input.GetKeyDown(KeyCode.Space))
            {
                this.rb2d.AddForce(transform.up * this.jumpingPower);
                enemyJump = !enemyJump;
            }
        }

        //�G�l�~�[�̓����p
        if (enemyMove == false)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.transform.Translate(-0.01f, 0.0f, 0.0f);
                transform.localScale = new Vector3(-enemyScale.x, enemyScale.y, enemyScale.z);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                this.transform.Translate(0.01f, 0.0f, 0.0f);
                transform.localScale = enemyScale;
            }

            if (enemyJump == false && Input.GetKeyDown(KeyCode.Space))
            {
                this.rb2d.AddForce(transform.up * this.jumpingPower);
                enemyJump = !enemyJump;
            }
        }

        //������̐؂�ւ�����
        //1��ڂ̐؂�ւ����̓���
        if(isFollowing)
        {
            if (Input.GetKeyDown(KeyCode.F) && Follow == false)
            {
                mt.player_Move = !mt.player_Move;
                Following();
                enemyMove = !enemyMove;
                Follow = !Follow;
            }
        }
        //2��ڂ̐؂�ւ����A�v���C���[���������ăG�l�~�[�s����
        //���̏�Ԃ��Ɖ���Enter�����Ă��v���C���[�����������
        else if (Input.GetKeyDown(KeyCode.F) && Follow == true)
        {
            isFollowing = false;
            enemyMove = true;
            mt.player_Move = false;
        }

        //�Ăԃ{�^��(Delete���u��)�����������̓���
        //Follow��؂�ւ��邱�Ƃł�����x�Ǐ]��؂�ւ����ł��邨
        if (Follow == true && Input.GetKeyDown(KeyCode.Delete ) && enemyMove == true)
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ElectricCable")
        {
            //EventFlagManager.Instance.SetFlagState(EventFlagName.ElectricCableFlag, true);
        }
    }
}
