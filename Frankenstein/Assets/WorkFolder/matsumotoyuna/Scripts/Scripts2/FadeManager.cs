using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    float fadeSpeed = 0.0005f; //�J�ڑ��x
    float red, green, blue, alpha;

    //bool�^�@�`3��̃t�F�[�h��Y���ā`
    public bool isFadeout = false;
    public bool isFadein = false;
    public bool isWhiteout = false;

    //�C���[�W��L�����o�X
    Image fadeImage;

    [SerializeField] private GameObject canvas;

    //��Z�FSceneChange����
    public SceneChange sc;

    //Dont�`�̌Ăт����h�~
    private static bool dontDestroy = false;

    // Start is called before the first frame update
    void Start()
    {
        fadeImage = GetComponent<Image>();
        red = fadeImage.color.r;
        green = fadeImage.color.g;
        blue = fadeImage.color.b;
        alpha = fadeImage.color.a;

        if (!dontDestroy)
        {
            DontDestroyOnLoad(canvas);
            dontDestroy = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //���ꂼ��̃t�F�[�h�ɔ�Ԃ�
        if(isFadein)
        {
            StartFadeIn();
        }

        if(isFadeout)
        {
            StartFadeOut();
        }

        if(isWhiteout)
        {
            StartWhiteOut();
        }
    }

    void StartFadeIn()
    {
        //�t�F�[�h�C��
        alpha -= fadeSpeed;
        SetAlpha();

        if(alpha <= 0)
        {
            isFadein = false;
            fadeImage.enabled = false;

            /*if(���T�̉��� == true)
            {
                SceneManager.LoadScene("TentativeMap1");
                sc.Map1 = false;
            }*/
        }
    }

    public void StartFadeOut()
    {
        //�����t�F�[�h�A�E�g
        fadeImage.enabled = true;
        alpha += fadeSpeed;
        SetAlpha();

        if(alpha >= 1)
        {
            //�����Ƀt�F�[�h�C�����ς܂����܂�ze
            isFadeout = false;
            isFadein = true;

            if(sc.Tutorial == true)
            {
                //�`���[�g���A����
                SceneManager.LoadScene("TentativeTutorial");
                sc.Tutorial = false;
            }

            if(sc.Title == true)
            {
                //�^�C�g����
                SceneManager.LoadScene("TentativeTitle");
                sc.Title = false;
            }

            if(sc.Map1 == true)
            {
                //���T��������ĂˁA������
            }

            if (sc.Map2 == true)
            {
                //Map2��
                //�{���̓}�b�v3�ɑJ��
                SceneManager.LoadScene("TentativeTitle");
                sc.Map2 = false;
            }
        }
    }

    void StartWhiteOut()
    {
        //�����t�F�[�h�A�E�g
        alpha += fadeSpeed;
        SetAlpha();

        if(alpha >= 1)
        {
            //�����Ƀt�F�[�h�C�����ς܂��}�X�I
            isFadein = true;
            isWhiteout = false;

            if (sc.Logo == true)
            {
                //�^�C�g�����S��Map1��
                SceneManager.LoadScene("TitleLogo");
                sc.Logo = false;
                isFadeout = true;
                sc.Map1 = true;
            }
        }
    }

    void SetAlpha()
    {
        fadeImage.color = new Color(red, green, blue, alpha);
    }
}
