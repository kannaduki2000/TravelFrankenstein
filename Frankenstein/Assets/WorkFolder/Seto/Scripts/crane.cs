using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class crane : MonoBehaviour
{
    [SerializeField] GameObject enem;
    [SerializeField] GameObject enem1;
    private Animator anim;

    [SerializeField] dropdown drop;

    public bool craneMove = false;

    [SerializeField] private Sprite announceImage;
    [SerializeField] private ImageData imageData;
    [SerializeField] private Image announceObject;

    private PlayerController player;
    private EventBandController band;

    void Start()
    {
        anim = GetComponent<Animator>();
        imageData = FindObjectOfType<ImageData>();
        announceImage = imageData.GetAnnounceImage(AnnounceName.T_SquareButton);
        player = FindObjectOfType<PlayerController>();
        band = FindObjectOfType<EventBandController>();

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            AnimationControl();
        }

        if (craneMove == true)
        {
            AnimationControl();
            announceObject.enabled = false;
            craneMove = false;
        }
    }
    public void AnimationControl()
    {
        player.PlayerNotMove();
        band.EventStart(()=>
        {
            //"e"キーを押したときの処
            //エネミーを非アクティブ
            enem.SetActive(false);
            //エネミー1をアクティブ
            enem1.SetActive(true);
            //アニメーションCraneを再生
            anim.Play("Crane");
        }
        );
    }

     /// <summary>
     /// anim
     /// </summary>
    public void CrashFrame()
    {
        StartCoroutine(drop.DropDown(()=>
        {
            band.EventEnd(()=>
            {
                player.PlayerMove();
            }
            );
        }
        ));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            announceObject.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            announceObject.enabled = false;
        }
    }
}
