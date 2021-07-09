using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Vector2 Speed = new Vector2(1,1);   //���x
    Animator anim;
   
    void Start()
    {
        //Animetor�R���|�l�[�V�������擾����
        anim = GetComponent<Animator>();

    }

    // �A�b�v�f�[�g�̓t���[�����Ƃ�1��Ăяo�����
    void Update()
    {
        anim = gameObject.GetComponent<Animator>();
        //�ړ�
        Vector2 Position = transform.position;
        if (Input.GetKey(KeyCode.A))
        {
            Position.x -= Speed.x;
            anim.SetBool("Walking", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Position.x += Speed.x;
            anim.SetBool("Walking", true);
        }
        else 
        {
            anim.SetBool("Walking",false);
        }
        transform.position = Position;

        //�������]
        float x = Input.GetAxisRaw("Horizontal");
        if (x != 0)
        {
            Vector2 Iscale = gameObject.transform.localScale;
            if ((Iscale.x > 0 && x < 0) || (Iscale.x < 0 && x > 0))
            {
                Iscale.x *= -1;
                gameObject.transform.localScale = Iscale;
            }
        }

    }

  


}
