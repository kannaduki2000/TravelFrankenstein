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

    void Start()
    {
        anim = GetComponent<Animator>();
        imageData = FindObjectOfType<ImageData>();
        announceImage = imageData.GetAnnounceImage(AnnounceName.T_SquareButton);

    }
    void Update()
    {
        if (craneMove == true)
        {
            AnimationControl();
            announceObject.enabled = false;

        }
    }
    public void AnimationControl()
    {
        //"e"キーを押したときの処
        //エネミーを非アクティブ
        enem.SetActive(false);
        //エネミー1をアクティブ
        enem1.SetActive(true);
        //アニメーションCraneを再生
        anim.Play("Crane");

        Invoke("CrashFrame", 1.5f);

    }

    private void CrashFrame()
    {
        drop.foll = true;
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
