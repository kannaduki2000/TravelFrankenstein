using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    //¢Š«
    public FadeManager fm;

    public FadeControl fadeControl;

    //FX
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
        //‘JˆÚ
        SceneManager.LoadScene(sceneName);
        if (isFadeIn)
        {
            //”’‚­‚È‚éŽžŒÀ’è
            if (isWhite)
            {
                fadeControl.Fade("win", ()=> fadeControl.Fade("out", ()=> fadeControl.sceneChange.SceneSwitching("TentativeMap1")));
                return;
            }
            fadeControl.Fade("in");
        }
    }
}