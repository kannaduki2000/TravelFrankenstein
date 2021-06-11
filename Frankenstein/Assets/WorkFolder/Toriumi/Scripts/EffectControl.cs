using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectControl : MonoBehaviour
{
    private ParticleSystem Particle;
    public GameObject m_particle1;
    public GameObject m_particle2;
    public GameObject m_particle3;
    public GameObject m_particle4;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        var pos = GetComponent<Transform>().localPosition;
        //�����L�[�������Ă���
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            pos.x -= 0.01f;
        }
        //�E���L�[�������Ă���
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            pos.x += 0.01f;
        }
        GetComponent<Transform>().localPosition = pos;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Instantiate(m_particle1.gameObject, transform.position, transform.rotation);
            Instantiate(m_particle2.gameObject, transform.position, transform.rotation);
            Instantiate(m_particle3.gameObject, transform.position, transform.rotation);
            Instantiate(m_particle4.gameObject, transform.position, transform.rotation);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(m_particle1.gameObject);
            Destroy(m_particle2.gameObject);
            Destroy(m_particle3.gameObject);
            Destroy(m_particle4.gameObject);
        }
    }

}

    

    //�d�C�̓��o���ɃG�t�F�N�g������

    /*�g�ݗ���*/

    // 1 �d�C�����鎞�ɃG�t�F�N�g�𗬂�
    // if(�d�C����ꂽ��)
    // {
    //  �d�C�G�t�F�N�g���Đ�����
    //  ���K�v�H�Đ����Ԃ�0�ɂ���
    // }

    // 2 �t�����P���̃_���[�W
    // if(Enemy����_���[�W���󂯂���)
    // {
    //  �_���[�W�G�t�F�N�g���Đ�����
    //  ���K�v�H�Đ����Ԃ�0�ɂ���
    // }
