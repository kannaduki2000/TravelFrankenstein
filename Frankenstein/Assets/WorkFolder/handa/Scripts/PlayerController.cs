using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb2d;
    Animator anim;
    public GameObject Player;
    public GameObject enemy;

    public float speed;//���x
    public float jumpPower;//�W�����v
    private float vx = 0;
    private bool leftFlag = false;
    private bool jumpFlag = false;
    private bool groundCheck = false;//�ڒn���� 
    private bool pushFlag = false;
    public bool player_Move = false;

    //
    [SerializeField] private LayerMask enemyLayer; // ���b�N�ŌF�q:�G��Layer�擾�p

    [SerializeField] int maxHP = 100;
    [SerializeField] float HP = 100;
    [SerializeField] private bool touchFlag = false;
    [SerializeField] private bool enemyTouchFlag = false; // ���b�N�ŌF�q:�t���O�ǉ�
    private bool onElectricity = true;
    public GameObject hpCanvas;
    private float hpCanvasScale_x;


    private bool enemyFollowFlg = false;
    // ���b�N�ŌF�q:GetCompornent�d����Œ��Ŏ擾�A�����G�̐�������͂��Ȃ̂ŏ��������邱��
    [SerializeField] private EnemyController enemyCon;
    [SerializeField] private Image hp;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        hpCanvasScale_x = hpCanvas.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        anim = gameObject.GetComponent<Animator>();

        // ���b�N�ŌF�q:Layer�ł���Ă����ۂ��̂�Linecast�Ŏ擾
        if (GetEnemyLayer())
        {
            if (enemyCon.isCharging)
            {
                enemyTouchFlag = true;
            }
            hpCanvas.SetActive(true);
        }
        else
        {
            enemyTouchFlag = false;
            hpCanvas.SetActive(false);
        }


        /*�v���C���[�̈ړ����͏���--------------------------------------------*/
        if(player_Move == false)
        {
            vx = 0;
            if (Input.GetKey("right"))
            {
                vx = speed;
                leftFlag = false;
                anim.SetBool("Walking", true);
            }
            else if (Input.GetKey("left"))
            {
                vx = -speed;
                leftFlag = true;
                anim.SetBool("Walking", true);
            }
            else
            {
                anim.SetBool("Walking", false);
            }

            if (Input.GetKey("space") && groundCheck)
            {
                if (pushFlag == false)
                {
                    jumpFlag = true;
                    pushFlag = true;
                }
                else
                {
                    pushFlag = false;
                }
            }

        }
        /*--------------------------------------------------------------------------*/

        /*�̗͂̌�������-----------------------------------------------------------------*/
        if (touchFlag || enemyTouchFlag || enemyFollowFlg)
        {
            // �\��
            hpCanvas.SetActive(true);

            // �d�C�𗬂�
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if(onElectricity == true)
                {
                    HP -= 20;// HP�����炷
                             // ���b�N�ŌF�q:HP�o�[
                    hp.fillAmount = HP / maxHP;
                    Debug.Log(HP);
                    onElectricity = false;

                }

                // ���b�N�ŌF�q:�ǉ����܂���
                // �G��Ă��镨��Enemy�̏ꍇ
                if (enemyTouchFlag)
                {
                    // �Ǐ]�J�n
                    enemyCon.isFollowing = true;
                    // �[�d�����̂ł���ȏ�[�d�o���Ȃ��悤��
                    enemyCon.isCharging = false;
                    hpCanvas.SetActive(false);
                }
            }
            // �d�C���[�d
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                if(onElectricity == false)
                {
                    HP += 20;// HP�𑝂₷
                    hp.fillAmount = HP / maxHP;
                    Debug.Log(HP);
                    // �����ɏ�����������
                    onElectricity = true;
                }

                if (enemyFollowFlg)
                {
                    enemyCon.isFollowing = false;
                }
            }
        }
        // ���b�N�ŌF�q:HP�\������Object���痣�ꂽ�狭���I��HP�o�[���\���ɂ��܂�
        else
        {
            hpCanvas.SetActive(false);
        }
        /*-----------------------------------------------------------------*/
    }

    /*�����W�����v��h������------------------------------------------------*/
    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(vx, rb2d.velocity.y);
        this.GetComponent<SpriteRenderer>().flipX = leftFlag;
        if (jumpFlag)
        {
            jumpFlag = false;
            rb2d.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
        }
    }
    /*-------------------------------------------------------------------*/

    /// <summary>
    /// �G�̃��C���[�����邩�ǂ������擾����֐�
    /// </summary>
    /// <returns></returns>
    private bool GetEnemyLayer()
    {
        Vector3 left = transform.position + Vector3.up * 0.5f - Vector3.right * 3.5f;
        Vector3 right = transform.position + Vector3.up * 0.5f + Vector3.right * 3.5f;
        // �����̃R�����g�����΃f�o�b�O�p�̐��������܂�
        Debug.DrawLine(left, right);
        return Physics2D.Linecast(left, right, enemyLayer);
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            groundCheck = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HomeApp")
        {
            touchFlag = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HomeApp")
        {
            touchFlag = false;
            hpCanvas.SetActive(false);
        }
        else
        {
            groundCheck = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            touchFlag = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            touchFlag = false;
        }
    }
}


