using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsAnim : MonoBehaviour
{
    Animator anim;
    GameObject wall;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Stairs()
    {
        //�A�j���[�V�������Đ�
        //���[�v��؂��Ă���̂ň��̂ݍĐ��B
        //����͖Œ��ꒃ����B
        anim.SetBool("Stairs", true);
    }

    //�A�j���[�V�����C�x���g�ɂĐݒ�B
    //�Đ���A�����ȕǂ���\���ɂȂ�B
    private void Wall()
    {
        wall = GameObject.Find("TransparentWall");
        wall.SetActive(false);
        Debug.Log("�ǁA��\��");
    }

    
}
