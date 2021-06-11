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
        //左矢印キーを押している
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            pos.x -= 0.01f;
        }
        //右矢印キーを押している
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

    

    //電気の入出時にエフェクトが発生

    /*組み立て*/

    // 1 電気を入れる時にエフェクトを流す
    // if(電気を入れたら)
    // {
    //  電気エフェクトを再生する
    //  ※必要？再生時間を0にする
    // }

    // 2 フランケンのダメージ
    // if(Enemyからダメージを受けたら)
    // {
    //  ダメージエフェクトを再生する
    //  ※必要？再生時間を0にする
    // }
