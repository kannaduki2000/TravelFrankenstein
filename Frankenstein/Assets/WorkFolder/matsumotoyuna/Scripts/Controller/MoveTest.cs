using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour
{
    public GameObject player;

    public bool playerMove = false;
    private bool Jump = false;
    public bool touchminecart = false;

    Rigidbody2D rigid2D;
    float jumpForce = 300.0f;

    public SceneChange sc;
    public FadeControl fadeControl;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Jump = false;
        //Å™è∞Ç…íÖÇ≠Ç‹Ç≈ÉWÉÉÉìÉvÇ≥ÇπÇ»Ç¢É}Éì
        if(collision.gameObject.name == "MineCart" && touchminecart == false)
        {
            rigid2D.constraints = RigidbodyConstraints2D.FreezeAll;
            Invoke("Constraints", 2.0f);
        }
    }

    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        sc = FindObjectOfType<SceneChange>();
        fadeControl = FindObjectOfType<FadeControl>();
    }

    void Update()
    {
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
        //îªíËÇÃèÍèäÇí âﬂÇµÇΩÇÁî≠ê∂
        if (collision.gameObject.tag == "GoTitleLogo")
        {
            fadeControl.Fade("wout", () => fadeControl.sceneChange.SceneSwitching("TitleLogo", true));
        }

        if (collision.gameObject.tag == "GoMap2")
        {
            fadeControl.Fade("out", () => fadeControl.sceneChange.SceneSwitching("MainTitle"));
        }
    }

    public void Constraints()
    {
        rigid2D.constraints = RigidbodyConstraints2D.None;
        touchminecart = true;
    }
}
