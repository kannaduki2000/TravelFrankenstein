using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test_PlayerHPChecker : MonoBehaviour
{
    /*確認用なので、確認できたらアタッチしているオブジェクトとこのスクリプトを消してください*/
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
        TEXT.text = "フランケンのHP:" + test_PlayerController.HP + "\n" + "確認用なので確認できたら消してください";
    }
}
