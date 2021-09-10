using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButton : MonoBehaviour
{
    public MinecartPush mpush;
    public RotationEnemy rotenemy;
    public RotLadder rotladder;

    public GameObject Button;
    public GameObject Button1;
    public GameObject Button2;
    private float speed = 5f;
    public bool pushingbutton = false;     //トロッコとボタンが接触したとき用
    public bool rpush = false;             //最初にトロッコ押す用
    public bool notpushingbutton = false;
    public bool notpushingbutton2 = false;

    //坂を降りた後のボタン用
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "MineCart")
        {
            pushingbutton = true;
        }
    }

    //はしごを上げ下げするボタン用
    //トロッコがボタンを押した時
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "MineCart")
        {
            notpushingbutton2 = false;
            pushingbutton = true;
        }
    }

    //トロッコがボタンから離れたとき
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "MineCart")
        {
            notpushingbutton2 = true;
        }
    }

    void Update()
    {
        //ボタン押す判定がtrueかな？もしそうなら…
        if (pushingbutton == true || notpushingbutton2 == true)
        {
            PushingButton();
        }
    }

    public void PushingButton()
    {
        //坂を降りた後のボタン用
        //ボタンが横に移動するタイプ
        if(notpushingbutton == false && this.gameObject.name == "Button")
        {
            Transform button = this.transform;
            Vector2 buttonpos = button.position;

            buttonpos.x = Mathf.MoveTowards(buttonpos.x, -15.5f, Time.deltaTime * speed);
            button.position = buttonpos;

            Invoke("NotPushButton", 2.0f);
        }

        //はしごを上げ下げするボタン用
        //ボタンが下に移動するタイプ
        if(notpushingbutton2 == false && this.gameObject.name == "Button2")
        {
            Transform button2 = this.transform;
            Vector2 button2pos = button2.position;

            button2pos.y = Mathf.MoveTowards(button2pos.y, -4.4f, Time.deltaTime * speed);
            button2.position = button2pos;

            rotenemy.rothaguruma = true;
            rotladder.pushtorotation = true;
        }

        //はしごを上げ下げするボタン用
        //ボタンが上に移動するタイプ
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
        //ずっとnotpushingbutton部分のMove.Towardsが続かないようにする
        //プレイヤーとのすれ違い対策などのレイヤー変更などなど
        notpushingbutton = true;
        Button1.gameObject.layer = 6;
        Button.gameObject.layer = 6;
        rpush = true;
    }
}