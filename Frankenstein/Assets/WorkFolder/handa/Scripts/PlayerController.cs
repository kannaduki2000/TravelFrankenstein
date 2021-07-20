using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;


[System.Serializable]
public enum AnnounceName
{
    // Tutorial
    T_Put_Electric_Enemy,           // �G�l�~�[�̎��̂ɓd�C������
    T_Throw_Having,                 // ������A����
    T_Come_Out_Home,                // �Ƃ���o��
    T_Release_Object,               // �������ŏ�ɓ�����A����
    T_SquareButton,                 // �l�p�{�^��
    T_CircleButton_StartUp,         // �ۃ{�^���F�N������
    T_Leftstick_Move,               // ���X�e�B�b�N�F�ړ�
    T_SquareButton_Absorption,      // �l�p�{�^���F�d�C���z������
    T_SquareButton_Input,           // �l�p�{�^���F�d�C������

    // Stage1
    S1_TriangleButton_Gear,         // �O�p�{�^���F���ԂɂȂ�
    S1_CircleButton_EnemyCall,      // �ۃ{�^���F�G�l�~�[���Ă�
    S1_SquareButton_ElectricCable,  // �l�p�{�^���F�d����`��
    S1_LButton_EnemyChange,         // L�{�^���F����؂�ւ�
    S1_RButton_Push,                // R�{�^���F����
    S1_SquareButton,                // �l�p�{�^��
    S1_SquareButton_Input,          // �l�p�{�^���F�d�C������
    S1_SquareButton_Absorption,     // �l�p�{�^���F�d�C���z������

}

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb2d;
    Animator anim;
    public GameObject Player;
    public GameObject enemy;

    public float speed;//���x
    public float jumpPower;//�W�����v
    public float vx = 0;
    private bool leftFlag = false;
    private bool jumpFlag = false;
    private bool groundCheck = false;//�ڒn���� 
    private bool pushFlag = false;
    public bool player_Move = false;

    //
    [SerializeField] private LayerMask enemyLayer; // ���b�N�ŌF�q:�G��Layer�擾�p

    public int maxHP = 100;
    public float HP = 100;
    public bool touchFlag = false;
    public bool enemyTouchFlag = false; // ���b�N�ŌF�q:�t���O�ǉ�
    public bool onElectricity = true;
    public GameObject hpCanvas;
    private float hpCanvasScale_x;

    private int presskeyFrames = 0;             //�������t���[����
    private int PressLong = 300;                //������
    private int PressShort = 100;               //�y����
    private bool Throw = false;                 //�����̃t���O
    Rigidbody2D rb;
    KeyPlessThrow item;

    public Sprite[] AnnounceImageArray;
    public Image AnnounceImage;


    private bool enemyFollowFlg = false;
    // ���b�N�ŌF�q:GetCompornent�d����Œ��Ŏ擾�A�����G�̐�������͂��Ȃ̂ŏ��������邱��
    [SerializeField] private EnemyController enemyCon;
    [SerializeField] private Image hp;

    public SceneChange sc;
    public FadeControl fadeControl;

    private bool getUpTrigger = true;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        hpCanvasScale_x = hpCanvas.transform.localScale.x;
        sc = FindObjectOfType<SceneChange>();
        fadeControl = FindObjectOfType<FadeControl>();
    }

    // Update is called once per frame
    void Update()
    {

        // �t�����P�����܂��N���オ���Ă��Ȃ����
        if (EventFlagManager.Instance.GetFlagState(EventFlagName.frankensteinGetUp) == false)
        {
            return;
        }
        else
        {
            // ��x�ł��N���オ�������Ƃ�����΋N����A�j���[�V�����̃X�L�b�v
            if (EventFlagManager.Instance.GetFlagState(EventFlagName.getupFlag))
            {
                anim.SetBool("GetUpFlag", true);
                getUpTrigger = false;
            }

            if (getUpTrigger)
            {
                SetAnnounceImage(AnnounceName.T_CircleButton_StartUp);
                if (Input.GetKeyDown(KeyCode.P))
                {
                    EventFlagManager.Instance.SetFlagState(EventFlagName.getupFlag, true);
                    ViewAnnounceImage(false);
                    getUpTrigger = false;
                    anim.SetTrigger("isGetUp");
                }
            }
        }

        //anim = gameObject.GetComponent<Animator>();
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
                anim.SetBool("Walking", true);
            }
            else if (Input.GetKey("left"))
            {
                vx = -speed;
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

        //
        float x = Input.GetAxisRaw("Horizontal");
        if(x != 0)
        {
            Vector2 Iscale = gameObject.transform.localScale;
            if ((Iscale.x > 0 && x < 0) || (Iscale.x < 0 && x > 0))
            {
                Iscale.x *= -1;
                gameObject.transform.localScale = Iscale;
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

        /*�A�C�e���������͏���---------------------------------------------*/
        if (Throw)
        {
            if (Input.GetKey(KeyCode.R))//���c�FSpace����R�ɕύX
            {
                //�X�y�[�X�̔���
                //memo  �w? true:false�x
                presskeyFrames += (Input.GetKey(KeyCode.R)) ? 1 : 0;//���c�FSpace����R�ɕύX
                Debug.Log(presskeyFrames);
            }

            if (Input.GetKeyUp(KeyCode.R))//���c�FSpace����R�ɕύX
            {
                //�����X�y�[�X�����������ꂽ��
                if (PressLong <= presskeyFrames)

                //���߂ɓ�����
                {
                    item.Hight();
                    Debug.Log("����");
                }

                //�����X�y�[�X�������ꂽ��
                else if (PressShort <= presskeyFrames)

                //��߂ɓ�����
                {
                    item.Low();
                    Debug.Log("�Z��");
                }
            }

            //if (Input.GetKeyUp(KeyCode.W))
            //{
            //    this.gameObject.transform.DetachChildren();
            //}
        }
        /*-----------------------------------------------------------------*/

    }



    /*�����W�����v��h������------------------------------------------------*/
    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(vx, rb2d.velocity.y);
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

    /// <summary>
    /// �A�i�E���X�摜�����Ă���\��
    /// </summary>
    /// <param name="name"></param>
    public void SetAnnounceImage(AnnounceName name)
    {
        AnnounceImage.sprite = AnnounceImageArray[(int)name];
        ViewAnnounceImage(true);
    }

    /// <summary>
    /// �A�i�E���X�摜�̕\��/��\��
    /// </summary>
    /// <param name="isView"></param>
    public void ViewAnnounceImage(bool isView)
    {
        AnnounceImage.enabled = isView;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            groundCheck = true;
        }

        if (collision.gameObject.tag == "Item")
        {
            Debug.Log("stay");

            //item = collision.gameObject.GetComponent<Item>();
            //W�������Ă�����
            if (Input.GetKey(KeyCode.W))
            {
                Throw = true;
                //�A�C�e���N���X�̎擾
                item = collision.gameObject.GetComponent<KeyPlessThrow>();

                //�A�C�e����Y�����オ��
                // �����ł��̃I�u�W�F�N�g���v���C���[�̎q���ɂ���
                item.gameObject.transform.parent = this.transform;
            }
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            item.transform.parent = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HomeApp")
        {
            touchFlag = true;
        }

        //����̏ꏊ��ʉ߂����甭��
        if (collision.gameObject.tag == "GoTitleLogo")
        {
            fadeControl.Fade("wout", () => fadeControl.sceneChange.SceneSwitching("TitleLogo", true));
        }

        if (collision.gameObject.tag == "GoMap2")
        {
            fadeControl.Fade("out", () => fadeControl.sceneChange.SceneSwitching("TentativeTitle"));
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

        if (collision.gameObject.tag == "Item")
        {
            Throw = false;
            presskeyFrames = 0;
            //item.transform.parent = null;
            Debug.Log("exit");
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


