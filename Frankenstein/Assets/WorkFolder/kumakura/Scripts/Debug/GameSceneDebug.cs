using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneDebug : MonoBehaviour
{
    [SerializeField] private bool debugMode = false;
    private PlayerController player = null;
    private TextController text = null;


    void Start()
    {
        if (FindObjectOfType<PlayerController>() != null)
        {
            player = FindObjectOfType<PlayerController>();
        }
        if (FindObjectOfType<TextController>() != null)
        {
            text = FindObjectOfType<TextController>();
        }
    }

    void Update()
    {
        if (!debugMode) return;
        #if UNITY_EDITOR
        // 左コントロールキー
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            // シーンの再読み込み
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // 左Altキー
        if (Input.GetKey(KeyCode.Tab))
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                // テキストの表示速度最速化
                text.textTime = 0.0f;
                // テキストの表示感覚最速化
                text.loadTime = 0.0f;
            }
            
            if (Input.GetKeyDown(KeyCode.P))
            {
                // プレイヤーの移動速度上昇
                player.speed = 20.0f;
            }
        }

        // 左のシフトキー
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            // ゲーム内時間3倍に変更
            Time.timeScale = 3;
        }

        // 右のシフトキー
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            // ゲーム内時間0.5倍に変更
            Time.timeScale = 0.5f;
        }

        // シフトキーを離す
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            // ゲーム内時間等速に戻す
            Time.timeScale = 1;
        }
        #endif
    }
}
