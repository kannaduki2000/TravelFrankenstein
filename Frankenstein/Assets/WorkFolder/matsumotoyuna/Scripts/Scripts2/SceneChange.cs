using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    //����
    public FadeManager fm;

    public FadeControl fadeControl;

    //�F�X
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
        //�J��
        SceneManager.LoadScene(sceneName);
        if (isFadeIn)
        {
            //�����Ȃ鎞����
            if (isWhite)
            {
                // ���܁F�����f�B���C������������������
                fadeControl.Fade("win", ()=> fadeControl.Fade("out", ()=> SceneSwitching("MainStage_1")));
                return;
            }

            // ���܁F������������ŏ�������
            if (sceneName == "MainTutorial")
            {
                // FadeIn�I�������t���O�𗧂Ă�
                fadeControl.Fade("in", ()=> EventFlagManager.Instance.SetFlagState(EventFlagName.frankensteinGetUp, true));
                return;
            }
            fadeControl.Fade("in");
        }
    }
}