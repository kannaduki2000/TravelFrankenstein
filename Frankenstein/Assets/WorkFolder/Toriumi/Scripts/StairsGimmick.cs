using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsGimmick : MonoBehaviour
{
    GameObject anim;

    //�A�j���[�V������transform�Őݒ肵�Ă���܂��B
    //�d�C������̑����P������

    private void OnTriggerStay2D(Collider2D collision)//���c:OnCollision����OnTrigger�ɕύX
    {
        //�K�i�̃I�u�W�F�N�g�擾
        anim = GameObject.Find("Stairs");

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("���ɓ������Ă�");
            if (Input.GetKey(KeyCode.P))
            {
                //StairsAnim����A�j���[�V�����Đ��X�N���v�g�Ăяo��

                anim.gameObject.GetComponent<StairsAnim>().Stairs();
                Debug.Log("�A�j���[�V�����Đ�");

            }
        }
    }







    //�A�j���[�V�����J�n
    //�A�j���[�V�����I��

}
