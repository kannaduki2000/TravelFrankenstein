using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricCurrent : MonoBehaviour
{
    // HP��\��
    int HP = 100;

    public float speed;
    private Rigidbody2D rb;

    private bool touchFlag = false;

    public GameObject hpBar;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // �v���C���[�ړ�
        float horizontalKey = Input.GetAxis("Horizontal");

        if (horizontalKey > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else if (horizontalKey < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (touchFlag)
        {
            Debug.Log("aaa");
            // �\��
            hpBar.SetActive(true);

            // �d�C�𗬂�
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // HP�����炷
                HP -= 30;
                Debug.Log(HP);
                // �����ɏ�����������
            }
            // �d�C���[�d
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // HP�𑝂₷
                HP += 30;
                Debug.Log(HP);
                // �����ɏ�����������
            }


        }


        // ��\��

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HomeApp")
        {
            touchFlag = true;   
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HomeApp")
        {
            touchFlag = false;
            hpBar.SetActive(false);
        }
    }
}

/*
���d���֌W
    �d�C�𗬂��Z
�@�@�d�C���[�d����Z
�@�@�d�C�𗬂��邩�ǂ����̔���Z
�@�@�@������Ȃ痬����Ώۂ̏�ԁi�d�C�𗬂�or�[�d�j���擾�Z
�@�@ �@��ނɍ��킹������������
�@�@�@�@�i�G�Ȃ炻�̓G�ɉ������d�C������ĒǏ]�j
�@�@�@�@�i�M�~�b�N�Ȃ炻�̃M�~�b�N�ɉ����������j
�@�@�d�C�𗬂��镨�̋߂��ɗ�������HP�o�[�ƃ{�^���̕\�� �Z
*/