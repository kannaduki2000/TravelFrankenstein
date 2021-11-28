using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //必要です！！

public class BGMController : MonoBehaviour
{

    //ヒエラルキーからD&Dしておく
    public AudioClip BGM_title;
    public AudioClip BGM_main;

    //使用するAudioSource
    private AudioSource source;

    //１つ前のシーン名
    private string beforeScene = "MainTitle";

    // Use this for initialization
    void Start()
    {
        //自分をシーン切り替え時も破棄しないようにする
        DontDestroyOnLoad(gameObject);

        //使用するAudioSource取得
        source = GetComponent<AudioSource>();

        //最初のBGM再生
        source.clip = BGM_title;
        source.Play();

        //シーンが切り替わった時に呼ばれるメソッドを登録
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    //シーンが切り替わった時に呼ばれるメソッド
    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
        //シーンがどう変わったかで判定

        //メニューからメインへ
        if (beforeScene == "MainTitle" && nextScene.name == "MainTutorial")
        {
            source.Stop();
            source.clip = BGM_main;    //流すクリップを切り替える
            source.Play();
        }

        //メインからメニューへ
        if (beforeScene == "TrueEndScene" || beforeScene == "BadEndScene" && nextScene.name == "MainTitle")
        {
            source.Stop();
            source.clip = BGM_title;    //流すクリップを切り替える
            source.Play();
        }

        //遷移後のシーン名を「１つ前のシーン名」として保持
        beforeScene = nextScene.name;
    }
}