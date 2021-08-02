using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Charger : MonoBehaviour
{
    LineRenderer line;
    private 
    int count;
    private void Start()
    {
        line = GetComponent<LineRenderer>();

    }
    private void Update()
    {
        if (this.transform.position.x >= 0)
        {
            if (Input.GetKeyUp(KeyCode.W))
            {
                Debug.Log("ok");
                this.gameObject.transform.parent = null;
            }
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.W))
            {
                Debug.Log("ok");
                this.gameObject.transform.parent = null;
                Transform charger = this.transform;
                Vector2 pos = charger.position;
                pos.x = -5.5f;
                pos.y = -1.9f;
                charger.position = pos;
            }
            
        }
    }

    private void FixedUpdate()
    {
        count += 1;
        line.positionCount = count;
        line.SetPosition(count-1,transform.position);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Dog")
        {
            if(Input.GetKey(KeyCode.W))
            {
                this.gameObject.transform.parent = GameObject.Find("Dog").transform;
            }
        }
    }

    /*
     �@���X�N���v�g���e��

      �ELineRenderer���g���Đ����쐬�B
    �@�@�P�[�u���̌�̋O�Ղ������B
      �E�����̂ɓ����蔻�肪�Ȃ��B
      �E�P�[�u����W�������ƌ��H�̎q���ɂ��Ă邽�ߓ����͓����B
    �@�@�����Ɖ��������B
    �@�E�A���P�[�u�������̈ʒu(�����ł�1�ȏ�)�ł͂Ȃ��Ƃ��A
    �@�@�����l(�����ł͂��̂܂܂̏����l)�ɖ߂�B
    �@�E�����łȂ��ꍇ�̓P�[�u���͂��̏�Ɏ��c�����B
    �@
    �@���C���_��

    �@�E���͂�����ł����R���݂ɕ`����B
    �@�E�������̏�Ɏc�����܂܏�����������B
    �@

     */

}
