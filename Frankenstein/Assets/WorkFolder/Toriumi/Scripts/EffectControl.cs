using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectControl : MonoBehaviour
{

    private ParticleSystem Particle;
    public GameObject m_effect;         //�v���n�u

    // Effect���A�N�e�B�u��
    void Start()
    {
        m_effect = GameObject.Find("Effect");   //��\���ɂ��Ă�������悤��
        m_effect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Effect���A�N�e�B�u��
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")   //���g�̕ύX���肢���܂��B
        {
           m_effect.SetActive(true);
        }
    }

}



//�d�C�̓��o���ɃG�t�F�N�g������

/*�\��*/

// 1 �d�C�����鎞�ɃG�t�F�N�g�𗬂�

// Effect���A�N�e�B�u��
//
// if(�d�C����ꂽ��)
// {
//    Effect�A�N�e�B�u��  
// }

