using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public GameObject Player;

    private float x_val;
    private float speed;

    //?v???C???[???????????l???????i?????A?W?????v?j
    public float inputSpeed;
    public float jumpingPower;
    //
    public LayerMask CollisionLayer;
    [SerializeField] private LayerMask enemyLayer; // ???b?N???F?q:?G??Layer?????p
    private bool jumpFlg = false;

    //public Vector2 Speed = new Vector2(1, 1);   //???x
    private int presskeyFrames = 0;             //???????t???[????
    private int PressLong = 300;                 //???????????????l
    private int PressShort = 100;                //?y?????????????????l
    private bool aa = false;
    Item item;

    [SerializeField] int maxHP = 100;
    [SerializeField] float HP = 100;
    [SerializeField] private bool touchFlag = false;
    [SerializeField] private bool enemyTouchFlag = false; // ???b?N???F?q:?t???O????
    public GameObject hpCanvas;
    private float hpCanvasScale_x;

    public bool player_Move = false;

    private bool enemyFollowFlg = false;

    public GameObject enemy;

    //public EnemyController enemyCon;

    // ???b?N???F?q:GetCompornent?d???????????????A?????G??????????????????????????????????
    [SerializeField] private EnemyController enemyCon;
    [SerializeField] private Image hp;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        hpCanvasScale_x = hpCanvas.transform.localScale.x;
        // ?F?q:???????????????v????
        //enemy = GameObject.Find("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        // ???b?N???F?q:Layer????????????????????Linecast??????
        if (GetEnemyLayer())
        {
            if (enemyCon.isCharging)
            {
                enemyTouchFlag = true;
            }
        }
        else
        {
            enemyTouchFlag = false;
        }


        /*?v???C???[??????????????--------------------------------------------*/
        if(player_Move == false)
        {
            //?????L?[??????????????
            x_val = Input.GetAxis("Horizontal");
            jumpFlg = IsCollision();
            //Space?L?[??????????????
            if (Input.GetKeyDown("space") && jumpFlg)
            {
                jump();
            }
        }
        
        /*-----------------------------------------------------------------*/

        /*?E???A????????????????----------------------------------------------------*/
        if (aa)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //?X?y?[?X??????
                presskeyFrames += (Input.GetKey(KeyCode.LeftShift)) ? 1 : 0;
                Debug.Log(presskeyFrames);
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                //?????X?y?[?X????????????????????????????
                if (PressLong <= presskeyFrames)
                {
                    item.Hight();
                    Debug.Log("????");
                    this.gameObject.transform.DetachChildren();
                }
                //?????X?y?[?X????????????????????????
                else if (PressShort <= presskeyFrames)
                {
                    item.Low();
                    Debug.Log("?Z??");
                    this.gameObject.transform.DetachChildren();
                }
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                this.gameObject.transform.DetachChildren();
            }
        }
        /*-----------------------------------------------------------------*/

        /*??????????????-----------------------------------------------------------------*/
        if (touchFlag || enemyTouchFlag)
        {
            // ?\??
            hpCanvas.SetActive(true);

            // ?d?C??????
            if (Input.GetKeyDown(KeyCode.Return))
            {
                HP -= 30;// HP????????
                // ???b?N???F?q:HP?o?[
                hp.fillAmount = HP / maxHP;
                Debug.Log(HP);
                
                // ???b?N???F?q:????????????
                // ?G????????????Enemy??????
                if (enemyTouchFlag)
                {
                    // ???]?J?n
                    enemyCon.isFollowing = true;
                    // ?[?d?????????????????[?d?o????????????
                    enemyCon.isCharging = false;
                    hpCanvas.SetActive(false);
                }
                
            }
            // ?d?C???[?d
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                HP += 30;// HP????????
                Debug.Log(HP);
                // ??????????????????

                if(enemyFollowFlg)
                {

                }
            }
        }
        // ???b?N???F?q:HP?\??????Object?????????????????I??HP?o?[?????\??????????
        else
        {
            hpCanvas.SetActive(false);
        }
        /*-----------------------------------------------------------------*/
    }

    /*?v???C???[??????????--------------------------------------------------*/
    void FixedUpdate()
    {
        //???@
        if (x_val == 0)
        {
            speed = 0;
        }
        //?E??????
        else if (x_val > 0)
        {
            speed = inputSpeed  ;
            transform.localScale = new Vector3(50, 50, 100);//?E??????????
            // ???b?N???F?q:HP?o?[????????????
            Vector3 hpTransform = new Vector3(hpCanvasScale_x, hpCanvas.transform.localScale.y, hpCanvas.transform.localScale.z);
            hpCanvas.transform.localScale = hpTransform;
        }
        //????????
        else if (x_val < 0)
        {
            speed = inputSpeed * -1;
            transform.localScale = new Vector3(-50, 50, 100);//????????????
            // ???b?N???F?q:HP?o?[????????????
            Vector3 hpTransform = new Vector3(-hpCanvasScale_x, hpCanvas.transform.localScale.y, hpCanvas.transform.localScale.z);
            hpCanvas.transform.localScale = hpTransform;
        }
        // ?L?????N?^?[?????? Vextor2(x???X?s?[?h?Ay???X?s?[?h(????????))
        rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
    }
    /*-----------------------------------------------------------------*/


    /*-----------------------------------------------------*/
    void jump()
    {
        rb2d.AddForce(Vector2.up * jumpingPower);
        jumpFlg = false;
    }
    /*------------------------------------------------------------------*/

    /*?????W?????v???h??????------------------------------------------------*/
    bool IsCollision()
    {
        Vector3 left_SP = transform.position - Vector3.right * 0.2f;
        Vector3 right_SP = transform.position + Vector3.right * 0.2f;
        Vector3 EP = transform.position - Vector3.up * 1.3f;
        return Physics2D.Linecast(left_SP, EP, CollisionLayer)
               || Physics2D.Linecast(right_SP, EP, CollisionLayer);
    }
    /*-------------------------------------------------------------------*/

    /// <summary>
    /// ?G?????C???[????????????????????????????
    /// </summary>
    /// <returns></returns>
    private bool GetEnemyLayer()
    {
        Vector3 left = transform.position + Vector3.up * 1f - Vector3.right * 3.5f;
        Vector3 right = transform.position + Vector3.up * 1f + Vector3.right * 3.5f;
        // ???????R?????g???????f?o?b?O?p??????????????
        //Debug.DrawLine(left, right);
        return Physics2D.Linecast(left, right, enemyLayer);
    }


    /*---------------------------*/
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            aa = false;
            Debug.Log("exit");
        }
    }
    /*-------------------------------------------------------------------*/

    /*-------------------------------------------------------------------*/
    //?A?C?e??????????????????
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            Debug.Log("stay");

            //W??????????????
            if (Input.GetKey(KeyCode.W))
            {
                aa = true;
                //?A?C?e???N???X??????
                item = collision.gameObject.GetComponent<Item>();

                //?A?C?e????Y??????????
                // ???????????I?u?W?F?N?g???v???C???[???q????????
                item.gameObject.transform.parent = this.transform;
            }

        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            this.gameObject.transform.DetachChildren();
        }
    }
    /*-------------------------------------------------------------------*/

    /*HP?o?[???\???????^?O??????-----------------------------------------*/
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

        

    }
    /*-------------------------------------------------------------------*/

}
