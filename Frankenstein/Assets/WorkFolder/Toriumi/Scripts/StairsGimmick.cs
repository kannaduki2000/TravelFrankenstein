using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StairsGimmick : ElectricItem
{
    private Animator switchAnim;

    [SerializeField] private GameObject anim;
    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject wall;
    [SerializeField] private Sprite announceImage;
    [SerializeField] private ImageData imageData;
    [SerializeField] private Image announceObject;

    private void Start()
    {
        switchAnim = GetComponent<Animator>();
        Power = 30;
        imageData = FindObjectOfType<ImageData>();
        announceImage = imageData.GetAnnounceImage(AnnounceName.S1_SquareButton_Input);
        anim = GameObject.Find("Stairs");
    }

    private void Update()
    {
        if (IsChargeEvent)
        {
            announceObject.enabled = false;
        }
    }

    public override void Event()
    {
        // アニメーション再生
        anim.gameObject.GetComponent<StairsAnim>().Stairs();
        player.PlayerNotMove();
        switchAnim.Play("Battery Animation");
    }

    public void AnimationEnd()
    {
        player.PlayerMove();
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


    //アニメーションはtransformで設定してあります。
    //電気を入れるの代わりにPを押す

    //private void OnTriggerStay2D(Collider2D collision)//半田:OnCollisionからOnTriggerに変更
    //{

    //    if (collision.gameObject.tag == "Player")
    //    {
    //        Debug.Log("物に当たってる");
    //        if (Input.GetKey(KeyCode.P))
    //        {
    //            //StairsAnimからアニメーション再生スクリプト呼び出し

    //            anim.gameObject.GetComponent<StairsAnim>().Stairs();
    //            Debug.Log("アニメーション再生");

    //        }
    //    }
    //}







    //アニメーション開始
    //アニメーション終了

}
