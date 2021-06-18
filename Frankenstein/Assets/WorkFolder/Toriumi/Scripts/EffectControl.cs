using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectControl : MonoBehaviour
{

    private ParticleSystem Particle;
    public GameObject m_effect;         //プレハブ

    // Effectを非アクティブ化
    void Start()
    {
        m_effect = GameObject.Find("Effect");   //非表示にしても見つかるように
        m_effect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Effectをアクティブ化
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")   //中身の変更お願いします。
        {
           m_effect.SetActive(true);
        }
    }

}



//電気の入出時にエフェクトが発生

/*構造*/

// 1 電気を入れる時にエフェクトを流す

// Effectを非アクティブ化
//
// if(電気を入れたら)
// {
//    Effectアクティブ化  
// }

