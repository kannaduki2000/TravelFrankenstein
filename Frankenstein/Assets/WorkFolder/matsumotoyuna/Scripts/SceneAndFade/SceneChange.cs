using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    //召喚
    public FadeManager fm;

    public FadeControl fadeControl;

    //色々
    public bool Title = false;
    public bool Tutorial = false;
    public bool Map1 = false;
    public bool Map12 = false;
    public bool Logo = false;
    public bool Map2 = false;
    Rigidbody2D rigid2D;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SceneSwitching(string sceneName, bool isWhite = false, bool isFadeIn = true)
    {
        //遷移
        SceneManager.LoadScene(sceneName);
        if (isFadeIn)
        {
            //白くなる時限定
            if (isWhite)
            {
                // シーン遷移後フラグを立てる
                fadeControl.Fade("win", ()=> fadeControl.Fade("out", ()=> 
                {
                    SceneSwitching("MainStage_1");
                }
                ));
                return;
            }

            // くま：書き方汚いんで書き直す
            if (sceneName == "MainTutorial")
            {
                // FadeIn終わったらフラグを立てる
                fadeControl.Fade("in", ()=> EventFlagManager.Instance.SetFlagState(EventFlagName.frankensteinGetUp, true));
                return;
            }

            if (sceneName == "BadEndScene")
            {
                fadeControl.Fade("in", () => fadeControl.Fade("out", () => SceneSwitching("MainTitle")));
                return;
            }
            fadeControl.Fade("in");
        }
    }
}