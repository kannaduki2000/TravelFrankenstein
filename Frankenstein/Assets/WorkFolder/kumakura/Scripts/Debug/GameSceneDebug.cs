using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneDebug : MonoBehaviour
{
    void Update()
    {
        #if UNITY_EDITOR
            // 左コントロールキー
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                // シーンの再読み込み
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
