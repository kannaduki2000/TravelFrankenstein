using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameSceneDebug : MonoBehaviour
{
    [SerializeField] private bool debugMode = false;
    private PlayerController player = null;
    private TextController text = null;
    private FadeControl fade;


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

        fade = FindObjectOfType<FadeControl>();
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

    private void GetUp()
    {
        EventFlagManager.Instance.SetFlagState(EventFlagName.frankensteinGetUp, true);
        EventFlagManager.Instance.SetFlagState(EventFlagName.getupFlag, true);
        EventFlagManager.Instance.SetFlagState(EventFlagName.electricAabsorption, true);
        EventFlagManager.Instance.SetFlagState(EventFlagName.enemyCharge, true);
    }

    public void ChangeMap1()
    {
        GetUp();
        fade.hiyoko = true;
        fade.Fade("out", ()=> fade.sceneChange.SceneSwitching("MainStage_1"));
    }

    public void ChangeMap2()
    {
        GetUp();
        fade.hiyoko = true;
        fade.Fade("out", ()=> fade.sceneChange.SceneSwitching("Main_Stag2"));
    }

}
#if UNITY_EDITOR

[CustomEditor(typeof(GameSceneDebug))]
public class GameSceneDebugEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.HelpBox(
        "DebugMode\n" +
        "true : デバッグモードの有効化\n" +
        "false : デバッグモードの無効化\n\n", MessageType.Info);
        EditorGUILayout.HelpBox(
        "～使い方～\n" +
        "左Ctrlキー : シーンの再読み込み\n" +
        "左Shiftキー : ゲーム内時間3倍に変更\n" +
        "右Shiftキー : ゲーム内時間0.5倍に変更\n" +
        "Shiftキーを離すと通常のゲーム内時間に戻ります\n" +
        "左Tabキー +\n" +
        "　　　Tキー : テキストの表示速度最速化\n" +
        "　　　Pキー : プレイヤーの移動速度上昇", MessageType.Info);
        EditorGUILayout.HelpBox(
        "Button\n" +
        "ゲーム再生中に下のボタンを押すことで指定のシーンに飛べます", MessageType.Info);


        GameSceneDebug gameSceneDebug = target as GameSceneDebug;

        if (GUILayout.Button("Map1"))
        {
            gameSceneDebug.ChangeMap1();
        }

        if (GUILayout.Button("Map2"))
        {
            gameSceneDebug.ChangeMap2();
        }


        EditorGUILayout.HelpBox("BuildErrorが起こる場合はこのGameObjectを破棄してください", MessageType.Warning);
    }
}

#endif