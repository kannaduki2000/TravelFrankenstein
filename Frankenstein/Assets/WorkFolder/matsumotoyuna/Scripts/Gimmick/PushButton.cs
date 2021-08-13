using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButton : MonoBehaviour
{
    public MinecartPush mpush;
    public RotationEnemy rotenemy;
    public RotLadder rotladder;

    private GameObject Button;
    public GameObject Button2;
    private float speed = 5f;
    public bool pushingbutton = false;
    public bool rpush = false;
    public bool notpushingbutton = false;
    public bool notpushingbutton2 = false;

    //����~�肽��̃{�^���p
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "MineCart")
        {
            pushingbutton = true;
        }
    }

    //�͂������グ��������{�^���p
    //�g���b�R���{�^���������Ă��鎞
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "MineCart")
        {
            notpushingbutton2 = false;
            pushingbutton = true;
        }
    }

    //�g���b�R���{�^�����痣�ꂽ�Ƃ�
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "MineCart")
        {
            notpushingbutton2 = true;
        }
    }

    void Update()
    {
        if (pushingbutton == true || notpushingbutton2 == true)
        {
            PushingButton();
        }
    }

    public void PushingButton()
    {
        //����~�肽��̃{�^���p
        //�{�^�������Ɉړ�����^�C�v
        if(notpushingbutton == false && this.gameObject.name == "Button")
        {
            Transform button = this.transform;
            Vector2 buttonpos = button.position;

            buttonpos.x = Mathf.MoveTowards(buttonpos.x, -15.5f, Time.deltaTime * speed);
            button.position = buttonpos;

            Invoke("NotPushButton", 2.0f);
        }

        //�͂������グ��������{�^���p
        //�{�^�������Ɉړ�����^�C�v
        if(notpushingbutton2 == false && this.gameObject.name == "Button2")
        {
            Transform button2 = this.transform;
            Vector2 button2pos = button2.position;

            button2pos.y = Mathf.MoveTowards(button2pos.y, -4.4f, Time.deltaTime * speed);
            button2.position = button2pos;

            rotenemy.rothaguruma = true;
            rotladder.pushtorotation = true;
        }

        //�͂������グ��������{�^���p
        //�{�^������Ɉړ�����^�C�v
        if (notpushingbutton2 == true && this.gameObject.name == "Button2")
        {
            Transform button2 = this.transform;
            Vector2 button2pos = button2.position;

            button2pos.y = Mathf.MoveTowards(button2pos.y, -4.236f, Time.deltaTime * speed);
            button2.position = button2pos;

            rotenemy.rothaguruma = false;
            rotenemy.kasokudekiru = false;
            rotladder.pushtorotation = false;
        }
    }

    public void NotPushButton()
    {
        notpushingbutton = true;
        mpush.playertouch = true;
        rpush = true;
    }
}