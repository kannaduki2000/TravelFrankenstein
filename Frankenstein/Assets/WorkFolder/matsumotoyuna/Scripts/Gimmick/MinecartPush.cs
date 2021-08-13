using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Physics;

//�G�l�~�[���g���b�R�̑O��R�������ƁA
//�g���b�R�������d�g�݂̃X�N���v�g����I
public class MinecartPush : MonoBehaviour
{
    public Rigidbody2D rigid2D;
    public PhysicsMaterial2D physics2D;
    public GameObject aaa;
    private float speed = 2f;
    public bool minecartpush = false;
    public bool playertouch = false;
    public bool playernotouch = false;
    public bool enemytouch = false;
    public bool enemyrpush = false;
    public bool movestop = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floar")
        {
            //����̏��ɒ����܂ł�Z��]���Œ肵�Ă܂�
            //���̏���Floar�^�O��ݒ肵�Ă��ꂽ�܂�
            rigid2D.constraints = RigidbodyConstraints2D.None;
            enemytouch = true;
        }

        else if (collision.gameObject.name == "Button")
        {
            rigid2D.constraints = RigidbodyConstraints2D.FreezePositionX;
            Invoke("MoveStop", 0.5f);
        }

        //�g���b�R�ƃv���C���[�̂���Ⴂ�΍�
        //���̑O�Ƀ��C���[�̐ݒ肵�ĂˁI
        //(project setting? �̂Ƃ���̂��)
        else if (collision.gameObject.name == "Player" && playertouch == false)
        {
            aaa.SetActive(false);
            playernotouch = true;
        }

        else if (collision.gameObject.name == "Enemy" && enemytouch == true)
        {
            aaa.SetActive(false);
            enemytouch = false;
            Invoke("SActive2", 2.0f);
        }
    }

    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (minecartpush == true)
        {
            MineCartPush();
        }

        if (playernotouch == true)
        {
            Invoke("SActive", 2.0f);
        }

        //�G�l�~�[����Ńg���b�R��������p�ɏd���ύX
        if (enemyrpush == true)
        {
            rigid2D.mass = 1;
        }

        else if(enemyrpush == false)
        {
            rigid2D.mass = 500;
        }
    }

    public void MineCartPush()
    {
        if (movestop == false)
        {
            Transform push = this.transform;
            Vector2 minecartposition = push.position;

            minecartposition.x = Mathf.MoveTowards(minecartposition.x, 3.5f, Time.deltaTime * speed);
            push.position = minecartposition;

            Invoke("MoveStop", 2.0f);
        }
    }

    public void MoveStop()
    {
        movestop = true;
        rigid2D.constraints = RigidbodyConstraints2D.None;
    }

    public void SActive()
    {
        playertouch = true;
        aaa.SetActive(true);
    }

    public void SActive2()
    {
        aaa.SetActive(true);
    }
}
