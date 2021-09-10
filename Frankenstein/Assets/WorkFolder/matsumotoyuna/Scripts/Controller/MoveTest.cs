using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour
{
    public GameObject player;

    public bool playerMove = false;  //プレイヤーが動く用
    private bool Jump = false;       //ジャンプ用

    Rigidbody2D rigid2D;
    float jumpForce = 300.0f;        //ジャンプ力

    public SceneChange sc;
    public FadeControl fadeControl;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Jump = false;
        //↑床に着くまでジャンプさせないマン
    }

    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        sc = FindObjectOfType<SceneChange>();
        fadeControl = FindObjectOfType<FadeControl>();
    }

    void Update()
    {
        //プレイヤー操作部分
        if (playerMove == false)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.transform.Translate(-0.01f, 0.0f, 0.0f);
                transform.localScale = new Vector3(-0.5f, 0.4710938f, 1);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                this.transform.Translate(0.01f, 0.0f, 0.0f);
                transform.localScale = new Vector3(0.5f, 0.4710938f, 1);
            }

            if (Jump == false && Input.GetKeyDown(KeyCode.Space))
            {
                this.rigid2D.AddForce(transform.up * this.jumpForce);
                Jump = !Jump;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //判定の場所を通過したら発生
        if (collision.gameObject.tag == "GoTitleLogo")
        {
            fadeControl.Fade("wout", () => fadeControl.sceneChange.SceneSwitching("TitleLogo", true));
        }

        if (collision.gameObject.tag == "GoMap2")
        {
            fadeControl.Fade("out", () => fadeControl.sceneChange.SceneSwitching("MainTitle"));
        }
    }
}
