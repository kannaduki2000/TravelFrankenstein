using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsGimmick : MonoBehaviour
{
  
    public GameObject stairs;
    // Start is called before the first frame update
    void Start()
    {  
        stairs = GameObject.Find("Stairs");
        stairs.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    //���A�j���[�V�����쐬���}��
    //�d�C������̑����P������
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                stairs.gameObject.SetActive(true);

            }
        }
    }


    //�A�j���[�V�����J�n
    //�A�j���[�V�����I��

}
