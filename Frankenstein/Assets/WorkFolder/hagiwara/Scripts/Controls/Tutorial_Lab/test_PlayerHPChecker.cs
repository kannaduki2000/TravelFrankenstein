using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test_PlayerHPChecker : MonoBehaviour
{
    /*�m�F�p�Ȃ̂ŁA�m�F�ł�����A�^�b�`���Ă���I�u�W�F�N�g�Ƃ��̃X�N���v�g�������Ă�������*/
    GameObject text;
    GameObject player;
    Text TEXT;
    test_playerController test_PlayerController;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("test_player");
        text = GameObject.Find("test_PlayerHPCheck");
        TEXT = text.GetComponent<Text>();
        test_PlayerController = player.GetComponent<test_playerController>();
    }

    // Update is called once per frame
    void Update()
    {
        TEXT.text = "�t�����P����HP:" + test_PlayerController.HP + "\n" + "�m�F�p�Ȃ̂Ŋm�F�ł���������Ă�������";
    }
}
