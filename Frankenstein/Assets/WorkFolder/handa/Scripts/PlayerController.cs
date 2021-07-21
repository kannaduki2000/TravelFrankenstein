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
    public GameObject canvasParent;
    private float canvasParentScale_x;

    private int presskeyFrames = 0;             //�������t���[����
    private int PressLong = 300;                //������
    private int PressShort = 100;               //�y����
    private bool Throw = false;                 //�����̃t���O
    Rigidbody2D rb;
    [SerializeField] KeyPlessThrow item;

    public Sprite[] AnnounceImageArray;
    public Image AnnounceImage;
    public Canvas TitleLogo;


    private bool enemyFollowFlg = false;
    // ���b�N�ŌF�q:GetCompornent�d����Œ��Ŏ擾�A�����G�̐�������͂��Ȃ̂ŏ��������邱��
    [SerializeField] private EnemyController enemyCon;
    [SerializeField] private Image hp;

    public SceneChange sc;
    public FadeControl fadeControl;

    // �V�[���J�ڂ����d�ŌĂ΂�Ȃ��悤�ɂ���
    private bool titleLogoflag = false;
    private bool map2Flag = false;

    public TextController textCon;

    public ElectricItem electricItem;

    private bool getUpTrigger = true;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        canvasParentScale_x = canvasParent.transform.localScale.x;
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
                player_Move = true;
                SetAnnounceImage(AnnounceName.T_CircleButton_StartUp);
                // �Z�{�^���Ή����Ȃ���
                if (Input.GetKeyDown(KeyCode.Return))
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
            // �d�C�̋z���C�x���g���I�����Ă���łȂ���HP�o�[����\�����Ȃ�
            if (EventFlagManager.Instance.GetFlagState(EventFlagName.electricAabsorption))
            {
                electricItem = enemy.GetComponent<ElectricItem>();
                //@item = enemy.GetComponent<KeyPlessThrow>();
                //@if (electricItem.IsThrow) Throw = true;
                if (enemyCon.isCharging)
                {
                }
                enemyTouchFlag = true;
                hpCanvas.SetActive(true);
            }
        }
        else
        {
            if (enemyTouchFlag) electricItem = null;
            //@item = null;
            //@Throw = false;
            enemyTouchFlag = false;
            hpCanvas.SetActive(false);
        }


        /*�v���C���[�̈ړ����͏���--------------------------------------------*/
        if(player_Move == false)
        {
            vx = 0;
            if (Input.GetKey("right"))
            {
                SystemTextEndPlayerMove();
                vx = speed;
                anim.SetBool("Walking", true);
                // HP�o�[�̌���
                canvasParent.transform.localScale = new Vector3(canvasParentScale_x, canvasParent.transform.localScale.y, canvasParent.transform.localScale.x);
            }
            else if (Input.GetKey("left"))
            {
                SystemTextEndPlayerMove();
                vx = -speed;
                anim.SetBool("Walking", true);
                canvasParent.transform.localScale = new Vector3(-canvasParentScale_x, canvasParent.transform.localScale.y, canvasParent.transform.localScale.x);
            }
            else
            {
                anim.SetBool("Walking", false);
            }

           

            if (Input.GetKey("space") && groundCheck)
            {
                SystemTextEndPlayerMove();
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

            //
            float x = Input.GetAxisRaw("Horizontal");
            if(x != 0)
            {
                SystemTextEndPlayerMove();
                Vector2 Iscale = gameObject.transform.localScale;
                if ((Iscale.x > 0 && x < 0) || (Iscale.x < 0 && x > 0))
                {
                    Iscale.x *= -1;
                    gameObject.transform.localScale = Iscale;
                }
            }
        }

        /*--------------------------------------------------------------------------*/
        Debug.Log(electricItem);
        /*�̗͂̌�������-----------------------------------------------------------------*/
        if (touchFlag || enemyTouchFlag || enemyFollowFlg || electricItem != null)
        {
            // �\��
            hpCanvas.SetActive(true);

            
            if (Input.GetKeyDown(KeyCode.Backspace)) // ���Ή�������
            {
                ViewAnnounceImage(false);

                // �d�C�𗬂�
                if (onElectricity == true || electricItem.ChargeFlag == false)
                {
                    electricItem.ChargeFlag = true;
                    HP -= electricItem.Power;
                    hp.fillAmount = HP / maxHP;
                    onElectricity = false; // ���ꂢ��񂩂�
                    // ���ꂽObject���̃C�x���g�̎��s
                    electricItem.Event();
                    electricItem.IsChargeEvent = true;
                }

                // �[�d����
                else if ((onElectricity == false || electricItem.ChargeFlag) && electricItem.IsCharge)
                {
                    HP += electricItem.Power;
                    //HP += 20;// HP�𑝂₷
                    hp.fillAmount = HP / maxHP;
                    // �����ɏ�����������
                    onElectricity = true;
                    electricItem.ChargeEvent();
                    electricItem.ChargeFlag = false;
                }

                // ���b�N�ŌF�q:�ǉ����܂���
                // �G��Ă��镨��Enemy�̏ꍇ
                if (enemyTouchFlag && EventFlagManager.Instance.GetFlagState(EventFlagName.electricAabsorption))
                {
                    EventFlagManager.Instance.SetFlagState(EventFlagName.enemyCharge, true);
                    // �Ǐ]�J�n
                    enemyCon.isFollowing = true;
                    // �[�d�����̂ł���ȏ�[�d�o���Ȃ��悤��
                    enemyCon.isCharging = false;
                    hpCanvas.SetActive(false);
                }
            }
            // �d�C���[�d
            else if (Input.GetKeyDown(KeyCode.Backspace))
            {
                //if (onElectricity == false || electricItem.ChargeFlag)
                //{
                //    Debug.Log("�[�d������");
                //    HP += electricItem.Power;
                //    //HP += 20;// HP�𑝂₷
                //    hp.fillAmount = HP / maxHP;
                //    // �����ɏ�����������
                //    onElectricity = true;
                //    electricItem.ChargeFlag = false;
                //}

                // �H�H�H
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


        // @�����鏈��
        //if (electricItem != null)
        //{
        //    if (electricItem.IsThrow)
        //    {
        //        if (Input.GetKey(KeyCode.W))
        //        {
        //            Debug.Log("�͂�");
        //            // �����ł��̃I�u�W�F�N�g���v���C���[�̎q���ɂ���
        //            item.gameObject.transform.parent = this.transform;
        //        }
        //        if (Input.GetKeyUp(KeyCode.W))
        //        {
        //            Debug.Log("������");
        //            item.transform.parent = null;
        //        }
        //    }
        //}



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

    /// <summary>
    /// �e�L�X�g�̕\����Ƀv���C���[����������A�i�E���X�摜���\���ɂ���@���O���K��������
    /// </summary>
    private void SystemTextEndPlayerMove()
    {
        if (EventFlagManager.Instance.GetFlagState(EventFlagName.text_System) && 
            EventFlagManager.Instance.GetFlagState(EventFlagName.text_SystemEnd) == false)
        {
            EventFlagManager.Instance.SetFlagState(EventFlagName.text_SystemEnd, true);
            ViewAnnounceImage(false);
        }
    }

    /// <summary>
    /// �e�L�X�g�̕\��(�����A�j���[�V�����p)
    /// </summary>
    public void TextAnim()
    {
        textCon.SetTextActive(true);
    }

    /// <summary>
    /// �ړ��\(�����A�j���[�V�����p)
    /// </summary>
    public void PlayerMove()
    {
        player_Move = false;
    }

    /// <summary>
    /// �ړ��s�\
    /// </summary>
    public void PlayerNotMove()
    {
        player_Move = true;
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
        if (collision.gameObject.GetComponent<ElectricItem>() != null)
        {
            electricItem = collision.gameObject.GetComponent<ElectricItem>();

            // �������Object�Ȃ�����擾
            //if (electricItem.IsThrow)
            //{
            //    Throw = true;
            //    //�A�C�e���N���X�̎擾
            //    item = collision.gameObject.GetComponent<KeyPlessThrow>();
            //}
        }

        if (collision.gameObject.tag == "HomeApp")
        {
            //SetAnnounceImage(AnnounceName.T_SquareButton_Input);
            touchFlag = true;
        }

        //����̏ꏊ��ʉ߂����甭��
        if (collision.gameObject.tag == "GoTitleLogo" && titleLogoflag == false && EventFlagManager.Instance.GetFlagState(EventFlagName.enemyCharge))
        {
            titleLogoflag = true;
            fadeControl.Fade("wout", () => sc.SceneSwitching("TitleLogo", true));
        }

        if (collision.gameObject.tag == "GoMap2")
        {
            map2Flag = true;
            fadeControl.Fade("out", () => fadeControl.sceneChange.SceneSwitching("TentativeTitle"));
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ElectricItem>() != null)
        {
            //if (electricItem != null && electricItem.IsThrow)
            //{
            //    Throw = false;
            //    presskeyFrames = 0;
            //    item = null;
            //}
            electricItem = null;
        }

        if (collision.gameObject.tag == "HomeApp")
        {
            touchFlag = false;
            hpCanvas.SetActive(false);
        }
        else
        {
            groundCheck = false;
        }

        // ��U����
        //if (collision.gameObject.tag == "Item")
        //{
        //    Throw = false;
        //    presskeyFrames = 0;
        //    //item.transform.parent = null;
        //    Debug.Log("exit");
        //}
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


