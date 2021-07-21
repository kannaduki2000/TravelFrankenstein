using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StairsGimmick : ElectricItem
{
    [SerializeField] private GameObject anim;
    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject wall;
    [SerializeField] private Sprite announceImage;
    [SerializeField] private Image announceObject;

    private void Start()
    {
        Power = 30;
        AnnounceImage = new Sprite[1];
        AnnounceImage[0] = announceImage;
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
        // �A�j���[�V�����Đ�
        anim.gameObject.GetComponent<StairsAnim>().Stairs();
        player.PlayerNotMove();
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


    //�A�j���[�V������transform�Őݒ肵�Ă���܂��B
    //�d�C������̑����P������

    //private void OnTriggerStay2D(Collider2D collision)//���c:OnCollision����OnTrigger�ɕύX
    //{

    //    if (collision.gameObject.tag == "Player")
    //    {
    //        Debug.Log("���ɓ������Ă�");
    //        if (Input.GetKey(KeyCode.P))
    //        {
    //            //StairsAnim����A�j���[�V�����Đ��X�N���v�g�Ăяo��

    //            anim.gameObject.GetComponent<StairsAnim>().Stairs();
    //            Debug.Log("�A�j���[�V�����Đ�");

    //        }
    //    }
    //}







    //�A�j���[�V�����J�n
    //�A�j���[�V�����I��

}
